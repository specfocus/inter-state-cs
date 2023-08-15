namespace XState
{
    /**
     * The full definition of an event, with a string `type`.
     */
    public interface Event
    {
        /**
         * The type of event that is sent.
         */
        string Type { get; }
    }
}
