using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Expressions.Level6;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Statements.Variables;

internal class VarAssignStatement : VarStatement
{
    internal Expression Expression { get; }

    public VarAssignStatement(string identifier, Expression expression, int rowNumber) : base(identifier, rowNumber)
    {
        Expression = expression;
    }

    protected override void Operation(Execution.ExecutionContext context, bool execute) 
    {
        Variable variable = context.VariableContext.GetVariable(Identifier, RowNumber);
        Type type = Expression.Analyze(context.CreateInnerContext(context.BranchOwner));
        if (variable.Datatype != type)
            throw new Exceptions.InvalidDatatypeException("Cannot assign expression to variable of different datatype", RowNumber);
        if (execute)
        {
            object value = Expression.Execute(context);
            variable.Expression = new LiteralExpression(value, RowNumber);
        }
        else
            variable.Expression = Expression;
    }
}
