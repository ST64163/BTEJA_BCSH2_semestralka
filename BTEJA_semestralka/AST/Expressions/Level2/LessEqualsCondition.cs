
namespace InterpreterSK.AST.Expressions.Level2;

internal class LessEqualsCondition : BinaryCondition
{
    public LessEqualsCondition(Expression left, Expression right) : base(left, right) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Less than or equals to condition is not defined for two different datatypes", RowNumber);
        if (leftType != typeof(int) && leftType != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Less than or equals to condition is defined only for Int and Double datatypes", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
        => (leftValue.GetType() == typeof(double))
        ? (double)leftValue <= (double)rightValue
        : (int)leftValue <= (int)rightValue;
}

