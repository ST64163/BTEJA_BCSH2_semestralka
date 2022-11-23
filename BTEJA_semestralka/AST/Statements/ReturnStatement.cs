using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements;

internal class ReturnStatement : Statement
{
    internal Expression Expression;

    public ReturnStatement(Expression expression)
    {
        Expression = expression;
    }
}
