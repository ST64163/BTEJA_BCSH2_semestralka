
using InterpreterSK.Execution.Library;

namespace InterpreterSK.Execution;

internal class ExecutionContext
{
    internal FunctionContext FunctionContext { get; }
    internal VariableContext VariableContext { get; }
    internal ExecutionContext? OuterContext { get; }
    internal object BranchOwner { get; }
    internal int RepetitionLimit { get; } = 1000;

    private ExecutionContext(object branchOwner, FunctionContext functionContext,
        VariableContext variableContext, ExecutionContext? outerContext)
    {
        FunctionContext = functionContext;
        VariableContext = variableContext;
        OuterContext = outerContext;
        BranchOwner = branchOwner;
    }

    internal ExecutionContext(FunctionContext functionContext, VariableContext variableContext,
        ExecutionContext outerContext, object branchOwner) 
        : this(branchOwner, functionContext, variableContext, outerContext) {}

    internal ExecutionContext(Interpreter interpreter) 
        : this(interpreter, new(new LibraryContext(interpreter)), new(new()), null) {}

    internal ExecutionContext CreateInnerContext(object branchOwner)
    {
        FunctionContext functionContext = FunctionContext.CreateCopy(this);
        VariableContext variableContext = VariableContext.CreateCopy(this);
        return new(functionContext, variableContext, this, branchOwner);
    }
}
