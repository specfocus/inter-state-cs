using XState.Actions;
using static XState.SCXML;

namespace XState
{
    internal static partial class Utils
    {
        public static TContext UpdateContext<TContext, TEvent>(
            TContext context,
            Event<TEvent> @event,
            List<AssignAction<TContext, TEvent, TEvent>> assignActions,
            State<TContext, TEvent, object, object, object>? state = null
        )
            where TContext : class
            where TEvent : Event
        {
            if (context == null)
            {
                if (!Environment.IS_PRODUCTION)
                {
                    Console.WriteLine("Attempting to update undefined context");
                }
                return default; // Assuming TContext is a reference type, return null
            }

            var updatedContext = assignActions.Aggregate(context, (acc, assignAction) =>
            {
                var assignment = assignAction.Assignment;
                var meta = new
                {
                    State = state,
                    Action = assignAction,
                    Event = @event
                };

                var partialUpdate = new Dictionary<string, object>();

                if (assignment is Func<TContext, object, object, IDictionary<string, object>, IDictionary<string, object>> assignmentFunc)
                {
                    partialUpdate = assignmentFunc(acc, @event.Data, meta);
                }
                else if (assignment is IDictionary<string, object> assignmentDict)
                {
                    foreach (var key in assignmentDict.Keys)
                    {
                        var propAssignment = assignmentDict[key];
                        partialUpdate[key] = propAssignment is Func<TContext, object, object, IDictionary<string, object>, object>
                            ? ((Func<TContext, object, object, IDictionary<string, object>, object>)propAssignment)(acc, @event.Data, meta)
                            : propAssignment;
                    }
                }

                return partialUpdate.Aggregate(acc, (current, kvp) =>
                {
                    current[kvp.Key] = kvp.Value;
                    return current;
                });
            });

            return updatedContext;
        }
    }
}
