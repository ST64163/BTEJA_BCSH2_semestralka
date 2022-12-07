namespace InterpreterSK.AST.Expressions.Level4;

internal class DivideExpression : BinaryExpression
{
    public DivideExpression(Expression left, Expression right, int rowNumber) : base(left, right, rowNumber) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Divide operation is not defined for two different datatypes", RowNumber);
        if (leftType != typeof(int) && leftType != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Divide operation is defined only for Int and Double datatypes", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
    {
        if ((double)rightValue == 0)
            throw new Exceptions.InvalidOperationException("Cannot divide with 0", RowNumber);
        return (leftValue.GetType() == typeof(double))
            ? (double)leftValue / (double)rightValue
            : (int)leftValue / (int)rightValue;
    } 
}
