namespace InterpreterSK.AST.Expressions.Level6;

internal class IntExpression : Expression
{
    internal int Value { get; }

    public IntExpression(int value) => Value = value;

    internal override object Evaluate() => Value;
}
