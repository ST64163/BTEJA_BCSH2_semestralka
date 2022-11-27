namespace InterpreterSK.Execution;

internal class ExecutionContext
{
    internal FunctionContext FunctionContext { get; }
    internal VariableContext VariableContext { get; }
    internal ExecutionContext? Global { get; }

    internal ExecutionContext(FunctionContext functions, VariableContext variables, ExecutionContext? global)
    {
        FunctionContext = functions;
        VariableContext = variables;
        Global = global;
    }
}
