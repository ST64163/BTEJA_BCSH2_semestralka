using BTEJA_BCSH2_semestralka.SemanticAnalysis.Variables;
using IDE.Interpreter.AST.Expressions;

namespace IDE.Interpreter.AST.Statements;

internal class ForStatement : Statement
{
    internal Variable Variable { get; }
    internal Expression Start { get; }
    internal Expression End { get; }
    internal Statement Statement { get; }

    public ForStatement(string identifier, Expression start, Expression end, Statement statement)
    {
        Variable = new(identifier, typeof(int), start);
        Start = start;
        End = end;
        Statement = statement;
    }
}
