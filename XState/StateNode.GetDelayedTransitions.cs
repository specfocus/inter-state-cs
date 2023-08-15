namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// All delayed transitions from the config.
        /// </summary>
        /// <returns></returns>
        private List<DelayedTransitionDefinition<TContext, TEvent>> GetDelayedTransitions()
        {
            var afterConfig = Config.After;

            if (afterConfig == null)
            {
                return new List<DelayedTransitionDefinition<TContext, TEvent>>();
            }

            List<DelayedTransitionDefinition<TContext, TEvent>> delayedTransitions = new List<DelayedTransitionDefinition<TContext, TEvent>>();

            string MutateEntryExit(object delay, int i)
            {
                string delayRef = delay is Delegate ? $"{Id}:delay[{i}]" : delay.ToString();

                string eventType = After(delayRef, Id);

                OnEntry.Add(Send(eventType, new Dictionary<string, object> { ["delay"] = delay }));
                OnExit.Add(Cancel(eventType));

                return eventType;
            }

            if (afterConfig is IList<object> afterList)
            {
                for (int i = 0; i < afterList.Count; i++)
                {
                    object transition = afterList[i];
                    string eventType = MutateEntryExit(transition, i);
                    delayedTransitions.Add(new DelayedTransitionDefinition<TContext, TEvent>
                    {
                        Event = eventType,
                        ... // Add other properties from transition
            });
                }
            }
            else if (afterConfig is IDictionary<string, object> afterDict)
            {
                foreach (var kvp in afterDict)
                {
                    string delay = kvp.Key;
                    object configTransition = kvp.Value;
                    object resolvedTransition = configTransition is string configTransitionString
                        ? new Dictionary<string, object> { ["target"] = configTransitionString }
                        : configTransition;

                    double resolvedDelay = double.TryParse(delay, out var parsedDelay)
                        ? parsedDelay
                        : Convert.ToDouble(delay);

                    string eventType = MutateEntryExit(resolvedDelay, 0);

                    if (resolvedTransition is IList<object> transitionList)
                    {
                        foreach (object transition in transitionList)
                        {
                            delayedTransitions.Add(new DelayedTransitionDefinition<TContext, TEvent>
                            {
                                Event = eventType,
                                Delay = resolvedDelay,
                                ... // Add other properties from transition
                    });
                        }
                    }
                    else
                    {
                        delayedTransitions.Add(new DelayedTransitionDefinition<TContext, TEvent>
                        {
                            Event = eventType,
                            Delay = resolvedDelay,
                            ... // Add other properties from resolvedTransition
                });
                    }
                }
            }

            return delayedTransitions;
        }

    }
}
