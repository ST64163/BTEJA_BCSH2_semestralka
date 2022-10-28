using IDE.Interpreter.AST.Expressions;

namespace IDE.Interpreter.AST.Statements;

internal class DoWhileStatement : Statement
{
    internal Expression Condition { get; }
    internal BlockStatement Block { get; }

    public DoWhileStatement(Expression condition, BlockStatement block)
    {
        Condition = condition;
        Block = block;
    }
}
