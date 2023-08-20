using Newtonsoft.Json.Linq;

namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public JObject ToJson()
        {
            return JObject.FromObject(Definition);
        }
    }
}
