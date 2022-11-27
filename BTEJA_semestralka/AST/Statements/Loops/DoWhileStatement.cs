using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Statements.Block;

namespace InterpreterSK.AST.Statements.Loops;

internal class DoWhileStatement : Statement
{
    internal Expression Condition { get; }
    internal BlockStatement Block { get; }

    public DoWhileStatement(Expression condition, BlockStatement block)
    {
        Condition = condition;
        Block = block;
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
