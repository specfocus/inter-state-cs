namespace XState
{
    internal static partial class Utils
    {
        /*
         export function toTransitionConfigArray<TContext, TEvent extends EventObject>(
  event: TEvent['type'] | NullEvent['type'] | '*',
  configLike: SingleOrArray<
    | TransitionConfig<TContext, TEvent>
    | TransitionConfigTarget<TContext, TEvent>
  >
): Array<
  TransitionConfig<TContext, TEvent> & {
    event: TEvent['type'] | NullEvent['type'] | '*';
  }
> {
  const transitions = toArrayStrict(configLike).map((transitionLike) => {
    if (
      typeof transitionLike === 'undefined' ||
      typeof transitionLike === 'string' ||
      isMachine(transitionLike)
    ) {
      return { target: transitionLike, event };
    }

    return { ...transitionLike, event };
  }) as Array<
    TransitionConfig<TContext, TEvent> & {
      event: TEvent['type'] | NullEvent['type'] | '*';
    } // TODO: fix 'as' (remove)
  >;

  return transitions;
}
        */
        public static List<TransitionConfig<TContext, TEvent, TEvent>> ToTransitionConfigArray<TContext, TEvent>(
            string eventValue,
            SingleOrArray<TransitionConfig<TContext, TEvent, TEvent>> configLike
        )
            where TContext : class
            where TEvent : Event
        {
            var transitions = new List<TransitionConfig<TContext, TEvent, TEvent>>();
            foreach (var transitionLike in configLike.ToArray())
            {
                if (transitionLike == null || transitionLike is string || IsMachine(transitionLike))
                {
                    transitions.Add(new TransitionConfig<TContext, TEvent, TEvent> { target = transitionLike, @event = eventValue });
                }
                else
                {
                    transitions.Add(new TransitionConfig<TContext, TEvent, TEvent> { @event = eventValue, ... }); // TODO: Copy properties from transitionLike
                }
            }

            return transitions;
        }
    }
}
