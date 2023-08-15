global using StateValueMap = System.Collections.Generic.Dictionary<string, XState.StateValue>;

namespace XState
{
    public sealed class StateValue
    {
        public static implicit operator StateValue(StateValueMap values) => new(values);


        public static implicit operator StateValue(string value) => new StateValue(value);


        public static implicit operator string?(StateValue value) => value.Value;


        public static implicit operator string[](StateValue value) => value._values.Keys.ToArray();


        public static implicit operator StateValueMap(StateValue value) => value._values;


        private readonly StateValueMap _values;

        private StateValue(string value) => _values = new StateValueMap { { value, value } };

        private StateValue(StateValueMap values) => _values = values;

        public string? Value => _values.Count == 1 ? _values.Keys.First() : null;

        public override string ToString() => Value ?? $"{_values?.Count} Values";
    }


}
