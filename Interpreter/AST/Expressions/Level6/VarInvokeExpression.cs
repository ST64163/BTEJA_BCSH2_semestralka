
namespace IDE.Interpreter.AST.Expressions;

internal class VarInvokeExpression : Expression
{
    internal string Identifier { get; }

    public VarInvokeExpression(string identifier) => Identifier = identifier;

    internal override object Evaluate() => throw new System.InvalidOperationException();
}
