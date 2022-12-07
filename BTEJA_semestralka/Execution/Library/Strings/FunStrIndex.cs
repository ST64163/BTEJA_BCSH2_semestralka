
using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library.Strings;

internal class FunStrIndex : LibraryFunction
{
    public FunStrIndex() : base("strIndex", typeof(string), new() {
        new Variable("string", typeof(string)),
        new Variable("index", typeof(int)),
    }) {}

    protected override object GetResult(ExecutionContext context)
    {
        Expression strExpr = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        Expression indexExpr = Parameters[1].Expression
            ?? throw new Exception("Unexpected behaviour");
        string str = (string)strExpr.Execute(context);
        int index = (int)indexExpr.Execute(context);
        return str[index].ToString();
    }
}
