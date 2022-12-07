
using InterpreterSK.AST.Expressions;

namespace InterpreterSK.Execution.Elements;

internal class Variable : ExecutionElement
{
    internal Expression? Expression { get; set; }
    internal bool IsConstant { get; }

    internal Variable(string identifier, Type datatype, Expression? expression, bool isConstant)
        : base(identifier, datatype)
    {
        Expression = expression;
        IsConstant = isConstant;
    }

    internal Variable(string identifier, Type datatype)
        : base(identifier, datatype)
    {
        Expression = null;
        IsConstant = true;
    }
}
