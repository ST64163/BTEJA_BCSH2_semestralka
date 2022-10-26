
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Expressions;
using System.Collections.Generic;

namespace BTEJA_BCSH2_semestralka.SemanticAnalysis.Statements;

internal class IfStatement : Statement
{
    internal List<(Expression?, Statement)> Conditionments { get; }

    internal IfStatement(List<(Expression?, Statement)> conditionments)
    { 
        Conditionments = conditionments;
    }
}
