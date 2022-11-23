
namespace InterpreterSK.AST.Expressions.Level4;

internal class MultiplyExpression : BinaryExpression
{
    public MultiplyExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() * (double)Right.Evaluate();
}
