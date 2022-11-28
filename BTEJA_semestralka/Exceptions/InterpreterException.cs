
namespace InterpreterSK.Exceptions;

internal abstract class InterpreterException : Exception
{
    internal InterpreterException(string name, int rowNumber, string message) : base($"{rowNumber}: {name}: {message}") { }
}
