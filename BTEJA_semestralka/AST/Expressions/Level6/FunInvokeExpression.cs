using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Expressions.Level6;

internal class FunInvokeExpression : InvokeExpression
{

    internal List<Expression> Parameters { get; }

    public FunInvokeExpression(string identifier, List<Expression> parameters) : base(identifier)
    {
        Parameters = parameters;
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        Function function = context.FunctionContext.GetFunction(Identifier, RowNumber);
        return function.Call(context, Parameters, RowNumber);
    } 

    protected override Type Analyzation(Execution.ExecutionContext context)
    {
        Function function = context.FunctionContext.GetFunction(Identifier, RowNumber);
        return function.Analyze(context, Parameters, RowNumber);
    }
}
