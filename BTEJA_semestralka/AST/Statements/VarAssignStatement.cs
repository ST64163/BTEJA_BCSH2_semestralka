using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements;

internal class VarAssignStatement : Statement
{
    internal string Identifier { get; }
    internal Expression Expression { get; }

    public VarAssignStatement(string identifier, Expression expression)
    {
        Identifier = identifier;
        Expression = expression;
    }
}
