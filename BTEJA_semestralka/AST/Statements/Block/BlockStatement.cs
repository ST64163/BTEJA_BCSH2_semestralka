using InterpreterSK.AST.Statements.Jumps;

namespace InterpreterSK.AST.Statements.Block;

internal class BlockStatement : Statement
{
    internal List<Statement> Statements { get; }

    internal BlockStatement(List<Statement> statements)
    {
        Statements = statements;
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        foreach (Statement statement in Statements)
        { 
            object result = statement.Execute(context);
            if (result is ReturnStatement)
                return result;
        }
        return this;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        Statements.ForEach(statement => statement.Analyze(context));
    }
}
