﻿
namespace BTEJA_BCSH2_semestralka.Tokens;

internal class StringToken : Token
{
    internal StringToken(string value) : base(TokenType.dataString, value) { }
}