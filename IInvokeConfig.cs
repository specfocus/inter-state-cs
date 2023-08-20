namespace XState
{
    public interface IInvokeConfig<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        string? Id { get; }
        InvokeSourceDefinition Src { get; }
        bool AutoForward { get; }
        bool Forward { get; } // Deprecated, use AutoForward
        Mapper<TContext, TEvent, object> Data { get; } // Replace 'object' with appropriate type for mapped data
        IEnumerable<TransitionConfigOrTarget<TContext, DoneInvokeEvent<object>, DoneInvokeEvent<object>>> OnDone { get; } // Replace 'object' with appropriate type
        IEnumerable<TransitionConfigOrTarget<TContext, DoneInvokeEvent<object>, DoneInvokeEvent<object>>> OnError { get; } // Replace 'object' with appropriate type
        MetaObject Meta { get; } // Define 'MetaObject' type appropriately
    }

    public class InvokeConfig<TContext, TEvent> : IInvokeConfig<TContext, TEvent>
       where TContext : class
       where TEvent : Event
    {
        public string? Id { get; set; }
        public InvokeSourceDefinition Src { get; set; }
        public bool AutoForward { get; set; }
        public bool Forward { get; set; } // Deprecated, use AutoForward
        public Mapper<TContext, TEvent, object> Data { get; set; } // Replace 'object' with appropriate type for mapped data
        public IEnumerable<TransitionConfigOrTarget<TContext, DoneInvokeEvent<object>, DoneInvokeEvent<object>>> OnDone { get; set; } // Replace 'object' with appropriate type
        public IEnumerable<TransitionConfigOrTarget<TContext, DoneInvokeEvent<object>, DoneInvokeEvent<object>>> OnError { get; set; } // Replace 'object' with appropriate type
        public MetaObject Meta { get; set; } // Define 'MetaObject' type appropriately
    }
}
