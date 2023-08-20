namespace XState.Actions
{
    public class ActionType
    {
        public static implicit operator ActionType(string value) => new(value);


        public static implicit operator string(ActionType type) => new(type.Value);

        public ActionType(string value) => Value = value;

        public string Value { get; }

        public override string ToString() => Value;

        public override bool Equals(object? obj) => obj is ActionType type && Value == type.Value || obj is string str && Value == str;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
