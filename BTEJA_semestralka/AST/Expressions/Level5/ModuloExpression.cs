
namespace InterpreterSK.AST.Expressions.Level5;

internal class ModuloExpression : BinaryExpression
{
    public ModuloExpression(Expression left, Expression right, int rowNumber) : base(left, right, rowNumber) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != rightType)
            throw new Exceptions.InvalidDatatypeException("Modulo operation is not defined for two different datatypes", RowNumber);
        if (leftType != typeof(int) && leftType != typeof(double))
            throw new Exceptions.InvalidDatatypeException("Modulo operation is defined only for Int and Double datatypes", RowNumber);
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
    {
        if ((double)rightValue == 0)
            throw new Exceptions.InvalidOperationException("Cannot divide with 0", RowNumber);
        return (leftValue.GetType() == typeof(double))
            ? (double)leftValue % (double)rightValue
            : (int)leftValue % (int)rightValue;
    }
}
