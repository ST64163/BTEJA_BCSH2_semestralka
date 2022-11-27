
namespace InterpreterSK.AST.Expressions;

internal abstract class InvokeExpression : Expression
{
    internal string Identifier { get; }

    internal InvokeExpression(string identifier)
    { 
        Identifier = identifier;
    }
}
