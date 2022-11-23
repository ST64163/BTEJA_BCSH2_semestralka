namespace InterpreterSK.AST.Expressions.Level3;

internal class MinusExpression : BinaryExpression
{
    public MinusExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() - (double)Right.Evaluate();
}
