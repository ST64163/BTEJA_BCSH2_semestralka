
namespace IDE.Interpreter.AST.Expressions;

internal class NotExpression : UnaryExpression
{
    public NotExpression(Expression expression) : base(expression) { }

    internal override object Evaluate() => !(bool)Expression.Evaluate();
}
