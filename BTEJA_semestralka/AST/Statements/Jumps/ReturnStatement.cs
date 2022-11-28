using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Jumps;

internal class ReturnStatement : JumpStatement
{
    internal Expression Expression { get; }

    internal object Value { get; private set; }

    public ReturnStatement(Expression expression)
    {
        Expression = expression;
        Value = expression;
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
}
