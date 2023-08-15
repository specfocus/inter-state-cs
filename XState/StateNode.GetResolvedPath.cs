namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private List<string> GetResolvedPath(string stateIdentifier)
        {
            if (IsStateId(stateIdentifier))
            {
                var stateNode = this.Machine.IdMap[stateIdentifier.Substring(Constants.STATE_IDENTIFIER.Length)];

                if (stateNode == null)
                {
                    throw new Exception($"Unable to find state node '{stateIdentifier}'");
                }

                return stateNode.Path;
            }

            return ToStatePath(stateIdentifier, this.Delimiter);
        }
    }
}
