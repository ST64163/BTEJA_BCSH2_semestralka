namespace InterpreterSK.Tokens.StaticTokens;

internal class OperatorToken : Token
{
    public OperatorToken(TokenType type, int rowNumber) : base(type, null, rowNumber) { }

    internal static Dictionary<string, TokenType> StringToOperator = new()
    {
        {":", TokenType.Colon},
        {";", TokenType.Semicolon},
        {",", TokenType.Comma},
        {"=", TokenType.Assign},
        {"(", TokenType.LeftParenth},
        {")", TokenType.RightParenth},
        {"{", TokenType.LeftBracket},
        {"}", TokenType.RightBracket},
        {"||", TokenType.Or},
        {"&&", TokenType.And},
        {"==", TokenType.Equals},
        {"!=", TokenType.NotEquals},
        {"<=", TokenType.LessEquals},
        {">=", TokenType.GreaterEquals},
        {"<", TokenType.LessThan},
        {">", TokenType.GreaterThan},
        {"+", TokenType.Plus},
        {"-", TokenType.Minus},
        {"*", TokenType.Multiply},
        {"/", TokenType.Divide},
        {"%", TokenType.Modulo},
        {"!", TokenType.Not},
    };
}
