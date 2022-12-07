using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Expressions;

internal class LiteralExpression : Expression
{
    internal object Value { get; set; }

    internal LiteralExpression(object value, int rowNumber) : base(rowNumber) 
    {
        Value = value;
    }

    protected override Type Analyzation(ExecutionContext context)
        => Value.GetType();

    internal override object Execute(ExecutionContext context)
        => Value;

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = true;
        return string.Concat(Enumerable.Repeat("-", level)) + GetType().Name + ": " + Value.ToString() + "\n";
    }
}
