namespace InterpreterSK.AST.Expressions.Level3;

internal class MinusExpression : BinaryExpression
{
    public MinusExpression(Expression left, Expression right, int rowNumber) : base(left, right, rowNumber) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Subtraction operation is not defined for two different datatypes", RowNumber);
        if (leftType != typeof(int) && leftType != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Subtraction operation is defined only for Int and Double datatypes", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
        => (leftValue.GetType() == typeof(double))
        ? (double)leftValue - (double)rightValue
        : (int)leftValue - (int)rightValue;
}
