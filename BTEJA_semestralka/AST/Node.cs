using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST;

internal abstract class Node
{
    protected Type? AnalyzedType { get; set; } = null;
    internal int RowNumber { get; set; } = -1;

    internal abstract Type Analyze(ExecutionContext context);
    internal abstract object Execute(ExecutionContext context);
}
