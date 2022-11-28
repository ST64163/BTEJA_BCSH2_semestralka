using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library.Conversions;

internal class FunBoolToStr : LibraryFunction
{
    public FunBoolToStr() : base("boolToStr", typeof(string), new() {new Variable("bool", typeof(bool), null)}) {}

    protected override object GetResult(ExecutionContext context)
    {
        Expression expression = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        bool boolean = (bool)expression.Execute(context);
        return boolean.ToString();
    }
}
