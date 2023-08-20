namespace XState.Actions
{
    internal class IActionEntry<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        String Type { get; }

        public List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> Actions { get; }
    }

    internal class ActionEntry<TContext, TEvent> : IActionEntry<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public String Type { get; set; }

        public List<ActionObject<TContext, TEvent, TEvent, BaseActionObject>> Actions { get; set; }
    }
}
