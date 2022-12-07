using InterpreterSK.Execution.Elements;
using InterpreterSK.Execution.Library;

namespace InterpreterSK.Execution;

internal class FunctionContext
{
    internal static List<Function>? LibraryFunctions { get; private set; } = null; 
    internal List<Function> GlobalFunctions { get; }
    internal List<Function> LocalFunctions { get; }
    internal List<Function> Functions { get => LocalFunctions.Concat(GlobalFunctions).Concat(LibraryFunctions ?? new()).ToList(); }

    internal FunctionContext(LibraryContext library)
    { 
        if (LibraryFunctions == null)
            LibraryFunctions = library.GlobalFunctions;
        GlobalFunctions = new();
        LocalFunctions = new();
    }

    internal FunctionContext(List<Function> functions)
    {
        GlobalFunctions = functions;
        LocalFunctions = new();
    }

    internal Function GetFunction(string identifier, int rowNumber)
    {
        Function? function = Functions.Find(fun => fun.Identifier == identifier);
        if (function == null)
            throw new Exceptions.InvalidInvocationException($"Calling unknown function: {identifier}", rowNumber);
        return function;
    }

    internal void AddFunction(Function newFunction)
    {
        LocalFunctions.Add(newFunction);
    }

    internal FunctionContext CreateCopy(ExecutionContext context)
    {
        List<Function> oldFunctions = LocalFunctions;
        GlobalFunctions.ForEach(globalFunction => 
        {
            bool add = true;
            foreach (var localFunction in LocalFunctions)
                if (localFunction.Identifier == globalFunction.Identifier)
                {
                    add = false;
                    break;
                }
            if (add)
                oldFunctions.Add(globalFunction);
        });

        List<Function> newFunctions = new();
        oldFunctions.ForEach(
            oldFunction => {
                List<Variable> parameters = new();
                oldFunction.Parameters.ForEach(
                    parameter => parameters.Add(
                        new Variable(parameter.Identifier, parameter.Datatype, null, oldFunction.Context ?? context)));
                newFunctions.Add(
                    new Function(oldFunction.Identifier, oldFunction.Datatype, parameters, oldFunction.Block, oldFunction.Context ?? context));
            });
        return new FunctionContext(newFunctions);
    }
}
