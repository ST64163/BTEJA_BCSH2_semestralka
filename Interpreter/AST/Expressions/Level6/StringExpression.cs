
namespace IDE.Interpreter.AST.Expressions;

internal class StringExpression : Expression
{
    internal string Value { get; }

    public StringExpression(string value) => Value = value;

    internal override object Evaluate() => Value;
}