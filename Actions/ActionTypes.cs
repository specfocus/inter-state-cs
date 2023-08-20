namespace XState.Actions
{
    public static class ActionTypes
    {
        public static readonly Lazy<ActionType> After = new(() => "xstate.after");
        public static readonly Lazy<ActionType> Assign = new(() => "xstate.assign");
        public static readonly Lazy<ActionType> Cancel = new(() => "xstate.cancel");
        public static readonly Lazy<ActionType> Choose = new(() => "xstate.choose");
        public static readonly Lazy<ActionType> DoneInvoke = new(() => "done.invoke");
        public static readonly Lazy<ActionType> DoneState = new(() => "done.state");
        public static readonly Lazy<ActionType> ErrorCommunication = new(() => "error.communication");
        public static readonly Lazy<ActionType> ErrorCustom = new(() => "xstate.error");
        public static readonly Lazy<ActionType> ErrorExecution = new(() => "error.execution");
        public static readonly Lazy<ActionType> ErrorPlatform = new(() => "error.platform");
        public static readonly Lazy<ActionType> Init = new(() => "xstate.init");
        public static readonly Lazy<ActionType> Invoke = new(() => "xstate.invoke");
        public static readonly Lazy<ActionType> Log = new(() => "xstate.log");
        public static readonly Lazy<ActionType> NullEvent = new(() => "");
        public static readonly Lazy<ActionType> Pure = new(() => "xstate.pure");
        public static readonly Lazy<ActionType> Raise = new(() => "xstate.raise");
        public static readonly Lazy<ActionType> Send = new(() => "xstate.send");
        public static readonly Lazy<ActionType> Start = new(() => "xstate.start");
        public static readonly Lazy<ActionType> Stop = new(() => "xstate.stop");
        public static readonly Lazy<ActionType> Update = new(() => "xstate.update");
    }
}
