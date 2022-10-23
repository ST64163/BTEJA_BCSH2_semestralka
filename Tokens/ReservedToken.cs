
using System.Collections.Generic;

namespace BTEJA_BCSH2_semestralka.Tokens;

internal class ReservedToken : Token
{
    internal ReservedToken(TokenType type) : base(type, null) { }

    internal Dictionary<TokenType, string> TokenTypeToString
        = new() {
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
            {TokenType.TRUE, "true"},
            {TokenType.FALSE, "false"},
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
        };
}
