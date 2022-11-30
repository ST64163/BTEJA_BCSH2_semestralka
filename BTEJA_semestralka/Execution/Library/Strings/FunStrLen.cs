using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library.Strings;

internal class FunStrLen : LibraryFunction
{
    public FunStrLen() : base("strLen", typeof(int), new() {new Variable("string", typeof(string), null)}) {}

    protected override object GetResult(ExecutionContext context)
    {
        Expression strExpr = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        string str = (string)strExpr.Execute(context);
        return str.Length;
    }
}
