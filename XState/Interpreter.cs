using XState.State;

namespace XState
{
    using Dynamic;
    using AnyInterpreter = Interpreter<object, object, object, object, object>;

    public class Interpreter<TContext, TStateSchema, TEvent, TTypestate, TResolvedTypesMeta>
        : ActorRef<TEvent, State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>>
        where TContext : class
        where TStateSchema : StateSchema<TContext>
        where TEvent : Event
        where TTypestate : Typestate<TContext>
        where TResolvedTypesMeta : TypegenDisabled
    {
        /**
   * The default interpreter options:
   *
   * - `clock` uses the global `setTimeout` and `clearTimeout` functions
   * - `logger` uses the global `console.log()` method
   */
        public static IInterpreterOptions defaultOptions = DefaultInterpreterOptions.Instance;
        /**
         * The current state of the interpreted machine.
         */
        private State<
          TContext,
          TEvent,
          TStateSchema,
          TTypestate,
          TResolvedTypesMeta
        >? _state;
        private State<
          TContext,
          TEvent,
          TStateSchema,
          TTypestate,
          TResolvedTypesMeta
        >? _initialState;
        /**
         * The clock that is responsible for setting and clearing timeouts, such as delayed events and transitions.
         */
        public IClock clock;
        public readonly IInterpreterOptions options;

        private Scheduler scheduler;
        private Record delayedEventsMap = new() { };
        private HashSet<
            StateListener<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta>
          > listeners = new();
        private HashSet<ContextListener<TContext>> contextListeners = new();
        private HashSet<Listener> stopListeners = new();
        private HashSet<EventListener<TEvent>> doneListeners = new();
        private HashSet<EventListener<TEvent>> eventListeners = new();
        private HashSet<EventListener<TEvent>> sendListeners = new();
        private Logger logger;
        /**
         * Whether the service is started.
         */
        public bool initialized = false;
        public InterpreterStatus status = InterpreterStatus.NotStarted;

        // Actor
        public AnyInterpreter? parent;
        public string? id;

        /**
         * The globally unique process ID for this invocation.
         */
        public string sessionId;
        public Dictionary<string | number, ActorRef<object>> children = new();
        private HashSet<string> forwardTo = new();

        private Array<[{ send: (ev: unknown) => void }, unknown]> _outgoingQueue = new();

        // Dev Tools
        private object? devTools;
        private DoneEvent? _doneEvent;

        public StateMachine<
            TContext,
          TStateSchema,
          TEvent,
          TTypestate,
          object,
          object,
          TResolvedTypesMeta
        > machine;

        /**
         * Creates a new Interpreter instance (i.e., service) for the given machine with the provided options, if any.
         *
         * @param machine The machine to be interpreted
         * @param options Interpreter options
         */
        public Interpreter(
            StateMachine<
              TContext,
              TStateSchema,
              TEvent,
              TTypestate,
              object,
              object,
              TResolvedTypesMeta
            > machine,
            IInterpreterOptions? options
          )
        {
            this.machine = machine;
            this.options = Utils.Merge(new InterpreterOptions(), options);

            clock = this.options.Clock;
            logger = this.options.Logger;
            parent = this.options.Parent;
            id = this.options.Id ?? machine.Id;

            scheduler = new Scheduler(new()
            {
                deferEvents = this.options.deferEvents
            });

            sessionId = registry.bookId();
        }
    }
}
