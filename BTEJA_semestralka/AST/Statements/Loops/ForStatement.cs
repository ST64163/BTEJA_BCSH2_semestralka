using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Loops;

internal class ForStatement : Statement
{
    internal string Variable { get; }
    internal Expression Start { get; }
    internal Expression End { get; }
    internal Statement Statement { get; }

    public ForStatement(string identifier, Expression start, Expression end, Statement statement)
    {
        Variable = new(identifier);
        Start = start;
        End = end;
        Statement = statement;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
