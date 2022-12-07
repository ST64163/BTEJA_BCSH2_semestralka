using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;
using System.Globalization;
using System.Text.RegularExpressions;

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
            throw new Exceptions.InvalidOperationException($"String \"{str}\" cannot be converted to Int", expression.RowNumber);
        if (!Regex.Match(str, "[0-9]+").Success)
            throw new Exceptions.InvalidDatatypeException($"Invalid number format", expression.RowNumber);
        return result;
    }
}
