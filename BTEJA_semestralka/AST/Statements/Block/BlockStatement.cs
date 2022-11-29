using InterpreterSK.AST.Statements.Branching;
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
            if (result is JumpStatement)
                return result;
        }
        return this;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        Statements.ForEach(statement => statement.Analyze(context));
    }

    internal override bool EndsInReturn(Execution.ExecutionContext context, Type datatype)
    {
        bool hasReturn = true;
        foreach (Statement statement in Statements)
        {
            if (statement is LoopStatement || statement is IfStatement)
                if (!statement.EndsInReturn(context, datatype))
                    hasReturn = false;
            if (statement is ReturnStatement)
            { 
                hasReturn = statement.EndsInReturn(context, datatype);
                break;
            }
        }
        return hasReturn;
    }
}
