
namespace InterpreterSK.AST.Statements;

internal abstract class VarStatement : Statement
{
    internal string Identifier { get; }

    internal VarStatement(string identifier, int rowNumber) : base(rowNumber)
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

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = true;
        return string.Concat(Enumerable.Repeat("-", level)) + GetType().Name + ": " + Identifier + "\n";
    }
}
