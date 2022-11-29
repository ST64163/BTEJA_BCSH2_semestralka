
namespace InterpreterSK.Exceptions;

internal class StackOverflowException : InterpreterException
{
    public StackOverflowException(string message, int rowNumber) : base("StackOverflowException", rowNumber, message) {}
}
