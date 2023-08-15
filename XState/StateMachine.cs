global using AnyStateMachine = XState.StateMachine<object, XState.IStateSchema<object>, XState.Event, XState.Typestate<object>, XState.Actions.BaseActionObject, XState.ServiceMap, XState.TypegenFlag>;

namespace XState
{
    using XState.Actions;

    public interface IStateMachine<
        TContext,
        TStateSchema,
        TEvent,
        TTypestate,
        TAction,
        TServiceMap,
        TResolvedTypesMeta
    > : IStateNode<
        TContext,
        TStateSchema,
        TEvent,
        TTypestate,
        TServiceMap,
        TResolvedTypesMeta
    >
        where TContext : class
        where TStateSchema : IStateSchema<TContext>
        where TEvent : Event
        where TTypestate : Typestate<TContext>
        where TAction : BaseActionObject
        where TServiceMap : ServiceMap
        where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
    {
        StateMachine<
            TContext,
            TStateSchema,
            TEvent,
            TTypestate,
            TAction,
            TServiceMap,
            TResolvedTypesMeta
        >
        WithConfig(
            IInternalMachineOptions<TContext, TEvent> options,
            TContext? context = null // Use appropriate type for context
        );

        StateMachine<
            TContext,
            TStateSchema,
            TEvent,
            TTypestate,
            TAction,
            TServiceMap,
            TResolvedTypesMeta
        >
        WithContext(
            TContext context
        );

        // Deprecated properties - these can be marked as internal or removed in C#
        // The __T... properties are acting as "phantom" types in TypeScript
        TContext __TContext { get; }
        TStateSchema __TStateSchema { get; }
        TEvent __TEvent { get; }
        TTypestate __TTypestate { get; }
        TAction __TAction { get; }
        TServiceMap __TServiceMap { get; }
        TResolvedTypesMeta __TResolvedTypesMeta { get; }
    }

    public class StateMachine<
       TContext,
       TStateSchema,
       TEvent,
       TTypestate,
       TAction,
       TServiceMap,
       TResolvedTypesMeta
   > : StateNode<
        TContext,
        TStateSchema,
        TEvent,
        TTypestate,
        TServiceMap,
        TResolvedTypesMeta
    >, IStateMachine<
       TContext,
       TStateSchema,
       TEvent,
       TTypestate,
       TAction,
       TServiceMap,
       TResolvedTypesMeta
   >
        where TContext : class
        where TStateSchema : IStateSchema<TContext>
        where TEvent : Event
        where TTypestate : Typestate<TContext>
        where TAction : BaseActionObject
        where TServiceMap : ServiceMap
        where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
    {
        public StateMachine(
            ContextProvider<TContext> context,
            // The raw config used to create the machine.
            StateNodeConfig<TContext, TStateSchema, TEvent, BaseActionObject> config,
            // The initial extended state
            MachineOptions<TContext, TEvent> options = null,
            StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> parent = null,
            string key = null
        )
            : base(context, config, options, parent, key)
        {
        }

        public StateMachine<
            TContext,
            TStateSchema,
            TEvent,
            TTypestate,
            TAction,
            TServiceMap,
            TResolvedTypesMeta
        >
        WithConfig(
            IInternalMachineOptions<TContext, TEvent> options,
            TContext? context = null // Use appropriate type for context
        )
        {
            return this;
        }

        public StateMachine<
            TContext,
            TStateSchema,
            TEvent,
            TTypestate,
            TAction,
            TServiceMap,
            TResolvedTypesMeta
        >
        WithContext(
            TContext context
        )
        {
            return this;
        }

        // Deprecated properties - these can be marked as internal or removed in C#
        // The __T... properties are acting as "phantom" types in TypeScript
        public TContext __TContext { get; set; }
        public TStateSchema __TStateSchema { get; set; }
        public TEvent __TEvent { get; set; }
        public TTypestate __TTypestate { get; set; }
        public TAction __TAction { get; set; }
        public TServiceMap __TServiceMap { get; set; }
        public TResolvedTypesMeta __TResolvedTypesMeta { get; set; }
    }
}
