
namespace BTEJA_BCSH2_semestralka.LexicalAnalysis.Tokens;

internal class DoubleToken : Token
{
    internal DoubleToken(double value) : base(TokenType.dataDouble, value) { }
}
