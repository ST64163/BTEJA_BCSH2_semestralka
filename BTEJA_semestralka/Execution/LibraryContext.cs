using InterpreterSK.Execution.Library.Conversions;
using InterpreterSK.Execution.Library.IO;
using InterpreterSK.Execution.Library.Strings;

namespace InterpreterSK.Execution;

internal class LibraryContext : FunctionContext
{
    internal LibraryContext(Interpreter interpreter) : base(new())
    {
        Init(interpreter);
    }

    private void Init(Interpreter interpreter)
    {
        Functions.Add(new FunPrint(interpreter));
        Functions.Add(new FunPrintLn(interpreter));
        Functions.Add(new FunReadLn(interpreter));

        Functions.Add(new FunStrLen());
        Functions.Add(new FunStrIndex());

        Functions.Add(new FunIntToStr());
        Functions.Add(new FunDoubleToStr());
        Functions.Add(new FunBoolToStr());

        Functions.Add(new FunStrToBool());
        Functions.Add(new FunStrToInt());
        Functions.Add(new FunStrToDouble());
    }
}
