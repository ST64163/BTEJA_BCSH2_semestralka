using IDE.Interpreter.AST.Expressions;

namespace IDE.Interpreter.AST.Statements;

internal class VarDeclareStatement : Statement
{
    internal string Identifier { get; }
    internal System.Type Datatype { get; }
    internal Expression? Value { get; }

    internal VarDeclareStatement(string identifier, System.Type datatype, Expression? value)
    {
        Identifier = identifier;
        Datatype = datatype;
        Value = value;
    }
}
