namespace IDE.Interpreter.AST.Expressions;

internal class GreaterEqualsExpression : BinaryExpression
{
    public GreaterEqualsExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() >= (double)Right.Evaluate();
}
