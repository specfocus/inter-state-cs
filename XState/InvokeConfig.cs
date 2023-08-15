namespace XState
{
    public interface InvokeConfig<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        string Id { get; set; }
        object Src { get; set; } // Replace 'object' with appropriate type for InvokeSourceDefinition, AnyStateMachine, or InvokeCreator<TContext, TEvent, any>
        bool AutoForward { get; set; }
        bool Forward { get; set; } // Deprecated, use AutoForward
        Mapper<TContext, TEvent, object> Data { get; set; } // Replace 'object' with appropriate type for mapped data
        IEnumerable<TransitionConfigOrTarget<TContext, DoneInvokeEvent<object>, DoneInvokeEvent<object>>> OnDone { get; set; } // Replace 'object' with appropriate type
        IEnumerable<TransitionConfigOrTarget<TContext, DoneInvokeEvent<object>, DoneInvokeEvent<object>>> OnError { get; set; } // Replace 'object' with appropriate type
        MetaObject Meta { get; set; } // Define 'MetaObject' type appropriately
    }

}
