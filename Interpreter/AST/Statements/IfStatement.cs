using IDE.Interpreter.AST.Expressions;
using System.Collections.Generic;

namespace IDE.Interpreter.AST.Statements;

internal class IfStatement : Statement
{
    internal List<(Expression?, Statement)> Conditionments { get; }

    internal IfStatement(List<(Expression?, Statement)> conditionments)
    {
        Conditionments = conditionments;
    }
}
