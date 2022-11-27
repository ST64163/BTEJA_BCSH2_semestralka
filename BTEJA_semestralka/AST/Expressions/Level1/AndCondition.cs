namespace InterpreterSK.AST.Expressions.Level1;

internal class AndCondition : BinaryCondition
{
    internal AndCondition(Expression left, Expression right) : base(left, right) { }

    protected override void CheckTypes(Type leftType, Type rightType)
    {
        if (leftType != typeof(bool) || rightType != typeof(bool))
            throw new Exceptions.InvalidDatatypeException("And condition is defined only for Boolean datatypes");
    }

    protected override object Execution(Execution.ExecutionContext context, object leftValue, object rightValue)
        => (bool)leftValue && (bool)rightValue;
}
