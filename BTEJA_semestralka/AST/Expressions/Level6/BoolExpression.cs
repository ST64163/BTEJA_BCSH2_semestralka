namespace InterpreterSK.AST.Expressions.Level6;

internal class BoolExpression : Expression
{
    internal bool Value { get; }

    public BoolExpression(bool value) => Value = value;

    internal override object Evaluate() => Value;
}
