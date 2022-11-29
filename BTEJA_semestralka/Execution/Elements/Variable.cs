
using InterpreterSK.AST.Expressions;

namespace InterpreterSK.Execution.Elements;

internal class Variable : ExecutionElement
{
    internal Expression? Expression { get; set; }

    internal Variable(string identifier, Type datatype, Expression? expression, ExecutionContext context)
        : base(identifier, datatype, context)
    {
        Expression = expression;
    }

    internal Variable(string identifier, Type datatype, Expression? expression)
        : base(identifier, datatype, null)
    {
        Expression = expression;
    }
}
