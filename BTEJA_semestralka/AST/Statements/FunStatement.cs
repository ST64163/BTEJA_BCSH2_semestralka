
namespace InterpreterSK.AST.Statements;

internal abstract class FunStatement : Statement
{
    internal string Identifier { get; }

    internal FunStatement(string identifier)
    {
        Identifier = identifier;
    }
}
