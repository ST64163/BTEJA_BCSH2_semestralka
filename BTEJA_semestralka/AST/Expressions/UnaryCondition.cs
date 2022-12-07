
namespace InterpreterSK.AST.Expressions;

internal abstract class UnaryCondition : UnaryExpression
{
    internal UnaryCondition(Expression expression, int rowNumber) : base(expression, rowNumber) { }

    internal override Type Analyze(Execution.ExecutionContext context)
    {
        if (AnalyzedType == null)
        {
            AnalyzedType = typeof(bool);
            Type type = Expression.Analyze(context);
            CheckType(type);
        }
        return AnalyzedType;
    }

}
