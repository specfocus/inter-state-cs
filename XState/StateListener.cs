namespace XState
{
    public delegate void StateListener<
      TContext,
      TEvent,
      TStateSchema,
      TTypestate,
      TResolvedTypesMeta
    >(
        State<TContext, TEvent, TStateSchema, TTypestate, TResolvedTypesMeta> state,
        TEvent @event
    )
    where TEvent : Event
    where TStateSchema : StateSchema<TContext>
    where TTypestate : Typestate<TContext>
    where TResolvedTypesMeta : TypegenDisabled;
}
