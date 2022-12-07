using ExecutionContext = InterpreterSK.Execution.ExecutionContext;

namespace InterpreterSK.AST;

internal abstract class Node
{
    protected Type? AnalyzedType { get; set; } = null;
    internal int RowNumber { get; set; } = -1;

    internal Node(int rowNumber)
    {
        RowNumber = rowNumber;
    }

    internal abstract Type Analyze(ExecutionContext context);
    internal abstract object Execute(ExecutionContext context);

    internal abstract string GetToString(int level, out bool isLeaf);

    /*
    public override string ToString()
        => GetToString(1, out bool _);
    */
}
