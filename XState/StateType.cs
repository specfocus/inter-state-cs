namespace XState
{
    public class StateType
    {
        public static implicit operator StateType(string value) => new(value);


        public static implicit operator string(StateType type) => new(type.Value);

        public StateType(string value) => Value = value;

        public string Value { get; }

        public override string ToString() => Value;

        public override bool Equals(object? obj) => obj is StateType type && Value == type.Value || obj is string str && Value == str;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
