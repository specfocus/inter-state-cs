using XState.State.Actions;

namespace XState
{
    public class ActorRef<TContext, TEvent> : IObserver<TContext>
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
