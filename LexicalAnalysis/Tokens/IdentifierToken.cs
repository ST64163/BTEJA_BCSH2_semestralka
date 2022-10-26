
namespace BTEJA_BCSH2_semestralka.LexicalAnalysis.Tokens;

internal class IdentifierToken : Token
{
    public IdentifierToken(string value) : base(TokenType.identifier, value) {}
}
