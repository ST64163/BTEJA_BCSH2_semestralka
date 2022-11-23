
namespace InterpreterSK.AST.Expressions.Level2;

internal class LessThanExpression : BinaryExpression
{
    public LessThanExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() < (double)Right.Evaluate();
}
