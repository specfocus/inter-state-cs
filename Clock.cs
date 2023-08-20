namespace XState
{
    public interface IClock
    {
        void SetTimeout(Action fn, int timeout);

        void ClearTimeout();
    }

    public class Clock : IClock, IDisposable
    {
        private CancellationTokenSource? cancellationTokenSource;

        public void SetTimeout(Action action, int timeout)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            Task.Delay(timeout, cancellationToken)
                .ContinueWith(task =>
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        action.Invoke();
                    }
                });
        }

        public void ClearTimeout()
        {
            cancellationTokenSource?.Cancel();
        }

        public void Dispose()
        {
            ClearTimeout();
            cancellationTokenSource?.Dispose();
        }
    }
}
