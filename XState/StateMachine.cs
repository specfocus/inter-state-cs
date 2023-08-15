global using AnyStateMachine = XState.StateMachine<object, object, object, object>;
using XState.Actions;

namespace XState
{
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
        where TServiceMap : ServiceMap
        where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
    {
        string Id { get; }

        Dictionary<string, StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> States { get; }

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
       where TServiceMap : ServiceMap
       where TResolvedTypesMeta : TypegenFlag // TypegenDisabled
    {
        public static StateMachine<
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
            TContext context = null // Use appropriate type for context
        )
        {
            throw new NotImplementedException();
        }

        public static StateMachine<
            TContext,
            TStateSchema,
            TEvent,
            TTypestate,
            TAction,
            TServiceMap,
            TResolvedTypesMeta>
        WithContext(
            TContext context // Use appropriate type for context
        )
        {
            throw new NotImplementedException();
        }

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
            Id = id;
            States = states;
            __TContext = tContext;
            __TStateSchema = tStateSchema;
            __TEvent = tEvent;
            __TTypestate = tTypestate;
            __TAction = tAction;
            __TServiceMap = tServiceMap;
            __TResolvedTypesMeta = tResolvedTypesMeta;
        }

        public Dictionary<string, StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>> States { get; set; }

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
