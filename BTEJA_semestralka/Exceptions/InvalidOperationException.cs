
namespace InterpreterSK.Exceptions;

internal class InvalidOperationException : InterpreterException
{
    public InvalidOperationException(string message, int rowNumber) : base("InvalidOperationException", rowNumber, message) { }
}
