namespace XState
{
    internal static partial class Utils
    {
        public static SCXML.Event<TEvent> ToSCXMLEvent<TEvent>(SCXML.Event<TEvent> eventValue, SCXML.Event<TEvent>? scxmlEvent = null)
            where TEvent : Event
        {
            if (eventValue is SCXML.Event<TEvent> _event)
            {
                return _event;
            }

            if (eventValue is string str)
            {
                return str;
            }

            var eventObject = ToEventObject((SCXML.Event<TEvent>)eventValue);

            dynamic @event = new SCXML.Event<TEvent>();
            {
                @event.Name = eventObject.Type;
                @event.Data = eventObject;
                @event.__Type = "scxml";
                @event.Type = "external";
                // ...scxmlEvent
            };
            return @event;
        }
    }
}
