namespace InterpreterSK.AST.Statements;

internal abstract class LoopStatement : Statement
{
    internal Statement Statement { get; }

    protected int CurrentRepetition { get; set; } = 0;

    internal LoopStatement(Statement statement)
    {
        Statement = statement;
    }

    internal override bool EndsInReturn(Execution.ExecutionContext context, Type datatype)
        => Statement.EndsInReturn(context, datatype);
}
