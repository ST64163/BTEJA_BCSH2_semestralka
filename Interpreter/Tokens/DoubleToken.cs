namespace IDE.Interpreter.Tokens;

internal class DoubleToken : Token
{
    internal DoubleToken(double value) : base(TokenType.dataDouble, value) { }
}
