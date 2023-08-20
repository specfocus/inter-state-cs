namespace XState
{
    internal static partial class Utils
    {
        public static bool EvaluateGuard<TContext, TEvent>(
            StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, object> machine,
            Guard<TContext, TEvent> guard,
            TContext context,
            SCXML.Event<TEvent> @event,
            State<TContext, TEvent> state
        )
            where TContext : class
            where TEvent : Event
        {
            var guards = machine.options?.guards;
            dynamic guardMeta = new GuardMeta<TContext, TEvent>();
            {
                guardMeta.State = state;
                guardMeta.Cond = guard;
                guardMeta._Event = @event;
            };

            if (guard.Type == Constants.DEFAULT_GUARD_TYPE)
            {
                var guardFn = guards?.ContainsKey(guard.Name) == true
                    ? guards[guard.Name]
                    : guard.Predicate;

                return guardFn(context, @event.Data, guardMeta);
            }

            var condFn = guards?.ContainsKey(guard.Type) == true
                ? guards[guard.Type]
                : throw new Exception($"Guard '{guard.Type}' is not implemented on machine '{machine.Id}'.");

            return condFn(context, @event.Data, guardMeta);
        }
    }
}
