using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.FlowControl;

internal class IfStatement : Statement
{
    internal List<(Expression?, Statement)> Conditionments { get; }

    internal IfStatement(List<(Expression?, Statement)> conditionments)
    {
        Conditionments = conditionments;
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
