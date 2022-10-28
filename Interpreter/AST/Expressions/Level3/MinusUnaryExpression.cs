
namespace IDE.Interpreter.AST.Expressions;

internal class MinusUnaryExpression : UnaryExpression
{
    public MinusUnaryExpression(Expression expression) : base(expression) { }

    internal override object Evaluate() => -(double)Expression.Evaluate();
}
