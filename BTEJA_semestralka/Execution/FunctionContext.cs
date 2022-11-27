using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution;

internal class FunctionContext
{
    internal List<Function> Functions { get; }

    internal FunctionContext(List<Function> functions)
    {
        Functions = functions;
    }

    internal Function GetFunction(string identifier)
    {
        Function? function = Functions.Find(fun => fun.Identifier == identifier);
        if (function == null)
            throw new Exceptions.InvalidInvocationException($"Calling unknown function: {identifier}");
        return function;
    }
}
