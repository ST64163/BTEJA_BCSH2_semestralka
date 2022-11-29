
namespace InterpreterSK.AST.Statements;

internal abstract class VarStatement : Statement
{
    internal string Identifier { get; }

    internal VarStatement(string identifier)
    { 
        Identifier = identifier;
    }

    internal override bool EndsInReturn(Execution.ExecutionContext _, Type __)
        => false;

    internal override object Execute(Execution.ExecutionContext context)
    {
        Operation(context, true);
        return this;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
        => Operation(context, false);

    protected abstract void Operation(Execution.ExecutionContext context, bool execute);
}
