namespace XState
{
    internal static partial class Utils
    {
        /*
         export const symbolObservable: typeof Symbol.observable = (() =>
          (typeof Symbol === 'function' && Symbol.observable) ||
          '@@observable')() as any;
         */

        public static readonly string Observable = GetSymbolObservable();

        private static string GetSymbolObservable()
        {
            string symbol = "@@observable";
            if (typeof(Symbol) != null && typeof(Symbol).GetMethod("observable") != null)
            {
                symbol = ((Func<string>)(() => {
                    return Symbol.observable.ToString();
                }))();
            }
            return symbol;
        }
    }
}
