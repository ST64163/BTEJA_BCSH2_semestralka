namespace InterpreterSK.Tokens.ValueTokens;

internal class DoubleToken : Token
{
    internal DoubleToken(double value, int rowNumber) : base(TokenType.dataDouble, value, rowNumber) { }
}
