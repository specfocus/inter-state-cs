namespace XState
{
    internal static partial class Utils
    {
        public interface IPromise<T>
        {
            Func<Action<T>, Action<Exception>, Action> Then { get; }    
        }

        public static bool HasThenMethod(object obj)
        {
            var type = obj.GetType();
            var thenMethod = type.GetMethod("Then", new Type[0]);
            return thenMethod != null && thenMethod.ReturnType == type;
        }

        public static bool IsPromiseLike<T>(object value)
        {
            if (value is IPromise<T>)
            {
                return true;
            }

            // Check if shape matches the Promise/A+ specification for a "thenable".
            if (value != null && (value is Delegate || value is object) && HasThenMethod(value))
            {
                return true;
            }

            return false;
        }
    }
}
