namespace InterpreterSK.Execution.Library.IO;

internal class FunReadLn : LibraryFunction
{
    private readonly Interpreter interpreter;

    public FunReadLn(Interpreter interpreter) 
        : base("readLn", typeof(string), new()) 
    {
        this.interpreter = interpreter;
    }

    protected override object GetResult(ExecutionContext context)
    {
        return interpreter.Read();
    }
}
