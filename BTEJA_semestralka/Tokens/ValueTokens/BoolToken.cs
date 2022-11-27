namespace InterpreterSK.Tokens.ValueTokens;

internal class BoolToken : Token
{
    internal BoolToken(bool value, int rowNumber) : base(TokenType.dataBool, value, rowNumber) { }
}
