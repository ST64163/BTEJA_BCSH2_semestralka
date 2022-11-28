
namespace InterpreterSK.Exceptions;

internal class InvalidSyntaxException : InterpreterException
{
    public InvalidSyntaxException(string message, int rowNumber) : base("InvalidSyntaxException", rowNumber, message) { }
}
