namespace InterpreterSK.AST.Statements.Jumps;

internal class ContinueStatement : JumpStatement
{
    public ContinueStatement(int rowNumber) : base(rowNumber) {}

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        if (context.BranchOwner is not LoopStatement)
            throw new Exceptions.InvalidSyntaxException("Continue statement can be located only in loop statement", RowNumber);
    }

    internal override object Execute(Execution.ExecutionContext context)
        => this;
}
