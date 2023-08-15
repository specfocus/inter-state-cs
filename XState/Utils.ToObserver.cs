namespace XState
{
    internal static partial class Utils
    {
        /*
         export function toObserver<T>(
  nextHandler?: Partial<Observer<T>> | ((value: T) => void),
  errorHandler?: (error: any) => void,
  completionHandler?: () => void
): Observer<T> {
  const noop = () => {};
  const isObserver = typeof nextHandler === 'object';
  const self = isObserver ? nextHandler : null;

  return {
    next: ((isObserver ? nextHandler.next : nextHandler) || noop).bind(self),
    error: ((isObserver ? nextHandler.error : errorHandler) || noop).bind(self),
    complete: (
      (isObserver ? nextHandler.complete : completionHandler) || noop
    ).bind(self)
  };
}
        */
        public static IObserver<T> ToObserver<T>(
            Action<T>? nextHandler = null,
            Action<Exception>? errorHandler = null,
            Action? completionHandler = null
        )
        {
            Action noop = () => { };
            bool isObserver = nextHandler != null && errorHandler != null && completionHandler != null;
            object self = isObserver ? nextHandler : null;

            return new Observer<T>(
                (isObserver ? nextHandler : (Action<T>)((value) => nextHandler?.Invoke(value) ?? noop)).Bind(self),
                (isObserver ? errorHandler : (Action<Exception>)((error) => errorHandler?.Invoke(error) ?? noop)).Bind(self),
                (isObserver ? completionHandler : (Action)(() => completionHandler?.Invoke() ?? noop)).Bind(self));
        }
    }
}
