using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Jumps;

internal class ReturnStatement : JumpStatement
{
    internal Expression Expression { get; }

    private object? value;

    public ReturnStatement(Expression expression, int rowNumber) : base(rowNumber)
    {
        Expression = expression;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        AnalyzedType = Expression.Analyze(context);   
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        value = Expression.Execute(context);
        return this;
    }

    internal object GetValue()
    {
        if (value == null)
            throw new Exception("Unexpected behaviour");
        return value;
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
