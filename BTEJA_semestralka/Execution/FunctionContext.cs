using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution;

internal class FunctionContext
{
    internal static List<Function>? LibraryFunctions { get; private set; } = null; 
    internal List<Function> Functions { get; }

    internal FunctionContext(LibraryContext library)
    { 
        if (LibraryFunctions == null)
            LibraryFunctions = library.Functions;
        Functions = new();
    }

    internal FunctionContext(List<Function> functions)
    {
        Functions = functions;
    }

    internal Function GetFunction(string identifier, int rowNumber)
    {
        Function? function = Functions.Find(fun => fun.Identifier == identifier) 
            ?? LibraryFunctions?.Find(fun => fun.Identifier == identifier);
        if (function == null)
            throw new Exceptions.InvalidInvocationException($"Calling unknown function: {identifier}", rowNumber);
        return function;
    }

    internal void AddFunction(Function newFunction)
    {
        foreach (var oldFunction in Functions)
            if (oldFunction.Identifier == newFunction.Identifier)
            { 
                Functions.Remove(oldFunction);
                break;
            }
        Functions.Add(newFunction);
    }

    internal FunctionContext CreateCopy(ExecutionContext context)
    {
        List<Function> functions = new();
        Functions.ForEach(
            function => {
                List<Variable> parameters = new();
                function.Parameters.ForEach(
                    parameter => parameters.Add(
                        new Variable(parameter.Identifier, parameter.Datatype, null, function.Context ?? context)));
                functions.Add(
                    new Function(function.Identifier, function.Datatype, parameters, function.Block, function.Context ?? context));
            });
        return new FunctionContext(functions);
    }
}
