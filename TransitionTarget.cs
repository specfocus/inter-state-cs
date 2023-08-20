namespace XState
{
    /*
     export type TransitionTarget<
  TContext,
  TEvent extends EventObject
> = SingleOrArray<string | StateNode<TContext, any, TEvent>>;
     */
    public class TransitionTarget<TContext, TEvent> : List<StateNode<TContext, IStateSchema<TContext>, TEvent, Typestate<TContext>, ServiceMap, object>>
        where TContext : class
        where TEvent : Event
    {
    }

    /*
    export type TransitionTargets<TContext> = Array<
  string | StateNode<TContext, any>
    >;
    */

    public class TransitionTargets<TContext> : List<StateNode<TContext, IStateSchema<TContext>, EventObject, Typestate<TContext>, ServiceMap, object>>
        where TContext : class
    {
    }
}
