namespace XState
{
    using static XState.SCXML;

    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Returns `true` if this state node explicitly handles the given event.
        /// </summary>
        /// <param name="event">The event in question</param>
        /// <returns></returns>
        public bool Handles(Event<TEvent> @event)
        {
            var eventType = @event.GetType();
            return Events.Contains(eventType);
        }

    }
}
