﻿namespace XState.Actions
{
    public interface ISendAction<TContext, TEvent, TSentEvent>
        : IAction<TContext, TEvent, TSentEvent>
        where TContext : class
        where TSentEvent : Event
        where TEvent : Event
    {
        string To { get; set; } // You might need to define ActorRef and ExprWithMeta

        TSentEvent Event { get; set; }

        Delay<TContext, TEvent> Delay { get; set; } // You might need to define DelayExpr

        string Id { get; set; }
    }

    public class SendAction<TContext, TEvent, TSentEvent> : Action<TContext, TEvent, TSentEvent>, ISendAction<TContext, TEvent, TSentEvent>
        where TContext : class
        where TSentEvent : Event
        where TEvent : Event
    {
        public override ActionType Type => ActionTypes.Send.Value;

        public string To { get; set; }

        public TSentEvent Event { get; set; }

        public Delay<TContext, TEvent> Delay { get; set; }

        public string Id { get; set; }
    }

    public class SendActionObject<TContext, TEvent, TSentEvent> : SendAction<TContext, TEvent, TSentEvent>
        where TContext : class
        where TEvent : Event
        where TSentEvent : Event
    {
        public SCXML.Event<TSentEvent> _Event { get; set; }
    }
}
