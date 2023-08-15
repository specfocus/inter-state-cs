namespace XState
{
    public interface IActorRef<TContext, TEvent> : IObserver<TContext>
        where TContext : class
        where TEvent : Event
    {
        string Id { get; }

        Action<TEvent> Send { get; }

        Action Stop { get; }

        Func<object> GetSnapshot { get; }

        Func<object> ToJSON { get; }
    }

    public class ActorRef<TContext, TEvent> : IActorRef<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public string Id { get; }

        public Action<TEvent> Send { get; set; }

        public Action Stop { get; set; }

        public Func<object> GetSnapshot { get; set; }

        public Func<object> ToJSON { get; set; }

        void IObserver<TContext>.OnCompleted()
        {
            throw new NotImplementedException();
        }

        void IObserver<TContext>.OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        void IObserver<TContext>.OnNext(TContext value)
        {
            throw new NotImplementedException();
        }
    }
}
