namespace InterpreterSK.Tokens.ValueTokens;

internal class StringToken : Token
{
    internal StringToken(string value, int rowNumber) : base(TokenType.dataString, value, rowNumber) { }
}
