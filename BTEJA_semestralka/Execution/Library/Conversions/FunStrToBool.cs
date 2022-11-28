using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;
using System.Globalization;

namespace InterpreterSK.Execution.Library.Conversions;

internal class FunStrToBool : LibraryFunction
{
    public FunStrToBool() : base("strToBool", typeof(bool), new() {new Variable("string", typeof(string), null)}) {}

    protected override object GetResult(ExecutionContext context)
    {
        Expression expression = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        string str = (string)expression.Execute(context);
        if (str == "true") 
            return true;
        if (str == "false") 
            return false;
        throw new Exceptions.InvalidOperationException($"Parameter {str} cannot be converted to Boolean", expression.RowNumber);
    }
}
