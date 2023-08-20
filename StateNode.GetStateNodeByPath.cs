using System.Xml.Linq;

namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// Returns the relative state node from the given `statePath`, or throws.
        /// </summary>
        /// <param name="statePath">The string or string array relative path to the state node.</param>
        /// <returns></returns>
        public StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta> GetStateNodeByPath(
            string statePath
        )
        {
            if (statePath.StartsWith("#") && IsStateId(statePath))
            {
                try
                {
                    return GetStateNodeById(statePath.Substring(1));
                }
                catch (Exception e)
                {
                    // try individual paths
                    // throw e;
                }
            }

            var arrayStatePath = ToStatePath(statePath, Delimiter).ToList();
            var currentStateNode = this;
            while (arrayStatePath.Count > 0)
            {
                var key = arrayStatePath.First();
                arrayStatePath.RemoveAt(0);

                if (key.Length == 0)
                {
                    break;
                }

                currentStateNode = currentStateNode.GetStateNode(key);
            }

            return currentStateNode;
        }
    }
}
