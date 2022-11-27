using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Loops;

internal class WhileStatement : Statement
{
    internal Expression Condition { get; }
    internal Statement Statement { get; }

    public WhileStatement(Expression condition, Statement statement)
    {
        Condition = condition;
        Statement = statement;
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        Condition.Analyze(context);
        Statement.Analyze(context);
    }
}
