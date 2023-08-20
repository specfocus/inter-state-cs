namespace XState
{
    public interface WaitForOptions
    {
        int Timeout { get; }
    }

    public class DefaultWaitForOptions : WaitForOptions
    {
        public int Timeout { get; } = 10_000; // 10 seconds
    }

    public static class WaitForHelper
    {
        public static async Task<TEmitted> WaitFor<TActorRef, TEmitted>(
            TActorRef actorRef,
            Func<TEmitted, bool> predicate,
            WaitForOptions options = null
        )
            where TActorRef : IActorRef<Event, TEmitted>
        {
            WaitForOptions resolvedOptions = options ?? new DefaultWaitForOptions();

            if (resolvedOptions.Timeout < 0)
            {
                Console.Error.WriteLine("`timeout` passed to `waitFor` is negative and it will reject its internal promise immediately.");
            }

            TaskCompletionSource<TEmitted> tcs = new TaskCompletionSource<TEmitted>();

            IDisposable subscription = null;
            bool done = false;

            if (resolvedOptions.Timeout != Timeout.Infinite)
            {
                var handle = new System.Threading.Timer(_ =>
                {
                    subscription?.Dispose();
                    tcs.TrySetException(new TimeoutException($"Timeout of {resolvedOptions.Timeout} ms exceeded"));
                }, null, resolvedOptions.Timeout, Timeout.Infinite);
            }

            void Dispose()
            {
                subscription?.Dispose();
                done = true;
            }

            subscription = actorRef.Subscribe(emitted =>
            {
                if (predicate(emitted))
                {
                    Dispose();
                    tcs.TrySetResult(emitted);
                }
            },
            err =>
            {
                Dispose();
                tcs.TrySetException(err);
            },
            () =>
            {
                Dispose();
                tcs.TrySetException(new Exception("Actor terminated without satisfying predicate"));
            });

            if (done)
            {
                subscription.Dispose();
            }

            return await tcs.Task;
        }
    }
}
