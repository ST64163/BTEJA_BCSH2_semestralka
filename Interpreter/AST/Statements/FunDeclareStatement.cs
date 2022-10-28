
using System.Collections.Generic;
using ParamExpression = IDE.Interpreter.AST.Expressions.ParamExpression;

namespace IDE.Interpreter.AST.Statements;

internal class FunDeclareStatement : Statement
{
    internal string Identifier { get; }
    internal List<ParamExpression> Parameters { get; }
    internal System.Type ReturnType { get; }
    internal BlockStatement Block { get; }

    internal FunDeclareStatement(string identifier, List<ParamExpression> parameters, System.Type returnType, BlockStatement block)
    {
        Identifier = identifier;
        ReturnType = returnType;
        Parameters = parameters;
        Block = block;
    }
}
