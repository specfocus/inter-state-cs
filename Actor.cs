using System;
using System.Collections.Generic;

namespace XState
{
    public interface Actor<TContext, TEvent> : IObservable<TContext>
    {
        string Id { get; }
        void Send(TEvent @event);
        void Stop();
        object ToJSON();
        InvokeDefinition<TContext, TEvent> Meta { get; }
        object State { get; set; }
        bool Deferred { get; }
    }



    public class Program
    {
        public static ActorRef<object, object> CreateNullActor(string id)
        {
            return new ActorRef<object, object>
            {
                Id = id,
                Send = @event => { },
                GetSnapshot = () => null,
                ToJSON = () => new { Id = id }
            };
        }

        public static ActorRef<object, object> CreateInvocableActor<TC, TE>(
            InvokeDefinition<TC, TE> invokeDefinition,
            StateMachine<TC, object, TE, object> machine,
            TC context,
            SCXML.Event<TE> _event
        )
        {
            var invokeSrc = ToInvokeSource(invokeDefinition.Src);
            var serviceCreator = machine?.Options?.Services?.GetValueOrDefault(invokeSrc.Type);
            var resolvedData = invokeDefinition.Data != null
                ? MapContext(invokeDefinition.Data, context, _event)
                : null;
            var tempActor = serviceCreator != null
                ? CreateDeferredActor((Spawnable)serviceCreator, invokeDefinition.Id, resolvedData)
                : CreateNullActor(invokeDefinition.Id);

            tempActor.Meta = invokeDefinition;

            return tempActor;
        }

        public static ActorRef<object, object> CreateDeferredActor(Spawnable entity, string id, object data = null)
        {
            var tempActor = CreateNullActor(id);
            tempActor.Deferred = true;

            if (entity is Machine<object, object, object, object> machine)
            {
                var initialState = tempActor.State = ServiceScope.Provide<object>(null,
                    () => (data != null ? machine.WithContext(data) : machine).InitialState);
                tempActor.GetSnapshot = () => initialState;
            }

            return tempActor;
        }

        public static InvokeSource ToInvokeSource(object src)
        {
            // Implement ToInvokeSource method
            return null;
        }

        public static object MapContext(object data, object context, object _event)
        {
            // Implement MapContext method
            return null;
        }
    }
}