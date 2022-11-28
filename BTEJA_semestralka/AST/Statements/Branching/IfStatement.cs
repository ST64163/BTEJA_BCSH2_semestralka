using InterpreterSK.AST.Expressions;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Statements.Branching;

internal class IfStatement : Statement
{
    internal List<(Expression?, Statement)> Conditionments { get; }

    internal IfStatement(List<(Expression?, Statement)> conditionments)
    {
        Conditionments = conditionments;
    }

    protected override void Analyzation(ExecutionContext outerContext)
    {
        foreach ((Expression?, Statement) pair in Conditionments)
        {
            Expression? expression = pair.Item1;
            Statement statement = pair.Item2;
            ExecutionContext innerContext = outerContext.CreateInnerContext(outerContext.BranchOwner);

            Type condition = expression?.Analyze(innerContext) ?? typeof(bool);
            CheckCondition(condition);
            statement.Analyze(innerContext);
        }
    }

    internal override object Execute(ExecutionContext outerContext)
    {
        foreach ((Expression?, Statement) pair in Conditionments)
        {
            Expression? expression = pair.Item1;
            Statement statement = pair.Item2;
            ExecutionContext innerContext = outerContext.CreateInnerContext(outerContext.BranchOwner);

            object condition = expression?.Execute(innerContext) ?? true;
            CheckCondition(condition.GetType());
            if ((bool)condition)
            {
                object result = statement.Execute(innerContext);
                if (result is JumpStatement)
                    return result;
            }
        }
        return this;
    }

    private void CheckCondition(Type conditionType)
    {
        if (conditionType != typeof(bool))
            throw new Exceptions.InvalidDatatypeException("Condition must be evaluated as Boolean", RowNumber);
    }
}
