using XState.State.Actions;

namespace XState
{

    public static class StateMachineUtility
    {
        public const string NULL_EVENT = "";
        public const char STATE_IDENTIFIER = "#";
        public const char WILDCARD = "*";

        public static readonly Dictionary<string, object> EMPTY_OBJECT = new Dictionary<string, object>();

        public static bool IsStateId(string str) => str[0] == STATE_IDENTIFIER;

        public static MachineOptions<TContext, TEvent> CreateDefaultOptions<TContext, TEvent>()
        {
            return new MachineOptions<TContext, TEvent>
            {
                Actions = new Dictionary<string, Action>(),
                Guards = new Dictionary<string, Func<bool>>(),
                Services = new Dictionary<string, Func<TContext, object>>(),
                Activities = new Dictionary<string, Action<TContext>>(),
                Delays = new Dictionary<string, int>()
            };
        }

        public static void ValidateArrayifiedTransitions<TContext, TEvent>(
            StateNode<TContext, TEvent> stateNode,
            string @event,
            List<TransitionConfig<TContext, EventObject>> transitions)
        {
            bool hasNonLastUnguardedTarget = transitions
                .GetRange(0, transitions.Count - 1)
                .Any(transition =>
                    !transition.Conditions.ContainsKey("cond") &&
                    !transition.Conditions.ContainsKey("in") &&
                    (transition.Target is string || transition.Target is StateNode_<TContext, TEvent>)
                );

            string eventText = @event == NULL_EVENT ? "the transient event" : $"event "{ @event}
            "";

            if (hasNonLastUnguardedTarget)
            {
                Console.WriteLine(
                    $"One or more transitions for {eventText} on state "{ stateNode.Id}
                " are unreachable. " +
                    "Make sure that the default transition is the last one defined."
                );
            }
        }

        private static void Warn(bool condition, string message)
        {
            if (condition)
            {
                Console.WriteLine(message);
            }
        }
    }
}