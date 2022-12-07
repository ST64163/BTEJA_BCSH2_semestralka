using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Statements.Block;
using InterpreterSK.AST.Statements.Jumps;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Statements.Loops;

internal class DoWhileStatement : LoopStatement
{
    internal Expression Condition { get; }

    public DoWhileStatement(Expression condition, BlockStatement block, int rowNumber) : base(block, rowNumber)
    {
        Condition = condition;
    }

    protected override void Analyzation(ExecutionContext outerContext)
    {
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);
        Statement.Analyze(innerContext);
        Type conditionType = Condition.Analyze(innerContext);
        CheckCondition(conditionType);
    }

    internal override object Execute(ExecutionContext outerContext)
    {
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);
        object condition;
        do
        {
            if (CurrentRepetition++ >= outerContext.RepetitionLimit)
                throw new Exceptions.StackOverflowException("Do-while loop reached limit of repeats", RowNumber);
            object result = Statement.Execute(innerContext);
            if (result is ReturnStatement)
                return result;
            else if (result is BreakStatement)
                break;
            condition = Condition.Execute(innerContext);
            CheckCondition(condition.GetType());
        } while ((bool)condition);
        return this;
    }

    private void CheckCondition(Type conditionType)
    {
        if (conditionType != typeof(bool))
            throw new Exceptions.InvalidDatatypeException("Condition must be evaluated as Boolean", RowNumber);
    }
}
