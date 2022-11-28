using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Expressions;

internal class LiteralExpression : Expression
{
    internal object Value { get; set; }

    internal LiteralExpression(object value) 
    {
        Value = value;
    }

    protected override Type Analyzation(ExecutionContext context)
        => Value.GetType();

    internal override object Execute(ExecutionContext context)
        => Value;
}
