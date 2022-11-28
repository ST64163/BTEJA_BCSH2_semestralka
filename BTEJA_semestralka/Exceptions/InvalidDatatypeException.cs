
namespace InterpreterSK.Exceptions;

internal class InvalidDatatypeException : InterpreterException
{
    public InvalidDatatypeException(string message, int rowNumber) : base("InvalidDatatypeException", rowNumber, message) { }
}
