namespace XState
{
    public interface Behavior<TEvent, TEmitted>
    {
        Func<TEmitted, TEvent, ActorContext<TEvent, TEmitted>, TEmitted> Transition { get; }
        TEmitted InitialState { get; }
        Func<ActorContext<TEvent, TEmitted>, TEmitted> Start { get; }
    }
}
