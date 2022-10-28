namespace IDE.Interpreter.AST.Expressions;

internal class LessEqualsExpression : BinaryExpression
{
    public LessEqualsExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() <= (double)Right.Evaluate();
}

