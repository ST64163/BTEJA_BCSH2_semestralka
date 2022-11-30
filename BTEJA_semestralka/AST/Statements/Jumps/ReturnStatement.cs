using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Jumps;

internal class ReturnStatement : JumpStatement
{
    internal Expression Expression { get; }

    internal object? Value { get; private set; }

    public ReturnStatement(Expression expression)
    {
        Expression = expression;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        AnalyzedType = Expression.Analyze(context);   
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        Value = Expression.Execute(context);
        return this;
    }

    internal override bool EndsInReturn(Execution.ExecutionContext context, Type datatype)
    {
        if (AnalyzedType == null)
            AnalyzedType = Expression.Analyze(context);
        if (AnalyzedType != datatype)
            throw new Exceptions.InvalidDatatypeException($"Invalid datatype of return expression", RowNumber);
        return true;
    }
}
