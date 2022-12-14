using InterpreterSK.Execution.Elements;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Expressions.Level6;

internal class VarInvokeExpression : InvokeExpression
{
    public VarInvokeExpression(string identifier, int rowNumber) : base(identifier, rowNumber) {}

    protected override Type Analyzation(ExecutionContext context)
        => FindVariable(context).Datatype;

    internal override object Execute(ExecutionContext context)
    {
        Variable variable = FindVariable(context);
        if (variable.Expression == null)
            throw new Exceptions.InvalidInvocationException($"Variable {Identifier} not declared", RowNumber);
        return variable.Expression.Execute(context);
    }

    private Variable FindVariable(ExecutionContext context)
        => context.VariableContext.GetVariable(Identifier, RowNumber);
}
