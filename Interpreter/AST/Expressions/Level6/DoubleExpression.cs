
namespace IDE.Interpreter.AST.Expressions;

internal class DoubleExpression : Expression
{
    internal double Value { get; }

    public DoubleExpression(double value) => Value = value;

    internal override object Evaluate() => Value;
}
