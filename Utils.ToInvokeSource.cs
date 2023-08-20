namespace XState
{
    internal static partial class Utils
    {
        public static InvokeSourceDefinition ToInvokeSource(object src)
        {
            if (src is string type)
            {
                return new InvokeSourceDefinition(type);
            }

            return (InvokeSourceDefinition)src;
        }
    }
}
