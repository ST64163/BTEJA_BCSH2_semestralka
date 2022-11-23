using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements;

internal class FunInvokeStatement : Statement
{
    internal string Identifier { get; }
    internal List<Expression> Parameters { get; }

    public FunInvokeStatement(string identifier, List<Expression> parameters)
    {
        Identifier = identifier;
        Parameters = parameters;
    }
}
