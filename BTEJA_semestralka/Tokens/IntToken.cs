namespace InterpreterSK.Tokens;

internal class IntToken : Token
{
    internal IntToken(int value) : base(TokenType.dataInt, value) { }
}
