namespace InterpreterSK.AST.Expressions.Level3;

internal class PlusUnaryExpression : UnaryExpression
{

    internal PlusUnaryExpression(Expression expression) : base(expression) { }

    protected override void CheckType(Type type)
    {
        if (type != typeof(int) && type != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Plus operation is defined only for Int and Double datatype");
    }

    protected override object Execution(Execution.ExecutionContext context, object value)
        => (value.GetType() == typeof(double)) ? +(double)value : +(int)value;
}
