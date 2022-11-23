namespace InterpreterSK.Tokens;

internal class StringToken : Token
{
    internal StringToken(string value) : base(TokenType.dataString, value) { }
}
