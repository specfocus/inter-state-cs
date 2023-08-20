namespace XState
{
    public delegate void ContextListener<TContext>(
      TContext context,
      TContext? prevContext
    ) where TContext : DefaultContext;
}