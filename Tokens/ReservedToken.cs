
using System.Collections.Generic;

namespace BTEJA_BCSH2_semestralka.Tokens;

internal class ReservedToken : Token
{
    internal ReservedToken(TokenType type) : base(type, null) { }

    internal static Dictionary<string, TokenType> StringToReservedWord = new()
    {
        {"var", TokenType.VAR},
        {"fun", TokenType.FUN},
        {"if", TokenType.IF},
        {"else", TokenType.ELSE},
        {"for", TokenType.FOR},
        {"in", TokenType.IN},
        {"until", TokenType.UNTIL},
        {"while", TokenType.WHILE},
        {"do", TokenType.DO},
        {"break", TokenType.BREAK},
        {"continue", TokenType.CONTINUE},
        {"return", TokenType.RETURN},
    };
}
