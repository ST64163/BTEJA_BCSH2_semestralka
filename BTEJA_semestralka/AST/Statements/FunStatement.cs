
namespace InterpreterSK.AST.Statements;

internal abstract class FunStatement : Statement
{
    internal string Identifier { get; }

    internal FunStatement(string identifier, int rowNumber) : base(rowNumber)
    {
        Identifier = identifier;
    }

    internal override bool EndsInReturn(Execution.ExecutionContext _, Type __)
        => false;
}
