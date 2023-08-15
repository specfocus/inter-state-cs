namespace XState
{
    public interface IInternalMachineOptions<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        // Deprecated property - Use 'services' instead
        Dictionary<string, ActivityConfig<TContext, TEvent>> Activities { get; }
    }

    public class InternalMachineOptions<TContext, TEvent> : IInternalMachineOptions<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        // Deprecated property - Use 'services' instead
        public Dictionary<string, ActivityConfig<TContext, TEvent>> Activities { get; } = new();
    }
}
