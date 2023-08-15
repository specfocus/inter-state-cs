namespace XState
{
    using System.Reflection;

    internal static partial class Utils
    {
        public static T Assign<T>(T target, params T?[] sources) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (T? obj in sources)
            {
                if (obj == null) continue;

                foreach (PropertyInfo property in properties)
                {
                    property.SetValue(target, property.GetValue(obj));
                }
            }
            return target;
        }
    }
}
