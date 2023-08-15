using XState.Actions;

namespace XState
{
    public interface IMachineConfig<TContext, TStateSchema, TEvent, TAction, TServiceMap, TTypesMeta>
        where TContext : class
        where TStateSchema : IStateSchema<TContext>
        where TEvent : EventObject
        where TAction : BaseActionObject
        where TServiceMap : ServiceMap
    {
        // Properties from StateNodeConfig
        // These should be defined based on your requirements

        // The initial context (extended state)
        LowInfer<TContext> Context { get; }

        // The machine"s own version
        string Version { get; }

        // Optional properties specific to MachineConfig
        IMachineSchema<TContext, TEvent, TServiceMap> Schema { get; }

        TTypesMeta TsTypes { get; set; }
    }

    public class MachineConfig<TContext, TStateSchema, TEvent, TAction, TServiceMap, TTypesMeta>
        where TContext : class
        where TStateSchema : IStateSchema<TContext>
        where TEvent : EventObject
        where TAction : BaseActionObject
        where TServiceMap : ServiceMap
    {
        // Properties from StateNodeConfig
        // These should be defined based on your requirements

        // The initial context (extended state)
        public LowInfer<TContext> Context { get; set; }

        // The machine"s own version
        public string Version { get; set; }

        // Optional properties specific to MachineConfig
        public IMachineSchema<TContext, TEvent, TServiceMap> Schema { get; set; }

        public TTypesMeta TsTypes { get; set; }
    }
}
