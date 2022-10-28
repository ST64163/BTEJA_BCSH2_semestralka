using System.Collections.Generic;

namespace IDE.Interpreter.AST.Expressions;

internal class FunInvokeExpression : Expression
{
    internal string Identifier { get; }

    internal List<Expression> Parameters { get; }

    public FunInvokeExpression(string identifier, List<Expression> parameters)
    {
        Identifier = identifier;
        Parameters = parameters;
    }

    internal override object Evaluate() => throw new System.InvalidOperationException();
}
