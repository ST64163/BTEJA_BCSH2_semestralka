using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Statements.Functions;

internal class FunInvokeStatement : FunStatement
{
    internal List<Expression> Parameters { get; }

    public FunInvokeStatement(string identifier, List<Expression> parameters) : base(identifier)
    {
        Parameters = parameters;
    }

    protected override void Analyzation(Execution.ExecutionContext context) 
    {
        Function function = context.FunctionContext.GetFunction(Identifier, RowNumber);
        function.Analyze(context, Parameters, RowNumber);
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        Function function = context.FunctionContext.GetFunction(Identifier, RowNumber);
        function.Call(context, Parameters, RowNumber);
        return this;
    }
}
