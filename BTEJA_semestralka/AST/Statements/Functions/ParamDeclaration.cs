
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Statements.Functions;

internal class ParamDeclaration : Node
{
    internal string Identifier { get; }
    internal Type Datatype { get; }

    public ParamDeclaration(string identifier, Type datatype)
    {
        Identifier = identifier;
        Datatype = datatype;
    }

    internal override object Execute(Execution.ExecutionContext context)
        => new Variable(Identifier, Datatype, null);

    internal override Type Analyze(Execution.ExecutionContext context)
        => Datatype;
}
