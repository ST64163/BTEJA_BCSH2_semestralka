using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Expressions;

internal abstract class BinaryExpression : Expression
{
    internal Expression Left { get; }
    internal Expression Right { get; }

    public BinaryExpression(Expression left, Expression right, int rowNumber) : base(rowNumber)
    {
        Left = left;
        Right = right;
    }

    protected override Type Analyzation(ExecutionContext context)
    {
        Type leftType = Left.Analyze(context);
        Type rightType = Right.Analyze(context);
        CheckTypes(leftType, rightType);
        return leftType;
    }

    internal override object Execute(ExecutionContext context)
    {
        object leftValue = Left.Execute(context);
        object rightValue = Right.Execute(context);
        CheckTypes(leftValue.GetType(), rightValue.GetType());
        return Execution(context, leftValue, rightValue);
    }

    protected abstract object Execution(ExecutionContext context, object leftValue, object rightValue);

    protected abstract void CheckTypes(Type leftType, Type rightType);

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = false;
        return string.Concat(Enumerable.Repeat("-", level++)) + GetType().Name + ": " + "\n" 
        + Left.GetToString(level, out bool _) + "\n"
        + Right.GetToString(level, out bool leaf) + (!leaf ? "\n" : "");
    }
}
