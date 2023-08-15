namespace XState
{
    public interface IDoneInvokeEvent<TData> : Event
    {
        TData Data { get; }
    }

    public class DoneInvokeEvent<TData> : EventObject, IDoneInvokeEvent<TData>
    {
        public DoneInvokeEvent(string type) : base(type) { }

        public TData Data { get; set; }
    }
}
