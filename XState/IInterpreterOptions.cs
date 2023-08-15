namespace XState
{
    using AnyInterpreter = Interpreter<object, object, object, object, object>;

    public interface IInterpreterOptions
    {
        /// <summary>
        /// Whether state actions should be executed immediately upon transition. Defaults to <c>true</c>.
        /// </summary>
        bool Execute { get; }

        /// <summary>
        /// The custom <see cref="Clock"/> for controlling time-based behavior.
        /// </summary>
        IClock Clock { get; }

        /// <summary>
        /// Logger function for outputting information.
        /// </summary>
        Logger Logger { get; }

        /// <summary>
        /// The parent <see cref="AnyInterpreter"/> for nested interpreters.
        /// </summary>
        AnyInterpreter? Parent { get; }

        /// <summary>
        /// If <c>true</c>, defers processing of sent events until the service
        /// is initialized (<see cref="IService.Start"/>). Otherwise, an error will be thrown
        /// for events sent to an uninitialized service.
        /// 
        /// Default: <c>true</c>
        /// </summary>
        bool DeferEvents { get; }

        /// <summary>
        /// The custom ID for referencing this service.
        /// </summary>
        string? Id { get; }

        /// <summary>
        /// If <c>true</c>, states and events will be logged to Redux DevTools.
        /// 
        /// Default: <c>false</c>
        /// </summary>
        bool DevTools { get; }
    }

    public class InterpreterOptions : IInterpreterOptions
    {
        public bool Execute { get; set; } = true;

        public IClock Clock { get; set; } = new Clock();

        public Logger Logger { get; set; } = Console.WriteLine;

        public AnyInterpreter? Parent { get; set; }

        public bool DeferEvents { get; set; } = true;

        public string? Id { get; set; }

        public bool DevTools { get; set; } = false;
    }
}
