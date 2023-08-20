namespace XState
{
    public interface StateLike<TContext>
        where TContext : class
    {
        StateValue Value { get; set; }

        TContext Context { get; set; }

        EventObject Event { get; set; }

        SCXML.Event<Event> _Event { get; set; }
    }
}
