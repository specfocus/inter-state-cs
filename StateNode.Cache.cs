using System.Collections.Generic;

namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Private cache for internal state
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TStateSchema"></typeparam>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TTypestate"></typeparam>
        /// <typeparam name="TServiceMap"></typeparam>
        /// <typeparam name="TResolvedTypesMeta"></typeparam>
        internal class Cache<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
            where TContext : class
            where TStateSchema : IStateSchema<TContext>
            where TEvent : Event
            where TTypestate : Typestate<TContext>
            where TServiceMap : ServiceMap
            where TResolvedTypesMeta : TypegenFlag // default to TypegenDisabled
        {
            public List<string> Events { get; set; }

            public Dictionary<StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>, StateValue> RelativeValue { get; set; }

            public StateValue InitialStateValue { get; set; }

            public State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> InitialState { get; set; }

            public TransitionDefinitionMap<TContext, TEvent> On { get; set; }

            public List<TransitionDefinition<TContext, TEvent>> Transitions { get; set; }

            public Dictionary<string, List<TransitionDefinition<TContext, TEvent>>> Candidates { get; set; }

            public List<DelayedTransitionDefinition<TContext, TEvent>> DelayedTransitions { get; set; }
        }

        private Cache<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> __cache = new();
    }
}
