namespace XState
{
    public interface ActorContext<TEvent, TEmitted>
    {
        ActorRef<object, object> Parent { get; }

        ActorRef<TEvent, TEmitted> Self { get; }

        string Id { get; }

        HashSet<Observer<TEmitted>> Observers { get; }
    }
}
