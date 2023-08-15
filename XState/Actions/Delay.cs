namespace XState.Actions
{
    public class Delay<TContext, TEvent>
        where TContext : class
        where TEvent : Event
    {
        public static implicit operator Delay<TContext, TEvent>(int value) => new(value);


        public static implicit operator int?(Delay<TContext, TEvent> delay) => delay.Value;

        public Delay(int value) => Value = value;

        public Delay(DelayExpr<TContext, TEvent> expr) => Expr = expr;

        public int? Value { get; }

        public DelayExpr<TContext, TEvent>? Expr { get; }
    }
}
