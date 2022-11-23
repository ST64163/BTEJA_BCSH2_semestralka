namespace InterpreterSK.AST.Expressions.Level3;

internal class PlusExpression : BinaryExpression
{
    public PlusExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() + (double)Right.Evaluate();
}
