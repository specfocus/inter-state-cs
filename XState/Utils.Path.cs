using System.Linq.Expressions;

namespace XState
{
    internal static partial class Utils
    {
        /// <summary>
        /// Retrieves a value at the given path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="props">The deep path to the prop of the desired value</param>
        /// <returns></returns>
        public static Func<T, object> Path<T>(string[] props)
        {
            ParameterExpression paramExpr = Expression.Parameter(typeof(T), "object");

            Expression propAccessExpr = props.Aggregate(
                (Expression)paramExpr,
                (current, prop) => Expression.PropertyOrField(current, prop)
            );

            Expression<Func<T, object>> lambda = Expression.Lambda<Func<T, object>>(
                Expression.Convert(propAccessExpr, typeof(object)),
                paramExpr
            );

            return lambda.Compile();
        }
    }
}
