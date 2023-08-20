namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private List<StateNode<TContext, dynamic, TEvent>> ResolveTarget(List<string> _target)
        {
            if (_target == null)
            {
                // An undefined target signals that the state node should not transition from that state when receiving that event
                return null;
            }

            return _target.Select(target =>
            {
                if (!Utils.IsString(target))
                {
                    return target;
                }

                var isInternalTarget = target[0] == this.Delimiter;

                // If internal target is defined on machine,
                // do not include machine key on target
                if (isInternalTarget && this.Parent == null)
                {
                    return this.GetStateNodeByPath(target.Substring(1));
                }

                var resolvedTarget = isInternalTarget ? this.Key + target : target;

                if (this.Parent != null)
                {
                    try
                    {
                        var targetStateNode = this.Parent.GetStateNodeByPath(resolvedTarget);
                        return targetStateNode;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Invalid transition definition for state node '{this.Id}':\n{ex.Message}");
                    }
                }
                else
                {
                    return this.GetStateNodeByPath(resolvedTarget);
                }
            }).ToList();
        }
    }
}
