
using System.Collections.Generic;
using System.Windows.Documents;

namespace BTEJA_BCSH2_semestralka.SemanticAnalysis.Statements;

internal class BlockStatement : Statement
{
    internal List<Statement> Statements { get; }

    internal BlockStatement(List<Statement> statements)
    { 
        Statements = statements;
    }
}
