
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library;

internal abstract class LibraryFunction : Function
{
    internal LibraryFunction(string identifier, Type datatype, List<Variable> parameters) 
        : base(identifier, datatype, parameters, new LibraryBlock(datatype)) 
    {
        GetResultCallback callback = GetResult;
        ((LibraryBlock)Block).GetResult = callback;
    }

    protected abstract object GetResult(ExecutionContext context);
}
