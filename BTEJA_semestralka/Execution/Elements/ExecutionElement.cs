
namespace InterpreterSK.Execution.Elements;

internal abstract class ExecutionElement
{
    internal string Identifier { get; }
    internal Type Datatype { get; }
    internal ExecutionContext? Context { get; }

    internal ExecutionElement(string identifier, Type datatype, ExecutionContext? context)
    {
        Identifier = identifier;
        Datatype = datatype;
        Context = context;
    }
}
