using InterpreterSK.AST.Expressions;
using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution.Library.IO;

internal class FunPrint : LibraryFunction
{
    private readonly Interpreter interpreter;

    public FunPrint(Interpreter interpreter) 
        : base("print", typeof(void), new() { new Variable("message", typeof(string), null) }) 
    {
        this.interpreter = interpreter;
    }

    protected override object GetResult(ExecutionContext context)
    {
        Expression expression = Parameters[0].Expression 
            ?? throw new Exception("Unexpected behaviour");
        string message = (string)expression.Execute(context);
        interpreter.Write(message);
        return "OK";
    }
}
