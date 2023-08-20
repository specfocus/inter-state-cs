using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XState.Actions;

namespace XState
{
    internal class Invoke
    {
        public static InvokeDefinition<TContext, TEvent> ToInvokeDefinition<TContext, TEvent>(
            IInvokeConfig<TContext, TEvent> invokeConfig
        )
            where TContext : class
            where TEvent : Event
        {
            return new InvokeDefinition<TContext, TEvent>(ToInvokeSource(invokeConfig.Src))
            {
                Type = ActionTypes.Invoke,
                Id = invokeConfig.Id,
                Src = ToInvokeSource(invokeConfig.Src),
                /* ... Other properties from invokeConfig ... */
                ToJSON = () =>
                {
                    var invokableProperties = new
                    {
                        invokeConfig.id,
                        invokeConfig.src
                        /* ... Other properties from invokeConfig that you want to include ... */
                    };

                    return new
                    {
                        type = actionTypes.invoke,
                        src = ToInvokeSource(invokeConfig.src),
                        /* ... Other properties from invokeConfig that you want to include ... */
                    };
                }
            };
        }
    }
}
