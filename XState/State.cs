namespace XState
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using XState.Actions;

    public class State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>
    {
        public StateValue Value { get; set; }

        public TContext Context { get; set; }

        public IHistoryValue HistoryValue { get; set; }

        public State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> History { get; set; }

        public List<ActionObject<TContext, TEvent>> Actions { get; set; }

        public ActivityMap Activities { get; set; } = EMPTY_ACTIVITY_MAP;

        public Dictionary<string, object> Meta { get; set; } = new Dictionary<string, object>();

        public List<TEvent> Events { get; set; } = new List<TEvent>();

        public TEvent Event { get; set; }

        public SCXML.Event<TEvent> _event { get; set; }

        public string _sessionid { get; set; }

        public bool Changed { get; set; }

        public bool Done { get; set; }

        public List<StateNode<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>> Configuration { get; set; }

        public List<string> NextEvents => NextEventsInternal();
        public List<TransitionDefinition<TContext, TEvent>> Transitions { get; set; }

        public Dictionary<string, ActorRef<object>> Children { get; set; }

        public HashSet<string> Tags { get; set; } = new HashSet<string>();

        public StateMachine<TContext, object, TEvent, TTypestate, BaseActionObject, object, TResolvedTypesMeta> Machine { get; set; }

        private List<string> NextEventsInternal()
        {
            return Configuration.SelectMany(node => node.NextEvents).ToList();
        }

        public bool Matches(object parentStateValue)
        {
            return MatchesState(parentStateValue, Value);
        }

        public bool HasTag(object tag)
        {
            return Tags.Contains(tag.ToString());
        }

        public bool Can(object @event)
        {
            if (Environment.IS_PRODUCTION)
            {
                Console.WriteLine(
                    !!Machine
                        ? "state.can(...) used outside of a machine-created State object; this will always return false."
                        : string.Empty
                );
            }

            var transitionData = Machine?.GetTransitionData(this, @event);

            return transitionData?.Transitions.Any(
                       t => t.Target != null || t.Actions.Count > 0
                   ) == true;
        }

        private bool MatchesState(object stateValue1, object stateValue2)
        {
            // Implement MatchesState logic
            return false;
        }

        private Dictionary<string, object> EMPTY_ACTIVITY_MAP = new Dictionary<string, object>();
    }
}
