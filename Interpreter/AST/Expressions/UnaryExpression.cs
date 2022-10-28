namespace IDE.Interpreter.AST.Expressions;

internal abstract class UnaryExpression : Expression
{
    internal Expression Expression { get; }

    internal UnaryExpression(Expression expression) => Expression = expression;
}
