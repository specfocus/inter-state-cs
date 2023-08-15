namespace XState
{
    public interface IInteropObservable<T>
    {
        IObservable<T> ToObservable();
    }

    public interface IInteropSubscribable<T>
    {
        IDisposable Subscribe(System.IObserver<T> observer);
    }

    public interface ISubscribable<T> : IInteropSubscribable<T>, IInteropObservable<T>
    {
        IDisposable Subscribe(Action<T> onNext, Action<Exception>? onError = null, Action? onComplete = null);
    }

    public class Subscribable<T> : Observable<T>, ISubscribable<T>
    {
        public new IDisposable Subscribe(Action<T> onNext, Action<Exception>? onError = null, Action? onComplete = null)
        {
            return Subscribe(new Observer<T>(onNext, onError, onComplete));
        }

        IObservable<T> IInteropObservable<T>.ToObservable()
        {
            return this;
        }
    }

    public class Observable<T> : IObservable<T>
    {
        private List<System.IObserver<T>> observers;

        public Observable()
        {
            observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        public IDisposable Subscribe(Action<T> onNext, Action<Exception>? onError = null, Action? onComplete = null)
        {
            return Subscribe(new Observer<T>(onNext, onError, onComplete));
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
