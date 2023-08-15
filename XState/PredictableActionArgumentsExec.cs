namespace XState
{
    using XState.Actions;

    public delegate void PredictableActionArgumentsExec(
        ActionObject<object, EventObject> action,
        object context,
        SCXML.Event<EventObject> _event
    );
}
