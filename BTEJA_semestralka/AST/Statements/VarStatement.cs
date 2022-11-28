
namespace InterpreterSK.AST.Statements;

internal abstract class VarStatement : Statement
{
    internal string Identifier { get; }

    internal VarStatement(string identifier)
    { 
        Identifier = identifier;
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        Operation(context);
        return this;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
        => Operation(context);

    protected abstract void Operation(Execution.ExecutionContext context);
}
