namespace InterpreterSK.AST.Expressions.Level6;

internal class StringExpression : Expression
{
    internal string Value { get; }

    public StringExpression(string value) => Value = value;

    internal override object Evaluate() => Value;
}