
namespace IDE.Interpreter.AST.Expressions;

internal class ParamExpression : Expression
{
    internal string Identifier { get; }
    internal System.Type Type { get; }

    public ParamExpression(string identifier, System.Type type)
    {
        Identifier = identifier;
        Type = type;
    }

    internal override object Evaluate() => throw new System.InvalidOperationException();
}
