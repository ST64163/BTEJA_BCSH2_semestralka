using InterpreterSK.Execution.Elements;
using InterpreterSK.Execution.Library;

namespace InterpreterSK.Execution;

internal class LibraryContext : FunctionContext
{
    internal LibraryContext(Interpreter interpreter) : base(new List<Function>())
    {
        GetType().Assembly.GetTypes()
            .Where(type => type.IsClass && type.IsSubclassOf(typeof(LibraryFunction))).ToList()
            .ForEach(type =>
            {
                if (type.Namespace != null)
                {
                    object? function = type.Namespace.EndsWith("IO")
                        ? Activator.CreateInstance(type, interpreter)
                        : Activator.CreateInstance(type);
                    if (function != null)
                        Functions.Add((LibraryFunction)function);
                }
            });
    }
}
