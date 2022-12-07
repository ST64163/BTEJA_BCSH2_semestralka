using InterpreterSK.AST.Statements.Block;
using InterpreterSK.Execution;
using InterpreterSK.Execution.Elements;
using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST.Statements.Functions;

internal class FunDeclareStatement : FunStatement
{
    internal List<ParamDeclaration> Parameters { get; }
    internal Type ReturnType { get; }
    internal BlockStatement Block { get; }

    internal FunDeclareStatement(string identifier, List<ParamDeclaration> parameters, 
        Type returnType, BlockStatement block, int rowNumber) : base(identifier, rowNumber)
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
        CheckParamNames();
        Function function = new(Identifier, ReturnType, parameters, Block);
        outerContext.FunctionContext.AddFunction(function, RowNumber);

        ExecutionContext innerContext = outerContext.CreateInnerContext(function);
        parameters.ForEach(variable => innerContext.VariableContext.AddVariable(variable, RowNumber));
        Block.Analyze(innerContext);
        if (!Block.EndsInReturn(innerContext, ReturnType))
            throw new Exceptions.InvalidSyntaxException("Not all paths in function return value", RowNumber);
    }

    internal override object Execute(ExecutionContext context)
    {
        List<Variable> parameters = new();
        Parameters.ForEach(
            parameter => parameters.Add((Variable)parameter.Execute(context)));
        Function function = new(Identifier, ReturnType, parameters, Block);
        context.FunctionContext.AddFunction(function, RowNumber);
        return this;
    }

    private void CheckParamNames() 
    {
        foreach (var param1 in Parameters)
            foreach (var param2 in Parameters)
                if (param1 != param2 && param1.Identifier == param2.Identifier)
                    throw new Exceptions.InvalidSyntaxException($"Cannot declare two parameters with same name: {param2.Identifier}", param2.RowNumber);        
    }

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = false;
        return string.Concat(Enumerable.Repeat("-", level++)) + GetType().Name + ": " + Identifier + "\n"
            + Block.GetToString(level, out bool _);
    }
}
