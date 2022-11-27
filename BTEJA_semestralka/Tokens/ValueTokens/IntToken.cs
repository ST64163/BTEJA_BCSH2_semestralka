namespace InterpreterSK.Tokens.ValueTokens;

internal class IntToken : Token
{
    internal IntToken(int value, int rowNumber) : base(TokenType.dataInt, value, rowNumber) { }
}
