using InterpreterSK.AST.Expressions;

namespace InterpreterSK.AST.Statements.Variables;

internal class VarAssignStatement : Statement
{
    internal string Identifier { get; }
    internal Expression Expression { get; }

    public VarAssignStatement(string identifier, Expression expression)
    {
        Identifier = identifier;
        Expression = expression;
    }

    internal override object Execute(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }

    protected override void Analyzation(Execution.ExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
