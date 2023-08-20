namespace XState
{
    public class ContextProvider<TContext>
        where TContext : class
    {
        public static implicit operator ContextProvider<TContext>(TContext context) => new ContextProvider<TContext>(context);

        public static implicit operator ContextProvider<TContext>(Func<TContext> contextFunction) => new ContextProvider<TContext>(contextFunction);

        public static implicit operator TContext(ContextProvider<TContext> contextFactory) =>
            contextFactory._context ??
            contextFactory._contextFunction?.Invoke() ??
            throw new InvalidOperationException("ContextFactory has not been initialized.");

        private ContextProvider(TContext context) => _context = context;

        private ContextProvider(Func<TContext> contextFunction) => _contextFunction = contextFunction;

        private readonly TContext? _context;

        private readonly Func<TContext>? _contextFunction;
    }
}
