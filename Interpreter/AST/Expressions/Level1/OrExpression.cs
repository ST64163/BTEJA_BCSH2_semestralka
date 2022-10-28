
namespace IDE.Interpreter.AST.Expressions;

internal class OrExpression : BinaryExpression
{
    public OrExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (bool)Left.Evaluate() || (bool)Right.Evaluate();
}
