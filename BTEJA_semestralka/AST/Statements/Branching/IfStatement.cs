using InterpreterSK.AST.Expressions;
using System.Diagnostics;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Statements.Branching;

internal class IfStatement : Statement
{
    internal List<(Expression?, Statement)> Conditionments { get; }

    internal IfStatement(List<(Expression?, Statement)> conditionments, int rowNumber) : base(rowNumber)
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
                break;
            }
        }
        return this;
    }

    private void CheckCondition(Type conditionType)
    {
        if (conditionType != typeof(bool))
            throw new Exceptions.InvalidDatatypeException("Condition must be evaluated as Boolean", RowNumber);
    }

    internal override bool EndsInReturn(ExecutionContext outerContext, Type datatype)
    {
        bool hasReturn = true;
        foreach ((Expression?, Statement) pair in Conditionments)
        {
            Statement statement = pair.Item2;
            ExecutionContext innerContext = outerContext.CreateInnerContext(outerContext.BranchOwner);
            if (!statement.EndsInReturn(innerContext, datatype))
            { 
                hasReturn = false;
                break;
            }
        }
        return hasReturn;
    }


    internal override string GetToString(int level, out bool isLeaf)
    { 
        isLeaf = false;
        string toString = string.Concat(Enumerable.Repeat("-", level++)) + GetType().Name + "\n";
        Conditionments.ForEach(pair => 
            toString += pair.Item1?.GetToString(level, out bool _) + "\n" 
                + pair.Item2.GetToString(level, out bool _));
        return toString.Remove(toString.Length - 1);
    }
}
