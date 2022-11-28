namespace InterpreterSK.AST.Statements;

internal abstract class LoopStatement : Statement
{
    internal Statement Statement { get; }

    internal LoopStatement(Statement statement)
    {
        Statement = statement;
    }
}
