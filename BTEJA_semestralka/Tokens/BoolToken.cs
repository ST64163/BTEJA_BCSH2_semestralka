namespace InterpreterSK.Tokens;

internal class BoolToken : Token
{
    internal BoolToken(bool value) : base(TokenType.dataBool, value) { }
}
