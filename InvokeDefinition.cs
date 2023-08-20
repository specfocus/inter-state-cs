using XState.Actions;

namespace XState
{
    public class MetaObject : Dictionary<string, object>
    {
    }

    public interface IInvokeDefinition<TContext, TEvent> : IActivity<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        /// <summary>
        /// The source of the machine to be invoked, or the machine itself.
        /// </summary>
        InvokeSourceDefinition Src { get; } // You might need to define InvokeSourceDefinition

        /// <summary>
        /// If <c>true</c>, events sent to the parent service will be forwarded to the invoked service.
        /// </summary>
        bool? AutoForward { get; }

        /// <summary>
        /// Deprecated. Use <c>AutoForward</c> property instead of <c>Forward</c>. Support for <c>Forward</c> will get removed in the future.
        /// </summary>
        bool? Forward { get; }

        /// <summary>
        /// Data from the parent machine's context to set as the (partial or full) context
        /// for the invoked child machine.
        /// Data should be mapped to match the child machine's context shape.
        /// </summary>
        Mapper<TContext, TEvent, object>? Data { get; } // You might need to define Mapper

        /// <summary>
        /// Data from the parent machine's context to set as the (partial or full) context
        /// for the invoked child machine using property mapping.
        /// Data should be mapped to match the child machine's context shape.
        /// </summary>
        PropertyMapper<TContext, TEvent, object>? DataProperty { get; } // You might need to define PropertyMapper

        /// <summary>
        /// Metadata associated with the invoked definition.
        /// </summary>
        MetaObject? Meta { get; } // You might need to define MetaObject
    }

    public class InvokeDefinition<TContext, TEvent> : ActivityDefinition<TContext, TEvent>, IInvokeDefinition<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        private IInvokeConfig<TContext, TEvent> invokeConfig;

        public InvokeDefinition(IInvokeConfig<TContext, TEvent> config)
            : base(Actions.ActionTypes.Invoke.Value, config.Id ?? config.Src.Id)
        {
            invokeConfig = config;
        }

        /// <summary>
        /// The source of the machine to be invoked, or the machine itself.
        /// </summary>
        public InvokeSourceDefinition Src => invokeConfig.Src;

        /// <summary>
        /// If <c>true</c>, events sent to the parent service will be forwarded to the invoked service.
        /// </summary>
        public bool? AutoForward { get; set; }

        /// <summary>
        /// Deprecated. Use <c>AutoForward</c> property instead of <c>Forward</c>. Support for <c>Forward</c> will get removed in the future.
        /// </summary>
        public bool? Forward { get; set; }

        /// <summary>
        /// Data from the parent machine's context to set as the (partial or full) context
        /// for the invoked child machine.
        /// Data should be mapped to match the child machine's context shape.
        /// </summary>
        public Mapper<TContext, TEvent, object>? Data { get; set; } // You might need to define Mapper

        /// <summary>
        /// Data from the parent machine's context to set as the (partial or full) context
        /// for the invoked child machine using property mapping.
        /// Data should be mapped to match the child machine's context shape.
        /// </summary>
        public PropertyMapper<TContext, TEvent, object>? DataProperty { get; set; } // You might need to define PropertyMapper

        /// <summary>
        /// Metadata associated with the invoked definition.
        /// </summary>
        public MetaObject? Meta { get; set; }
    }
}
