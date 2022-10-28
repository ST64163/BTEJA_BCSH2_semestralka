namespace IDE.Interpreter.Tokens;

internal class StringToken : Token
{
    internal StringToken(string value) : base(TokenType.dataString, value) { }
}
