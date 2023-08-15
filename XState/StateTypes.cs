namespace XState
{
    public static class StateTypes
    {
        public static readonly Lazy<StateType> After = new(() => "xstate.after");
        public static readonly Lazy<StateType> Assign = new(() => "xstate.assign");
        public static readonly Lazy<StateType> Cancel = new(() => "xstate.cancel");
        public static readonly Lazy<StateType> Choose = new(() => "xstate.choose");
        public static readonly Lazy<StateType> DoneInvoke = new(() => "done.invoke");
        public static readonly Lazy<StateType> DoneState = new(() => "done.state");
        public static readonly Lazy<StateType> ErrorCommunication = new(() => "error.communication");
        public static readonly Lazy<StateType> ErrorCustom = new(() => "xstate.error");
    }
}
