using InterpreterSK.AST.Statements.Block;
using InterpreterSK.Execution.Elements;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Statements.Functions;

internal class FunDeclareStatement : FunStatement
{
    internal List<ParamDeclaration> Parameters { get; }
    internal Type ReturnType { get; }
    internal BlockStatement Block { get; }

    internal FunDeclareStatement(string identifier, List<ParamDeclaration> parameters, 
        Type returnType, BlockStatement block) : base(identifier)
    {
        ReturnType = returnType;
        Parameters = parameters;
        Block = block;
    }

    protected override void Analyzation(ExecutionContext outerContext)
    {

        List<Variable> parameters = new();
        Parameters.ForEach(
            parameter => parameters.Add((Variable)parameter.Execute(outerContext)));
        Function function = new(Identifier, ReturnType, parameters, Block);
        outerContext.FunctionContext.AddFunction(function);

        ExecutionContext innerContext = outerContext.CreateInnerContext(function);
        parameters.ForEach(variable => innerContext.VariableContext.AddVariable(variable));
        Block.Analyze(innerContext); // check if it has return and if they are the right datatype
    }

    internal override object Execute(ExecutionContext context)
    {
        List<Variable> parameters = new();
        Parameters.ForEach(
            parameter => parameters.Add((Variable)parameter.Execute(context)));
        Function function = new(Identifier, ReturnType, parameters, Block);
        context.FunctionContext.AddFunction(function);
        return this;
    }
}
