using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;
using System.Globalization;

namespace InterpreterSK.Execution.Library.Conversions;

internal class FunStrToInt : LibraryFunction
{
    public FunStrToInt() : base("strToInt", typeof(int), new() {new Variable("string", typeof(string), null)}) {}

    protected override object GetResult(ExecutionContext context)
    {
        Expression expression = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        string str = (string)expression.Execute(context);
        if (!int.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            throw new Exceptions.InvalidOperationException($"Parameter {str} cannot be converted to Int", expression.RowNumber);
        return result;
    }
}
