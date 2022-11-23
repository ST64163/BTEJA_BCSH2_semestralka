namespace InterpreterSK.AST.Expressions.Level2;

internal class NotEqualsExpression : BinaryExpression
{
    public NotEqualsExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => Left.Evaluate() != Right.Evaluate();
}
