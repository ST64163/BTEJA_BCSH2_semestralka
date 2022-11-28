using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.AST.Statements.Variables;

internal class VarDeclareStatement : VarStatement
{
    internal Type Datatype { get; }
    internal Expression? Expression { get; }

    internal VarDeclareStatement(string identifier, Type datatype, Expression? expression) : base(identifier)
    {
        Datatype = datatype;
        Expression = expression;
    }

    protected override void Operation(Execution.ExecutionContext context) 
    {
        Type? type = Expression?.Analyze(context.CreateInnerContext(context.BranchOwner));
        if (type != null && Datatype != type)
            throw new Exceptions.InvalidDatatypeException("Cannot assign expression to variable of different datatype", RowNumber);
        Variable variable = new(Identifier, Datatype, Expression);
        context.VariableContext.AddVariable(variable);
    }
}
