namespace InterpreterSK.Tokens.ValueTokens;

internal class IdentifierToken : Token
{
    public IdentifierToken(string value, int rowNumber) : base(TokenType.identifier, value, rowNumber) { }
}
