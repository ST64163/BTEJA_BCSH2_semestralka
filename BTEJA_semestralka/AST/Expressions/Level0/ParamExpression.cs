namespace InterpreterSK.AST.Expressions.Level0;

internal class ParamExpression : Expression
{
    internal string Identifier { get; }
    internal Type Type { get; }

    public ParamExpression(string identifier, Type type)
    {
        Identifier = identifier;
        Type = type;
    }

    internal override object Evaluate() => throw new InvalidOperationException();
}
