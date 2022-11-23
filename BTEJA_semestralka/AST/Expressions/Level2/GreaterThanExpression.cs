namespace InterpreterSK.AST.Expressions.Level2;

internal class GreaterThanExpression : BinaryExpression
{
    public GreaterThanExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() > (double)Right.Evaluate();
}
