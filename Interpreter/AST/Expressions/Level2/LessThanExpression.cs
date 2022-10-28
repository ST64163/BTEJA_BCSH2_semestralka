namespace IDE.Interpreter.AST.Expressions;

internal class LessThanExpression : BinaryExpression
{
    public LessThanExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() < (double)Right.Evaluate();
}
