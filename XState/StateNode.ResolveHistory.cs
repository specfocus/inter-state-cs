using System.Xml.Linq;

namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Resolves to the historical value(s) of the parent state node, represented by state nodes.
        /// </summary>
        /// <param name="historyValue"></param>
        /// <returns></returns>
        private List<StateNode<TContext, dynamic, TEvent, dynamic, dynamic, dynamic>> ResolveHistory(HistoryValue historyValue = null)
        {
            if (Type != "history")
            {
                return new List<StateNode<TContext, dynamic, TEvent, dynamic, dynamic, dynamic>> { this };
            }

            var parent = Parent;

            if (historyValue == null)
            {
                var historyTarget = Target;
                return historyTarget != null
                    ? historyTarget.SelectMany(relativeChildPath => parent.GetFromRelativePath(relativeChildPath))
                    : parent.InitialStateNodes;
            }

            var subHistoryValue = NestedPath<HistoryValue>(
                parent.Path,
                "states"
            )(historyValue).Current;

            if (subHistoryValue is string strValue)
            {
                return new List<StateNode<TContext, dynamic, TEvent, dynamic, dynamic, dynamic>> { parent.GetStateNode(strValue) };
            }

            return toStatePaths(subHistoryValue!).SelectMany(subStatePath =>
                History == "deep"
                    ? parent.GetFromRelativePath(subStatePath)
                    : new List<StateNode<TContext, dynamic, TEvent, dynamic, dynamic, dynamic>> { parent.States[subStatePath[0]] }
            ).ToList();
        }
    }
}
