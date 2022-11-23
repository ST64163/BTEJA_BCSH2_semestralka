using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements;

internal class IfStatement : Statement
{
    internal List<(Expression?, Statement)> Conditionments { get; }

    internal IfStatement(List<(Expression?, Statement)> conditionments)
    {
        Conditionments = conditionments;
    }
}
