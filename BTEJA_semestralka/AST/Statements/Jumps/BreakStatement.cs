namespace InterpreterSK.AST.Statements.Jumps;

internal class BreakStatement : JumpStatement
{
    protected override void Analyzation(Execution.ExecutionContext context) 
    {
        if (context.BranchOwner is not LoopStatement)
            throw new Exceptions.InvalidSyntaxException("Break statement can be located only in loop statement", RowNumber);
    }

    internal override object Execute(Execution.ExecutionContext context)
        => this;
}
