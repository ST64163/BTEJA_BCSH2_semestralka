
namespace InterpreterSK.AST.Statements;

internal class BlockStatement : Statement
{
    internal List<Statement> Statements { get; }

    internal BlockStatement(List<Statement> statements)
    {
        Statements = statements;
    }
}
