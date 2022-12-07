using System.ComponentModel.DataAnnotations;

namespace InterpreterSK.AST.Expressions.Level2;

internal class NotEqualsCondition : BinaryCondition
{
    public NotEqualsCondition(Expression left, Expression right, int rowNumber) : base(left, right, rowNumber) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Not equals to condition is not defined for two different datatypes", RowNumber);
        if (leftType != typeof(string) && leftType != typeof(int) && leftType != typeof(double) && leftType != typeof(bool))
            throw new Exceptions.InvalidDatatypeException("Not equals to condition is defined only for String, Int, Double, Boolean datatypes", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
    {
        Type type = leftValue.GetType();
        if (type == typeof(bool))
            return (bool)leftValue != (bool)rightValue;
        else if (type == typeof(int))
            return (int)leftValue != (int)rightValue;
        else if (type == typeof(double))
            return (double)leftValue != (double)rightValue;
        else if (type == typeof(string))
            return (string)leftValue != (string)rightValue;
        else
            throw new Exception("Unexpected behaviour");
    }
}
