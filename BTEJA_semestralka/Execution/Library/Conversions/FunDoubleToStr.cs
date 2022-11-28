using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library.Conversions;

internal class FunDoubleToStr : LibraryFunction
{
    public FunDoubleToStr() : base("doubleToStr", typeof(string), new() {new Variable("double", typeof(double), null)}) {}

    protected override object GetResult(ExecutionContext context)
    {
        Expression expression = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        double number = (double)expression.Execute(context);
        return number.ToString();
    }
}
