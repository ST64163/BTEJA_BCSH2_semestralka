using System.Collections.Generic;
using System.Windows.Documents;

namespace IDE.Interpreter.AST.Statements;

internal class BlockStatement : Statement
{
    internal List<Statement> Statements { get; }

    internal BlockStatement(List<Statement> statements)
    {
        Statements = statements;
    }
}
