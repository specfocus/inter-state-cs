namespace XState
{
    public interface InteropObservable<T>
    {
        IObservable<T> ToObservable();
    }

    public interface InteropSubscribable<T>
    {
        IDisposable Subscribe(Observer<T> observer);
    }

    public interface Subscribable<T> : InteropSubscribable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);

        IDisposable Subscribe(Action<T> onNext, Action<Exception>? onError = null, Action? onComplete = null);
    }
}
