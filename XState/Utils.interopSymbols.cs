namespace XState
{
    internal static partial class Utils
    {
        // TODO: to be removed in v5, left it out just to minimize the scope of the change and maintain compatibility with older versions of integration paackages
        /*
        export const interopSymbols = {
  [symbolObservable]: function()
        {
            return this;
        },
  [Symbol.observable]: function()
        {
            return this;
        }
    };*/
        public static Dictionary<string, Func<object, object>> Symbols = InitializeSymbols();

        private static Dictionary<string, Func<object, object>> InitializeSymbols()
        {
            var symbols = new Dictionary<string, Func<object, object>>
        {
            { SymbolObservable.Observable, (obj) => obj },
            { "@@observable", (obj) => obj }
        };

            return symbols;
        }

    }
}
