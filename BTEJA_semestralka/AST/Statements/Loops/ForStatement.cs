﻿using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Expressions.Level6;
using InterpreterSK.AST.Statements.Jumps;
using InterpreterSK.Execution.Elements;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Statements.Loops;

internal class ForStatement : LoopStatement
{
    internal string VariableIdentifier { get; }
    internal Expression Start { get; }
    internal Expression End { get; }

    public ForStatement(string identifier, Expression start, Expression end, Statement statement) : base(statement)
    {
        VariableIdentifier = new(identifier);
        Start = start;
        End = end;
    }

    protected override void Analyzation(ExecutionContext outerContext)
    {
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);

        Type startType = Start.Analyze(innerContext);
        Type endType = End.Analyze(innerContext);
        CheckStartEnd(startType, endType);
        Variable? variable = innerContext.VariableContext.Variables.Find(variable => variable.Identifier == VariableIdentifier);
        if (variable == null)
        {
            variable = new Variable(VariableIdentifier, typeof(int), Start, innerContext);
            innerContext.VariableContext.AddVariable(variable);
        }
        else
        { 
            CheckVariable(variable.Datatype);
            variable.Expression = Start;
        }
        Statement.Analyze(innerContext);
    }

    internal override object Execute(ExecutionContext outerContext)
    {
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);

        object startObject = Start.Execute(innerContext);
        object endObject = End.Execute(innerContext);
        CheckStartEnd(startObject.GetType(), endObject.GetType());
        int startValue = (int)startObject;
        int endValue = (int)endObject;

        Variable? variable = innerContext.VariableContext.Variables.Find(variable => variable.Identifier == VariableIdentifier);
        if (variable == null)
        {
            variable = new Variable(VariableIdentifier, typeof(int), new IntExpression(startValue), innerContext);
            innerContext.VariableContext.AddVariable(variable);
        }
        else
        { 
            CheckVariable(variable.Datatype);
            variable.Expression = new IntExpression(startValue);
        }

        for (int i = startValue; i < endValue; i++)
        {
            if (CurrentRepetition++ >= outerContext.RepetitionLimit)
                throw new Exceptions.StackOverflowException("For loop reached limit of repeats", RowNumber);
            Expression expression = variable?.Expression 
                ?? throw new Exception("Unexpected behaviour");
            ((IntExpression)expression).Value = i;
            object result = Statement.Execute(innerContext);
            if (result is ReturnStatement)
                return result;
            else if (result is BreakStatement)
                break;
        }
        return this;
    }

    private void CheckStartEnd(Type startType, Type endType)
    {
        if (startType != typeof(int) || endType != typeof(int))
            throw new Exceptions.InvalidDatatypeException("For statement condition is defined only for Int datatypes", RowNumber);
    }

    private void CheckVariable(Type variableType)
    {
        if (variableType != typeof(int))
            throw new Exceptions.InvalidDatatypeException("For statement variable must be of Int datatype", RowNumber);
    }
}
