namespace InterpreterSK.AST.Expressions;

internal abstract class BinaryExpression : Expression
{
    internal Expression Left { get; }
    internal Expression Right { get; }

    public BinaryExpression(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }
}
