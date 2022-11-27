using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Expressions;

internal abstract class UnaryExpression : Expression
{
    internal Expression Expression { get; }

    internal UnaryExpression(Expression expression) => Expression = expression;

    protected override Type Analyzation(ExecutionContext context)
    {
        Type type = Expression.Analyze(context);
        CheckType(type);
        return type;
    }

    internal override object Execute(ExecutionContext context)
    {
        object value = Expression.Execute(context);
        CheckType(value.GetType());
        return Execution(context, value);
    }

    protected abstract object Execution(ExecutionContext context, object value);

    protected abstract void CheckType(Type type);
}
