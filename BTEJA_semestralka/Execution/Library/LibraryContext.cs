using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library;

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
                        GlobalFunctions.Add((LibraryFunction)function);
                }
            });
    }
}
