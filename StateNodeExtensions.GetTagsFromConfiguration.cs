namespace XState
{
    static partial class StateNodeExtensions
    {
        public static HashSet<string> GetTagsFromConfiguration(
            this IEnumerable<StateNode<object, IStateSchema<object>, Event, Typestate<object>, ServiceMap, TypegenFlag>> configuration
        )
        {
            return new HashSet<string>(configuration.SelectMany(sn => sn.Tags));
        }
    }
}
