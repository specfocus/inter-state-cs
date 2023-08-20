namespace XState.Actions
{
    public class StopActivity
    {
        public static implicit operator StopActivity(string id) => new StopActivity(id);


        public static implicit operator string(StopActivity activity) => activity.Id;

        public StopActivity(string id) => Id = id;

        public virtual string Id { get; }

        public override bool Equals(object? obj) => obj is StopActivity activity && Id == activity.Id || obj is string str && Id == str;

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Id;
    }

    public class DelegateStopActivity<TContext, TExpressionEvent> : StopActivity
        where TContext : class
        where TExpressionEvent : Event
    {
        public DelegateStopActivity(Func<TContext, TExpressionEvent, StopActivity> expr)
            : base("")
        {
            Expr = expr;
        }

        public override string Id => Expr(default!, default!).Id;

        public Func<TContext, TExpressionEvent, StopActivity> Expr { get; set; }
    }

    public interface IStopAction<TContext, TExpressionEvent, TEvent>
        : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        StopActivity Activity { get; }
    }

    public class StopAction<TContext, TExpressionEvent, TEvent> : Action<TContext, TExpressionEvent, TEvent>, IStopAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public StopAction(StopActivity activity) => Activity = activity;

        public override ActionType Type => ActionTypes.Stop.Value;

        public StopActivity Activity { get; }
    }

    public class StopActionObject<TContext, TExpressionEvent, TEvent> : StopAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public StopActionObject(StopActivity activity)
            : base(activity)
        {
        }
    }
}
