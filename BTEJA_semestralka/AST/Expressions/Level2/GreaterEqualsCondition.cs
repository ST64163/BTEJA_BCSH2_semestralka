namespace InterpreterSK.AST.Expressions.Level2;

internal class GreaterEqualsCondition : BinaryCondition
{
    public GreaterEqualsCondition(Expression left, Expression right) : base(left, right) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Greater than or equals to condition is not defined for two different datatypes", RowNumber);
        if (leftType != typeof(int) && leftType != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Greater than or equals to condition is defined only for Int and Double datatypes", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
        => (leftValue.GetType() == typeof(double)) 
        ? (double)leftValue >= (double)rightValue
        : (int)leftValue >= (int)rightValue;
}
