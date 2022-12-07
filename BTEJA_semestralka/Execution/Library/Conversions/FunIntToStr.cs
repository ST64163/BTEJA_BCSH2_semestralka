using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library.Conversions;

internal class FunIntToStr : LibraryFunction
{
    public FunIntToStr() : base("intToStr", typeof(string), new() {new Variable("int", typeof(int))}) {}

    protected override object GetResult(ExecutionContext context)
    {
        Expression expression = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        int number = (int)expression.Execute(context);
        return number.ToString();
    }
}
