namespace XState
{
    public class InvokeSourceDefinition : Dynamic.Record
    {
        public static implicit operator string(InvokeSourceDefinition definition) => definition.Type;

        public static implicit operator InvokeSourceDefinition(string type) => new(type);

        public static implicit operator InvokeSourceDefinition(AnyStateMachine machine) => new(machine);

        public InvokeSourceDefinition(string type) => Type = type;

        public string Id { get; }

        public string Type { get; }

        public override bool Equals(object? obj) => obj is InvokeSourceDefinition definition && Type == definition.Type || obj is string str && Type == str;

        public override int GetHashCode() => Type.GetHashCode();

        public override string ToString() => Type;
    }
}
