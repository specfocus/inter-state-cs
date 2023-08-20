namespace XState
{
    using System;
    using System.Collections.Generic;

    public class Scheduler
    {
        private bool processingEvent = false;
        private readonly List<Action> queue = new();
        private bool initialized = false;

        // deferred feature
        private readonly SchedulerOptions options;

        public Scheduler(SchedulerOptions? options = null)
        {
            this.options = options ?? new SchedulerOptions();
        }

        public void Initialize(Action? callback = null)
        {
            initialized = true;

            if (callback != null)
            {
                if (!options.DeferEvents)
                {
                    Schedule(callback);
                    return;
                }

                Process(callback);
            }

            FlushEvents();
        }

        public void Schedule(Action task)
        {
            if (!initialized || processingEvent)
            {
                queue.Add(task);
                return;
            }

            if (queue.Count != 0)
            {
                throw new InvalidOperationException("Event queue should be empty when it is not processing events");
            }

            Process(task);
            FlushEvents();
        }

        public void Clear()
        {
            queue.Clear();
        }

        private void FlushEvents()
        {
            while (queue.Count > 0)
            {
                Action nextCallback = queue[0];
                queue.RemoveAt(0);
                Process(nextCallback);
            }
        }

        private void Process(Action callback)
        {
            processingEvent = true;
            try
            {
                callback();
            }
            catch (Exception e)
            {
                // there is no use to keep the future events
                // as the situation is not anymore the same
                Clear();

                throw e;
            }
            finally
            {
                processingEvent = false;
            }
        }
    }
}
