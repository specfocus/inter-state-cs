namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        public StateValue Target
        {
            get
            {
                StateValue target = null;
                if (Type == "history")
                {
                    var historyConfig = Config as HistoryStateNodeConfig<TContext, TEvent>;
                    if (historyConfig.Target is string targetString)
                    {
                        target = IsStateId(targetString)
                            ? PathToStateValue(
                                Machine
                                    .GetStateNodeById(targetString)
                                    .Path.Skip(Path.Count - 1)
                                    .ToList()
                            )
                            : targetString;
                    }
                    else
                    {
                        target = historyConfig.Target;
                    }
                }

                return target;
            }
        }
    }
}
