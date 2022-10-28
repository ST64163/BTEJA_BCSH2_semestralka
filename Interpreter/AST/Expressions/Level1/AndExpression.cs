
namespace IDE.Interpreter.AST.Expressions;

internal class AndExpression : BinaryExpression
{
    public AndExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (bool)Left.Evaluate() && (bool)Right.Evaluate();
}
