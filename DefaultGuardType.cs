namespace XState
{
    using XState.Extensions;

    public class DefaultGuardType
    {
        public static implicit operator DefaultGuardType(string value)
        {
            return new DefaultGuardType(value);
        }

        public static implicit operator string(DefaultGuardType guard)
        {
            return guard.Value;
        }

        public DefaultGuardType(string value) => Value = value;

        public string Value { get; }


        public static DefaultGuardType xstate_guard = new("xstate.guard");

        public override bool Equals(object? obj)
        {
            if (base.Equals(obj))
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() == typeof(string))
            {
                return obj.Equals(Value);
            }

            if (obj.GetType() == typeof(DefaultGuardType))
            {
                return obj.GetFieldValue<string>("Value")?.Equals(Value) ?? false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString() => Value;
    }
}
