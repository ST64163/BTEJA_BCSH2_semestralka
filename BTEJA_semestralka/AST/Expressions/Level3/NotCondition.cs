namespace InterpreterSK.AST.Expressions.Level3;

internal class NotCondition : UnaryCondition
{
    public NotCondition(Expression expression, int rowNumber) : base(expression, rowNumber) { }

    protected override void CheckType(Type type)
    {
        if (type != typeof(bool))
            throw new Exceptions.InvalidDatatypeException("Not condition is defined only for Boolean datatype", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object value)
        => !(bool)value;
}
