namespace InterpreterSK.AST.Expressions.Level3;

internal class MinusUnaryExpression : UnaryExpression
{
    public MinusUnaryExpression(Expression expression, int rowNumber) : base(expression, rowNumber) { }

    protected override void CheckType(Type type)
    {
        if (type != typeof(int) && type != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Minus operation is defined only for Int and Double datatype", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object value)
        => (value.GetType() == typeof(double)) ? -(double)value : -(int)value;
}
