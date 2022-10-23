
namespace BTEJA_BCSH2_semestralka.Tokens;

abstract internal class Token
{
    internal TokenType TokenType { get; }
    internal object? Value { get; }

    internal Token(TokenType type, object? value)
    {
        TokenType = type;
        Value = value;
    }
}


