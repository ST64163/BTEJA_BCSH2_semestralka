using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Functions;

internal class FunInvokeStatement : Statement
{
    internal string Identifier { get; }
    internal List<Expression> Parameters { get; }

    public FunInvokeStatement(string identifier, List<Expression> parameters)
    {
        Identifier = identifier;
        Parameters = parameters;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
