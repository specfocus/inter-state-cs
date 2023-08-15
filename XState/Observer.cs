namespace XState
{
    public class Observer<T> : System.IObserver<T>
    {
        private readonly Action<T> _onNext;
        private readonly Action<Exception> _onError;
        private readonly Action _onCompleted;

        public Observer(Action<T> onNext, Action<Exception>? onError = null, Action? onCompleted = null)
        {
            _onNext = onNext ?? throw new ArgumentNullException(nameof(onNext));
            _onError = onError ?? (_ => { });
            _onCompleted = onCompleted ?? (() => { });
        }

        public void OnNext(T value) => _onNext(value);
        public void OnError(Exception error) => _onError(error);
        public void OnCompleted() => _onCompleted();
    }
}
