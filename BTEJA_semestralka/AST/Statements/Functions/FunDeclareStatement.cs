using InterpreterSK.AST.Statements.Block;

namespace InterpreterSK.AST.Statements.Functions;

internal class FunDeclareStatement : Statement
{
    internal string Identifier { get; }
    internal List<ParamDeclaration> Parameters { get; }
    internal Type ReturnType { get; }
    internal BlockStatement Block { get; }

    internal FunDeclareStatement(string identifier, List<ParamDeclaration> parameters, Type returnType, BlockStatement block)
    {
        Identifier = identifier;
        ReturnType = returnType;
        Parameters = parameters;
        Block = block;
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
