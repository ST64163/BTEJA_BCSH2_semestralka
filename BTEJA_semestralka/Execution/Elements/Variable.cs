
using InterpreterSK.AST.Expressions;

namespace InterpreterSK.Execution.Elements;

internal class Variable : ExecutionElement
{
    internal Expression? Expression { get; set; }

    internal Variable(string identifier, Type datatype, Expression? expression) : base(identifier, datatype)
    {
        Expression = expression;
    }
}
