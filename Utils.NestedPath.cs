using System.IO;

namespace XState
{
    internal static partial class Utils
    {

        /// <summary>
        /// Retrieves a value at the given path via the nested accessor prop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="props">The deep path to the prop of the desired value</param>
        /// <param name="accessorProp"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Func<T, T> NestedPath<T>(string[] props, string accessorProp)
        {
            return (objectToAccess) =>
            {
                var result = objectToAccess;

                foreach (var prop in props)
                {
                    // Use reflection to access the nested property
                    var accessorPropInfo = typeof(T).GetProperty(accessorProp);
                    if (accessorPropInfo != null)
                    {
                        result = (T)accessorPropInfo.GetValue(result);
                        var propInfo = typeof(T).GetProperty(prop);
                        if (propInfo != null)
                        {
                            result = (T)propInfo.GetValue(result);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Property {prop} not found");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Accessor property {accessorProp} not found");
                    }
                }

                return result;
            };
        }
    }
}
