
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Functions;

namespace BTEJA_BCSH2_semestralka.SemanticAnalysis.Statements;

internal class FunDeclareStatement : Statement
{
    internal Function Function { get; }

    internal FunDeclareStatement(Function function)
    {
        Function = function;
    }
}
