
namespace InterpreterSK.AST.Expressions.Level2;

internal class EqualsExpression : BinaryExpression
{
    public EqualsExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => Left.Evaluate() == Right.Evaluate();
}
