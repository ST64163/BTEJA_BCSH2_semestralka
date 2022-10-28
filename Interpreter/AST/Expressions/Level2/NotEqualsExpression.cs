namespace IDE.Interpreter.AST.Expressions;

internal class NotEqualsExpression : BinaryExpression
{
    public NotEqualsExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => Left.Evaluate() != Right.Evaluate();
}
