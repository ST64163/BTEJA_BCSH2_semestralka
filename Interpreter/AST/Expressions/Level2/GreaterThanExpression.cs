
namespace IDE.Interpreter.AST.Expressions;

internal class GreaterThanExpression : BinaryExpression
{
    public GreaterThanExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() > (double)Right.Evaluate();
}
