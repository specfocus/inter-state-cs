namespace XState
{
    public class SCXML
    {
        public interface IEvent<TEvent>
            where TEvent : Event
        {
            /**
             * This field contains whatever data the sending entity chose to include in this event.
             * The receiving SCXML Processor should reformat this data to match its data model,
             * but must not otherwise modify it.
             *
             * If the conversion is not possible, the Processor must leave the field blank
             * and must place an error 'error.execution' in the internal event queue.
             */
            TEvent Data { get; }

            /**
             * This is a character string giving the name of the event.
             * The SCXML Processor must set the name field to the name of this event.
             * It is what is matched against the 'event' attribute of <transition>.
             * Note that transitions can do additional tests by using the value of this field
             * inside boolean expressions in the 'cond' attribute.
             */
            string Name { get; }

            /**
             * This field describes the event type.
             * The SCXML Processor must set it to: "platform" (for events raised by the platform itself, such as error events),
             * "internal" (for events raised by <raise> and <send> with target '_internal')
             * or "external" (for all other events).
             */
            EventType Type { get; }

            /**
             * If the sending entity has specified a value for this, the Processor must set this field to that value
             * (see C Event I/O Processors for details).
             * Otherwise, in the case of error events triggered by a failed attempt to send an event,
             * the Processor must set this field to the send id of the triggering <send> element.
             * Otherwise it must leave it blank.
             */
            string? SendId { get; }

            /**
             * This is a URI, equivalent to the 'target' attribute on the <send> element.
             * For external events, the SCXML Processor should set this field to a value which,
             * when used as the value of 'target', will allow the receiver of the event to <send>
             * a response back to the originating entity via the Event I/O Processor specified in 'origintype'.
             * For internal and platform events, the Processor must leave this field blank.
             */
            string? Origin { get; }

            /**
             * This is equivalent to the 'type' field on the <send> element.
             * For external events, the SCXML Processor should set this field to a value which,
             * when used as the value of 'type', will allow the receiver of the event to <send>
             * a response back to the originating entity at the URI specified by 'origin'.
             * For internal and platform events, the Processor must leave this field blank.
             */
            string? OriginType { get; }

            /**
             * If this event is generated from an invoked child process, the SCXML Processor
             * must set this field to the invoke id of the invocation that triggered the child process.
             * Otherwise it must leave it blank.
             */
            string? InvokeId { get; }

        }

        public class Event<TEvent> : IEvent<TEvent>
            where TEvent : Event
        {
            public static implicit operator Event<TEvent>(string type) => new() { Type = type };

            public static implicit operator string(Event<TEvent> @event) => @event.Type;

            public TEvent Data { get; set; }
            public string Name { get; set; }
            public EventType Type { get; set; }
            public string? SendId { get; set; }
            public string? Origin { get; set; }
            public string? OriginType { get; set; }
            public string? InvokeId { get; set; }
        }

        public class EventType
        {
            public static implicit operator EventType(string value) => new(value);


            public static implicit operator string(EventType eventType) => eventType.Value;

            public EventType(string value) => Value = value;

            public string Value { get; }

            public override bool Equals(object? obj) => obj is EventType eventType && Value == eventType.Value || obj is string str && Value == str;

            public override int GetHashCode() => Value.GetHashCode();

            public override string ToString() => Value;
        }

        public static class EventTypes
        {
            public static readonly Lazy<EventType> Platform = new(() => "platform");
            public static readonly Lazy<EventType> Internal = new(() => "internal");
            public static readonly Lazy<EventType> External = new(() => "external");
        }
    }
}
