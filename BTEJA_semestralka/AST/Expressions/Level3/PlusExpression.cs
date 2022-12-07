namespace InterpreterSK.AST.Expressions.Level3;

internal class PlusExpression : BinaryExpression
{
    public PlusExpression(Expression left, Expression right, int rowNumber) : base(left, right, rowNumber) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException($"Addition operation is not defined for two different datatypes", RowNumber);
        if (leftType != typeof(int) && leftType != typeof(double) && leftType != typeof(string))
            throw new Exceptions.InvalidDatatypeException("Addition operation is defined only for Int, Double and String datatypes", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
    {
        Type type = leftValue.GetType();
        if (type == typeof(double))
            return (double)leftValue + (double)rightValue;
        else if (type == typeof(string))
            return (string)leftValue + (string)rightValue;
        else
            return (int)leftValue + (int)rightValue;
    }
}
