
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Statements.Functions;

internal class ParamDeclaration : Node
{
    internal string Identifier { get; }
    internal Type Datatype { get; }

    public ParamDeclaration(string identifier, Type datatype, int rowNumber) : base(rowNumber)
    {
        Identifier = identifier;
        Datatype = datatype;
    }

    internal override object Execute(Execution.ExecutionContext context)
        => new Variable(Identifier, Datatype);

    internal override Type Analyze(Execution.ExecutionContext context)
        => Datatype;

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = true;
        return string.Concat(Enumerable.Repeat("-", level)) + GetType().Name + ": " + Identifier + "\n";
    }
}
