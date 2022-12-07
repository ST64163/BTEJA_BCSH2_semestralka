using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library.IO;

internal class FunPrintLn : LibraryFunction
{
    private readonly Interpreter interpreter;

    public FunPrintLn(Interpreter interpreter) 
        : base("printLn", typeof(void), new() { new Variable("message", typeof(string)) }) 
    {
        this.interpreter = interpreter;
    }

    protected override object GetResult(ExecutionContext context)
    {
        Expression expression = Parameters[0].Expression
            ?? throw new Exception("Unexpected behaviour");
        string message = (string)expression.Execute(context);
        interpreter.WriteLine(message);
        return "OK";
    }
}
