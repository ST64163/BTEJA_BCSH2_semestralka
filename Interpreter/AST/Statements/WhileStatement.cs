using IDE.Interpreter.AST.Expressions;

namespace IDE.Interpreter.AST.Statements;

internal class WhileStatement : Statement
{
    internal Expression Condition { get; }
    internal Statement Statement { get; }

    public WhileStatement(Expression condition, Statement statement)
    {
        Condition = condition;
        Statement = statement;
    }
}
