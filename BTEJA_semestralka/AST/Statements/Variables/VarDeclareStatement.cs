using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Variables;

internal class VarDeclareStatement : Statement
{
    internal string Identifier { get; }
    internal Type Datatype { get; }
    internal Expression? Value { get; }

    internal VarDeclareStatement(string identifier, Type datatype, Expression? value)
    {
        Identifier = identifier;
        Datatype = datatype;
        Value = value;
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
