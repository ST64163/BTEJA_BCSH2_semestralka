using IDE.Interpreter.AST.Expressions;
using System.Collections.Generic;

namespace IDE.Interpreter.AST.Statements;

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
