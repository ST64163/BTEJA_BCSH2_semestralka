using IDE.Interpreter.AST.Expressions;

namespace IDE.Interpreter.AST.Statements;

internal class ReturnStatement : Statement
{
    internal Expression Expression;

    public ReturnStatement(Expression expression)
    {
        Expression = expression;
    }
}
