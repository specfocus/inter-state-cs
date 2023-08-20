global using AnyStateNode = XState.StateNode<object, XState.IStateSchema<object>, XState.Event, XState.Typestate<object>, XState.ServiceMap, XState.TypegenFlag>;

namespace XState
{
    static partial class StateNodeExtensions
    {
        public static bool IsInFinalState(IEnumerable<AnyStateNode> configuration, AnyStateNode stateNode)
        {
            if (stateNode.Type == "compound")
            {
                return GetChildren(stateNode).Any(s => s.Type == "final" && configuration.Contains(s));
            }
            if (stateNode.Type == "parallel")
            {
                return GetChildren(stateNode).All(sn => IsInFinalState(configuration, sn));
            }

            return false;
        }
    }
}
