namespace InterpreterSK.AST.Expressions.Level4;

internal class DivideExpression : BinaryExpression
{
    public DivideExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() / (double)Right.Evaluate();
}
