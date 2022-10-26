
using System.Collections.Generic;

namespace BTEJA_BCSH2_semestralka.LexicalAnalysis.Tokens;

abstract internal class Token
{
    internal TokenType TokenType { get; }
    internal object? Value { get; }

    internal Token(TokenType type, object? value)
    {
        TokenType = type;
        Value = value;
    }

    internal static Dictionary<TokenType, string> TokenTypeToString = new()
    {
        {TokenType.VAR, "var"},
        {TokenType.FUN, "fun"},
        {TokenType.IF, "if"},
        {TokenType.ELSE, "else"},
        {TokenType.FOR, "for"},
        {TokenType.IN, "in"},
        {TokenType.UNTIL, "until"},
        {TokenType.WHILE, "while"},
        {TokenType.DO, "do"},
        {TokenType.BREAK, "break"},
        {TokenType.CONTINUE, "continue"},
        {TokenType.RETURN, "return"},
        {TokenType.Colon, ":"},
        {TokenType.Semicolon, ";"},
        {TokenType.Comma, ","},
        {TokenType.Assign, "="},
        {TokenType.LeftParenth, "("},
        {TokenType.RightParenth, ")"},
        {TokenType.LeftBracket, "{"},
        {TokenType.RightBracket, "}"},
        {TokenType.Or, "||"},
        {TokenType.And, "&&"},
        {TokenType.Equals, "=="},
        {TokenType.NotEquals, "!="},
        {TokenType.LessEquals, "<="},
        {TokenType.GreaterEquals, ">="},
        {TokenType.LessThan, "<"},
        {TokenType.GreaterThan, ">"},
        {TokenType.Plus, "+"},
        {TokenType.Minus, "-"},
        {TokenType.Multiply, "*"},
        {TokenType.Divide, "/"},
        {TokenType.Modulo, "%"},
        {TokenType.identifier, "<identifier>"},
        {TokenType.dataInt, "<int>" },
        {TokenType.dataDouble, "<double>"},
        {TokenType.dataString, "<string>"},
        {TokenType.dataBool, "<bool>"},
    };
}


