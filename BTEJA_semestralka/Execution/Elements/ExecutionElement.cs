
namespace InterpreterSK.Execution.Elements;

internal abstract class ExecutionElement
{
    internal string Identifier { get; }
    internal Type Datatype { get; }

    internal ExecutionElement(string identifier, Type datatype)
    {
        Identifier = identifier;
        Datatype = datatype;
    }
}
