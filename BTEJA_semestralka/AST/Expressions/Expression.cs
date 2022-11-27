namespace InterpreterSK.AST.Expressions;

internal abstract class Expression : Node 
{
    internal override Type Analyze(Execution.ExecutionContext context)
    {
        if (AnalyzedType == null)
            AnalyzedType = Analyzation(context);
        return AnalyzedType;
    }

    protected abstract Type Analyzation(Execution.ExecutionContext context);
}
