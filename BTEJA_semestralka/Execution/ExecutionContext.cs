
namespace InterpreterSK.Execution;

internal class ExecutionContext
{
    internal FunctionContext FunctionContext { get; }
    internal VariableContext VariableContext { get; }
    internal ExecutionContext? OuterContext { get; }
    internal object BranchOwner { get; }
    internal int RepetitionLimit { get; } = 1000;

    internal ExecutionContext(FunctionContext functionContext, VariableContext variableContext, 
        ExecutionContext outerContext, object branchOwner)
    {
        FunctionContext = functionContext;
        VariableContext = variableContext;
        OuterContext = outerContext;
        BranchOwner = branchOwner;
    }

    internal ExecutionContext(Interpreter interpreter)
    {
        FunctionContext = new LibraryContext(interpreter);
        VariableContext = new(new());
        OuterContext = null;
        BranchOwner = interpreter;
    }

    internal ExecutionContext CreateInnerContext(object branchOwner)
    {
        FunctionContext functionContext = FunctionContext.CreateCopy(this);
        VariableContext variableContext = VariableContext.CreateCopy(this);
        return new(functionContext, variableContext, this, branchOwner);
    }
}
