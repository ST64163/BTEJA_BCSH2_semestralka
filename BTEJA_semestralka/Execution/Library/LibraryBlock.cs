
using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Statements.Block;
using InterpreterSK.AST.Statements.Jumps;

namespace InterpreterSK.Execution.Library;

delegate object GetResultCallback(ExecutionContext context);

internal sealed class LibraryBlock : BlockStatement
{
    private readonly Type datatype;
    internal GetResultCallback? GetResult { get; set; }

    internal LibraryBlock(Type datatype) : base(new())
    {
        this.datatype = datatype;
    }

    internal override Type Analyze(ExecutionContext context)
    {
        if (AnalyzedType == null)
            AnalyzedType = datatype;
        return AnalyzedType;
    }

    internal override object Execute(ExecutionContext context)
    {
        if (GetResult == null)
            throw new Exception("Unexpected behaviour");
        object value = GetResult(context);
        return new ReturnStatement(new LiteralExpression(value));
    }
}
