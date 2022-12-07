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
        CheckNames(outerContext);
        Function function = new(Identifier, ReturnType, parameters, Block, outerContext);
        outerContext.FunctionContext.AddFunction(function);

        ExecutionContext innerContext = outerContext.CreateInnerContext(function);
        parameters.ForEach(variable => innerContext.VariableContext.AddVariable(variable));
        Block.Analyze(innerContext);
        if (!Block.EndsInReturn(innerContext, ReturnType))
            throw new Exceptions.InvalidSyntaxException("Not all paths in function return value", RowNumber);
    }

    internal override object Execute(ExecutionContext context)
    {
        List<Variable> parameters = new();
        Parameters.ForEach(
            parameter => parameters.Add((Variable)parameter.Execute(context)));
        Function function = new(Identifier, ReturnType, parameters, Block, context);
        context.FunctionContext.AddFunction(function);
        return this;
    }

    private void CheckNames(ExecutionContext context) 
    {
        foreach (var parameter in Parameters)
            if (Parameters.Where(param => parameter.Identifier == param.Identifier).Count() > 1)
                throw new Exceptions.InvalidSyntaxException($"Cannot declare two parameters with same name: {parameter.Identifier}", parameter.RowNumber);
        List<Function> functions = context.FunctionContext.GlobalFunctions;
        foreach (var function in functions)
            if (functions.Where(fun => fun.Identifier == function.Identifier && fun.Context == function.Context).Count() > 1)
                throw new Exceptions.InvalidSyntaxException($"Cannot declare two functions in the same context with the same name: {function.Identifier}", RowNumber);
    }

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = false;
        return string.Concat(Enumerable.Repeat("-", level++)) + GetType().Name + ": " + Identifier + "\n"
            + Block.GetToString(level, out bool _);
    }
}
