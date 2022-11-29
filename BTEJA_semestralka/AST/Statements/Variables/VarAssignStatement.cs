using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Statements.Variables;

internal class VarAssignStatement : VarStatement
{
    internal Expression Expression { get; }

    public VarAssignStatement(string identifier, Expression expression) : base(identifier)
    {
        Expression = expression;
    }

    protected override void Operation(Execution.ExecutionContext context, bool _) 
    {
        Variable variable = context.VariableContext.GetVariable(Identifier, RowNumber);
        Type type = Expression.Analyze(context.CreateInnerContext(context.BranchOwner));
        if (variable.Datatype != type)
            throw new Exceptions.InvalidDatatypeException("Cannot assign expression to variable of different datatype", RowNumber);
        variable.Expression = Expression;
    }
}
