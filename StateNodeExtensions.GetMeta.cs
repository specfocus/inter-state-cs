namespace XState
{
    static partial class StateNodeExtensions
    {
        private static Dictionary<string, object> GetMeta(
            IEnumerable<StateNode<object, IStateSchema<object>, Event, Typestate<object>, ServiceMap, TypegenFlag>> configuration
        )
        {
            return configuration
                .Where(stateNode => stateNode.Meta != null)
                .ToDictionary(stateNode => stateNode.Id, stateNode => stateNode.Meta);
        }
    }
}
