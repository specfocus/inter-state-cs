namespace XState
{
    using XState.Actions;

    public interface IActivity<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        string Id { get; }

        ActionType Type { get; }
    }

    public abstract class Activity<TContext, TEvent> : ActionObject<TContext, TEvent, TEvent, BaseActionObject>, IActivity<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public static implicit operator string(Activity<TContext, TEvent> activity) => activity.Type;

        public Activity(ActionType type)
            : base(type)
        {
        }

        public abstract string Id { get; }
    }

    public class ActivityDefinition<TContext, TEvent> : Activity<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public ActivityDefinition(ActionType type, string id)
            : base(type)
        {
            Id = id;
        }

        public override string Id { get; }
    }

    public sealed class ActivityMap : Dictionary<string, ActivityDefinition<object, Event>>
    {
    }

    public delegate DisposeActivityFunction ActivityConfig<TContext, TEvent>(
        TContext ctx,
        ActivityDefinition<TContext, TEvent> activity
    )
        where TContext : class
        where TEvent : Event;

    public delegate void DisposeActivityFunction();
}
