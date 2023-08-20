namespace XState
{
    using XState.Dynamic;

    /**
     * The full definition of an event, with a string `type`.
     */
    public class AnyEventObject : Record, EventObject
    {
        public AnyEventObject(string type) => Type = type;

        /**
         * The type of event that is sent.
         */
        public string Type { get; }
    }
}
