using InterpreterSK.Execution.Elements;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Expressions.Level6;

internal class FunInvokeExpression : InvokeExpression
{

    internal List<Expression> Parameters { get; }

    public FunInvokeExpression(string identifier, List<Expression> parameters) : base(identifier)
    {
        Parameters = parameters;
    }

    internal override object Execute(ExecutionContext context)
        => FindFunction(context).Execute(context);

    protected override Type Analyzation(ExecutionContext context)
        => FindFunction(context).Analyze(context);

    private Function FindFunction(ExecutionContext context)
        => context.FunctionContext.GetFunction(Identifier);
}
