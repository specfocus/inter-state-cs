namespace XState
{
    // Use the interface Event instead of the class EventObject
    public class EventObject : Event
    {
        public EventObject(string type) => Type = type;

        public string Type { get; }

        public override bool Equals(object obj) => obj is Event other && Type == other.Type;

        public override int GetHashCode() => Type.GetHashCode();

        public override string ToString() => Type;
    }
}
