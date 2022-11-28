using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Statements.Jumps;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Statements.Loops;

internal class WhileStatement : LoopStatement
{
    internal Expression Condition { get; }

    public WhileStatement(Expression condition, Statement statement) : base(statement)
    {
        Condition = condition;
    }

    internal override object Execute(ExecutionContext outerContext)
    {
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);
        while (true)
        {
            object condition = Condition.Execute(innerContext);
            CheckCondition(condition.GetType());
            if (!(bool)condition)
                break;
            object result = Statement.Execute(innerContext);
            if (result is ReturnStatement)
                return result;
            else if (result is BreakStatement)
                break;
        }
        return this;
    }

    protected override void Analyzation(ExecutionContext outerContext)
    {
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);
        Type conditionType = Condition.Analyze(innerContext);
        CheckCondition(conditionType);
        Statement.Analyze(innerContext);
    }

    private void CheckCondition(Type conditionType)
    { 
        if (conditionType != typeof(bool))
            throw new Exceptions.InvalidDatatypeException("Condition must be evaluated as Boolean", RowNumber);
    }
}
