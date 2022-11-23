namespace InterpreterSK.AST.Expressions.Level6;

internal class DoubleExpression : Expression
{
    internal double Value { get; }

    public DoubleExpression(double value) => Value = value;

    internal override object Evaluate() => Value;
}
