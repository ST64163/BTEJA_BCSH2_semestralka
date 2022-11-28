using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution;

internal class FunctionContext
{
    internal List<Function> Functions { get; }

    internal FunctionContext(List<Function> functions)
    {
        Functions = functions;
    }

    internal Function GetFunction(string identifier, int rowNumber)
    {
        Function? function = Functions.Find(fun => fun.Identifier == identifier);
        if (function == null)
            throw new Exceptions.InvalidInvocationException($"Calling unknown function: {identifier}", rowNumber);
        return function;
    }

    internal void AddFunction(Function function) 
        => Functions.Add(function);

    internal FunctionContext CreateCopy()
    {
        List<Function> functions = new();
        Functions.ForEach(
            function => {
                List<Variable> parameters = new();
                function.Parameters.ForEach(
                    parameter => parameters.Add(
                        new Variable(parameter.Identifier, parameter.Datatype, parameter.Expression)));
                functions.Add(
                    new Function(function.Identifier, function.Datatype, parameters, function.Block));
            });
        return new FunctionContext(functions);
    }
}
