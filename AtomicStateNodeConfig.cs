using XState.Actions;

namespace XState
{
    public interface AtomicStateNodeConfig<TContext, TEvent>
        : StateNodeConfig<TContext, StateSchema<TContext>, TEvent, BaseActionObject>
        where TContext : class
        where TEvent : Event
    {
        new StateValue Initial { get; }

        bool? Parallel { get; }

        new Dictionary<string, StateNodeConfig<TContext, StateSchema<TContext>, TEvent, BaseActionObject>> States { get; }

        new TransitionConfig<TContext, TEvent, TEvent> OnDone { get; }
    }
}
