using XState.State.Actions;

namespace XState
{
    public interface IMachineOptions<TContext, TEvent>
    {
        Dictionary<string, Action> Actions { get; }

        Dictionary<string, Func<bool>> Guards { get; }

        Dictionary<string, Func<TContext, object>> Services { get; }

        Dictionary<string, Action<TContext>> Activities { get; }

        Dictionary<string, int> Delays { get; }
    }

    public class MachineOptions<TContext, TEvent> : IMachineOptions<TContext, TEvent>
    {
        public Dictionary<string, Action> Actions { get; set; } = new();

        public Dictionary<string, Func<bool>> Guards { get; set; } = new();

        public Dictionary<string, Func<TContext, object>> Services { get; set; } = new();

        public Dictionary<string, Action<TContext>> Activities { get; set; } = new();

        public Dictionary<string, int> Delays { get; set; } = new();
    }
}
