namespace XState
{
    public interface IActorRef<TEvent, TEmitted> : ISubscribable<TEmitted>
        where TEvent : Event
    {
        string Id { get; }

        Action<TEvent> Send { get; }

        Action Stop { get; }

        Func<object> GetSnapshot { get; }

        Func<object> ToJSON { get; }
    }

    public class ActorRef<TEvent, TEmitted> : Subscribable<TEmitted>, IActorRef<TEvent, TEmitted>
        where TEvent : Event
    {
        public string Id { get; }

        public Action<TEvent> Send { get; set; }

        public Action Stop { get; set; }

        public Func<object> GetSnapshot { get; set; }

        public Func<object> ToJSON { get; set; }
    }
}
