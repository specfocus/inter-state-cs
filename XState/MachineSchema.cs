namespace XState
{
    public interface IMachineSchema<TContext, TEvent, TServiceMap>
        where TContext : class
        where TEvent : Event
        where TServiceMap : ServiceMap
    {
        TContext Context { get; }

        TEvent Events { get; }

        Dictionary<string, object> Actions { get; }

        Dictionary<string, object> Guards { get; }

        TServiceMap Services { get; }
    }

    public class MachineSchema<TContext, TEvent, TServiceMap> : IMachineSchema<TContext, TEvent, TServiceMap>
        where TContext : class
        where TEvent : Event
        where TServiceMap : ServiceMap
    {
        public TContext Context { get; set; }

        public TEvent Events { get; set; }

        public Dictionary<string, object> Actions { get; set; } = new();

        public Dictionary<string, object> Guards { get; set; } = new();

        public TServiceMap Services { get; set; }
    }
}
