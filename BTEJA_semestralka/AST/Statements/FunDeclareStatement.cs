using ParamExpression = InterpreterSK.AST.Expressions.Level0.ParamExpression;

namespace InterpreterSK.AST.Statements;

internal class FunDeclareStatement : Statement
{
    internal string Identifier { get; }
    internal List<ParamExpression> Parameters { get; }
    internal Type ReturnType { get; }
    internal BlockStatement Block { get; }

    internal FunDeclareStatement(string identifier, List<ParamExpression> parameters, Type returnType, BlockStatement block)
    {
        Identifier = identifier;
        ReturnType = returnType;
        Parameters = parameters;
        Block = block;
    }
}
