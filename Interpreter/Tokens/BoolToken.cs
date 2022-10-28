namespace IDE.Interpreter.Tokens;

internal class BoolToken : Token
{
    internal BoolToken(bool value) : base(TokenType.dataBool, value) { }
}
