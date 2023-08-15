namespace XState
{
    internal static partial class Utils
    {
        private static int currentId = 0;

        public static Func<string> UniqueId = () =>
        {
            currentId++;
            return currentId.ToString("X");
        };
    }
}
