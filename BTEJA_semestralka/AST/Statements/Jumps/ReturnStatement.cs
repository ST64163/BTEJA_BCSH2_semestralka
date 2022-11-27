using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Jumps;

internal class ReturnStatement : Statement
{
    internal Expression Expression;

    public ReturnStatement(Expression expression)
    {
        Expression = expression;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
        => Expression.Analyze(context);

    internal override object Execute(Execution.ExecutionContext context)
        => Expression.Execute(context);
}
