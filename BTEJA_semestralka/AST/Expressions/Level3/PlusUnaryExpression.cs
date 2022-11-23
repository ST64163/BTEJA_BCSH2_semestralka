namespace InterpreterSK.AST.Expressions.Level3;

internal class PlusUnaryExpression : UnaryExpression
{

    internal PlusUnaryExpression(Expression expression) : base(expression) { }

    internal override object Evaluate() => +(double)Expression.Evaluate();
}
