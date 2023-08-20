namespace XState
{
    internal static partial class Utils
    {
        public static TEvent ToEventObject<TEvent>(SCXML.Event<TEvent> eventValue, EventData payload = null)
            where TEvent : Event
        {
            if (eventValue is string || eventValue is int)
            {
                var eventType = eventValue.ToString();
                var eventData = new Dictionary<string, object>(payload ?? new EventData());
                eventData["type"] = eventType;
                return (TEvent)Convert.ChangeType(eventData, typeof(TEvent));
            }

            return eventValue;
        }
    }
}
