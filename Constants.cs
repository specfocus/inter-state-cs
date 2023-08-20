namespace XState
{
    internal static class Constants
    {
        public const string STATE_DELIMITER = ".";
        public static readonly ActivityMap EMPTY_ACTIVITY_MAP = new ActivityMap();
        public static readonly DefaultGuardType DEFAULT_GUARD_TYPE = DefaultGuardType.xstate_guard;
        public const string TARGETLESS_KEY = "";

        public const string NULL_EVENT = "";
        public const string STATE_IDENTIFIER = "#";
        public const string WILDCARD = "*";

        public static readonly dynamic EMPTY_OBJECT = new object();
    }
}
