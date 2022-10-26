
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Variables;

namespace BTEJA_BCSH2_semestralka.SemanticAnalysis.Statements;

internal class VarDeclareStatement : Statement
{
    internal Variable Variable { get; }

    internal VarDeclareStatement(Variable variable)
    {
        Variable = variable;
    }
}
