
namespace IDE.Interpreter.AST.Expressions;

internal class BoolExpression : Expression
{
    internal bool Value { get; }

    public BoolExpression(bool value) => Value = value;

    internal override object Evaluate() => Value;
}
