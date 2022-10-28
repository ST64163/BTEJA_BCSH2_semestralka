
namespace IDE.Interpreter.SemanticAnalysis;

internal class ParsingException : System.Exception
{
    internal ParsingException(string message) : base(message) { }
}
