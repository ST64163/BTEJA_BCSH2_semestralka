namespace InterpreterSK.AST.Expressions.Level2;

internal class NotEqualsCondition : BinaryCondition
{
    public NotEqualsCondition(Expression left, Expression right) : base(left, right) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Not equals to condition is not defined for two different datatypes");
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
        => leftValue != rightValue;
}
