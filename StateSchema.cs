namespace XState
{
    public interface IStateSchema<TContext> where TContext : class
    {
        object Meta { get; }

        Partial<TContext> Context { get; }

        IDictionary<string, IStateSchema<TContext>> States { get; }
    }

    public class StateSchema<TContext> : IStateSchema<TContext> where TContext : class
    {
        public object Meta { get; set; }

        public Partial<TContext> Context { get; set; }

        public IDictionary<string, IStateSchema<TContext>> States { get; set; } = new Dictionary<string, IStateSchema<TContext>>();
    }
}
