namespace InterpreterSK.AST.Expressions.Level4;

internal class DivideExpression : BinaryCondition
{
    public DivideExpression(Expression left, Expression right) : base(left, right) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Divide operation is not defined for two different datatypes");
        if (leftType != typeof(int) && leftType != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Divide operation is defined only for Int and Double datatypes");
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
        => (leftValue.GetType() == typeof(double))
        ? (double)leftValue / (double)rightValue
        : (int)leftValue * (int)rightValue;
}
