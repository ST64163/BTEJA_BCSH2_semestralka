using InterpreterSK.AST.Expressions;
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
        if (execute && Expression != null)
        {
            object value = Expression.Execute(context);
            Expression = new LiteralExpression(value, RowNumber);
        }
        Variable variable = new(Identifier, Datatype, Expression, false);
        context.VariableContext.AddVariable(variable, RowNumber);
    }
}
