
namespace InterpreterSK.Exceptions;

internal class InvalidInvocationException : InterpreterException
{
    public InvalidInvocationException(string message, int rowNumber) : base("InvalidInvocationException", rowNumber, message) { }
}
