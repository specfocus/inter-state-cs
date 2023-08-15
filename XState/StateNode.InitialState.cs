namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        /// <summary>
        /// The initial State instance, which includes all actions to be executed from entering the initial state.
        /// </summary>
        public State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> InitialState
        {
            get
            {
                var initialStateValue = InitialStateValue;

                if (initialStateValue == null)
                {
                    throw new Exception($"Cannot retrieve initial state from simple state '{Id}'.");
                }

                return GetInitialState(initialStateValue);
            }
        }
    }
}
