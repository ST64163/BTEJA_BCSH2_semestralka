using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Expressions;

internal abstract class UnaryExpression : Expression
{
    internal Expression Expression { get; }

    internal UnaryExpression(Expression expression, int rowNumber) : base(rowNumber)
    {
        Expression = expression;
    }

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

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = false;
        return string.Concat(Enumerable.Repeat("-", level)) + GetType().Name + ": " + "\n" 
            + Expression.GetToString(level++, out bool leaf) + (!leaf ? "\n" : "");
    }
}
