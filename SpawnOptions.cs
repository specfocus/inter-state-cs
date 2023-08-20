namespace XState
{
    public interface SpawnOptions
    {
        bool? AutoForward { get; }

        string? Name { get; }

        bool? Sync { get; }
    }

    sealed class DEFAULT_SPAWN_OPTIONS : SpawnOptions
    {
        private static readonly Lazy<DEFAULT_SPAWN_OPTIONS> instance = new(() => new DEFAULT_SPAWN_OPTIONS());

        public static DEFAULT_SPAWN_OPTIONS Instance => instance.Value;

        private DEFAULT_SPAWN_OPTIONS()
        {
        }

        bool? SpawnOptions.AutoForward => false;

        string? SpawnOptions.Name => null;

        bool? SpawnOptions.Sync => false;
    }
}
