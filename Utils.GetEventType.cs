namespace XState
{
    internal static partial class Utils
    {
        public static string GetEventType<TEvent>(SCXML.Event<TEvent> eventObj) where TEvent : EventObject
        {
            try
            {
                if (eventObj is string || eventObj is int)
                {
                    return eventObj.ToString();
                }
                else
                {
                    return eventObj.Type;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Events must be strings or objects with a string event.type property.", e);
            }
        }
    }
}
