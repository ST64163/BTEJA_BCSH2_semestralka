
namespace InterpreterSK.AST.Statements.Functions;

internal class ParamDeclaration : Statement
{
    internal string Identifier { get; }
    internal Type Datatype { get; }

    public ParamDeclaration(string identifier, Type datatype)
    {
        Identifier = identifier;
        Datatype = datatype;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
