using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements;

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
}
