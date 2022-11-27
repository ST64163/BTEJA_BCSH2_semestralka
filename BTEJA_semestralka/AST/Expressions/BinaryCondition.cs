
namespace InterpreterSK.AST.Expressions;

internal abstract class BinaryCondition : BinaryExpression
{
    protected BinaryCondition(Expression left, Expression right) : base(left, right) {}

    internal override Type Analyze(Execution.ExecutionContext context)
    {
        if (AnalyzedType == null)
        {
            AnalyzedType = typeof(bool);
            Type leftType = Left.Analyze(context);
            Type rightType = Right.Analyze(context);
            CheckTypes(leftType, rightType);
        }
        return AnalyzedType;
    }
}
