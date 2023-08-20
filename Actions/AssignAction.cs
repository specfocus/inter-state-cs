using static XState.SCXML;

namespace XState.Actions
{
    public interface AssignMeta<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        State<TContext, TEvent> State { get; set; }

        AssignAction<TContext, TEvent, TEvent> Action { get; set; }

        Event<TEvent> Event { get; set; }
    }

    public delegate Partial<TContext> Assigner<TContext, TEvent>(TContext context, TEvent @event, AssignMeta<TContext, TEvent> meta)
        where TContext : class
        where TEvent : Event;

    public delegate TContextValue PartialAssigner<TContext, TEvent, TContextValue>(TContext context, TEvent @event, AssignMeta<TContext, TEvent> meta)
        where TContext : class
        where TEvent : Event;

    public class PropertyAssigner<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public Dictionary<string, PartialAssigner<TContext, TEvent, string>> Assigners { get; } = new Dictionary<string, PartialAssigner<TContext, TEvent, string>>();
    }


    public interface IAssignAction<TContext, TExpressionEvent, TEvent>
        : IAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        Assigner<TContext, TExpressionEvent>? Assigner { get; }

        PropertyAssigner<TContext, TExpressionEvent>? Assignment { get; }
    }

    public class AssignAction<TContext, TExpressionEvent, TEvent> : Action<TContext, TExpressionEvent, TEvent>, IAssignAction<TContext, TExpressionEvent, TEvent>
        where TContext : class
        where TExpressionEvent : Event
        where TEvent : Event
    {
        public static implicit operator AssignAction<TContext, TExpressionEvent, TEvent>(Assigner<TContext, TExpressionEvent> assigner) => new AssignAction<TContext, TExpressionEvent, TEvent>(assigner);

        public static implicit operator AssignAction<TContext, TExpressionEvent, TEvent>(PropertyAssigner<TContext, TExpressionEvent> assignment) => new AssignAction<TContext, TExpressionEvent, TEvent>(assignment);

        public AssignAction(Assigner<TContext, TExpressionEvent>? assigner) => Assigner = assigner;

        public AssignAction(PropertyAssigner<TContext, TExpressionEvent> assignment) => Assignment = assignment;

        public Assigner<TContext, TExpressionEvent>? Assigner { get; }

        public PropertyAssigner<TContext, TExpressionEvent>? Assignment { get; }

        public override ActionType Type => ActionTypes.Assign.Value;

        public override bool Equals(object? obj) => obj is Assigner<TContext, TExpressionEvent> assigner && Assigner == assigner || obj is PropertyAssigner<TContext, TExpressionEvent> assignment && Assignment == assignment;

        public override int GetHashCode() => HashCode.Combine(Assigner, Assignment);
    }
}
