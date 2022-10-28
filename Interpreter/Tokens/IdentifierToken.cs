namespace IDE.Interpreter.Tokens;

internal class IdentifierToken : Token
{
    public IdentifierToken(string value) : base(TokenType.identifier, value) { }
}
