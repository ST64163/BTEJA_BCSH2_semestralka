namespace InterpreterSK.AST.Expressions.Level3;

internal class MinusUnaryExpression : UnaryExpression
{
    public MinusUnaryExpression(Expression expression) : base(expression) { }

    internal override object Evaluate() => -(double)Expression.Evaluate();
}
