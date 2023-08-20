namespace XState
{
    using XState.Actions;

    public class StatesConfig<TContext, TStateSchema, TEvent, TAction> : Dictionary<string, StateNodeConfig<TContext, TStateSchema, TEvent, TAction>>
        where TContext : class
        where TStateSchema : IStateSchema<TContext>
        where TEvent : Event
        where TAction : BaseActionObject
    {
    }
}
