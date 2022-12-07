
namespace InterpreterSK.AST.Expressions;

internal abstract class InvokeExpression : Expression
{
    internal string Identifier { get; }

    internal InvokeExpression(string identifier, int rowNumber) : base(rowNumber)
    { 
        Identifier = identifier;
    }

    internal override string GetToString(int level, out bool isLeaf)
    { 
        isLeaf = true;
        return string.Concat(Enumerable.Repeat("-", level)) + GetType().Name + ": " + Identifier + "\n";
    }
}
