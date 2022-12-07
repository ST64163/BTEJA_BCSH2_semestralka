﻿using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Expressions.Level6;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Statements.Variables;

internal class VarDeclareStatement : VarStatement
{
    internal Type Datatype { get; }
    internal Expression? Expression { get; private set; }

    internal VarDeclareStatement(string identifier, Type datatype, Expression? expression, int rowNumber) 
        : base(identifier, rowNumber)
    {
        Datatype = datatype;
        Expression = expression;
    }

    protected override void Operation(Execution.ExecutionContext context, bool execute) 
    {
        Type? type = Expression?.Analyze(context.CreateInnerContext(context.BranchOwner));
        if (type != null && Datatype != type)
            throw new Exceptions.InvalidDatatypeException("Cannot assign expression to variable of different datatype", RowNumber);
        CheckNames(context);
        if (execute && Expression != null)
        {
            object value = Expression.Execute(context);
            Expression = new LiteralExpression(value, RowNumber);
        }
        Variable variable = new(Identifier, Datatype, Expression, context);
        context.VariableContext.AddVariable(variable);
    }

    private void CheckNames(Execution.ExecutionContext context)
    {
        List<Variable> variables = context.VariableContext.Variables;
        foreach (var variable in variables)
            if (variables.Where(var => var.Identifier == variable.Identifier && var.Context == variable.Context).Count() > 1)
                throw new Exceptions.InvalidSyntaxException($"Cannot declare two functions in the same context with the same name: {variable.Identifier}", RowNumber);
    }
}
