using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XState
{
    partial class StateNode<TContext, TStateSchema, TEvent, TTypestate, TServiceMap, TResolvedTypesMeta>
    {
        private StateTransition<TContext, TEvent> _Transition(
            StateValue stateValue,
            State<TContext, TEvent> state,
            SCXML.Event<TEvent> _event
        )
        {
            // leaf node
            if (stateValue is string stateValueString)
            {
                return TransitionLeafNode(stateValueString, state, _event);
            }

            // hierarchical node
            if (stateValue is StateValueObject stateValueObject && stateValueObject.Count == 1)
            {
                return TransitionCompoundNode(stateValueObject, state, _event);
            }

            // orthogonal node
            return TransitionParallelNode(stateValue, state, _event);
        }

    }
}
