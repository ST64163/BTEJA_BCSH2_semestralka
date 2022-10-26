
namespace BTEJA_BCSH2_semestralka.LexicalAnalysis.Tokens;

internal class BoolToken : Token
{
    internal BoolToken(bool value) : base(TokenType.dataBool, value) { }
}
