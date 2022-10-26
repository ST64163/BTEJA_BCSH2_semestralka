
namespace BTEJA_BCSH2_semestralka.LexicalAnalysis.Tokens;

internal enum TokenType
{
    VAR, FUN,
    IF, ELSE, FOR, IN, UNTIL, WHILE, DO,
    BREAK, CONTINUE, RETURN,
    Colon, Semicolon, Comma, Assign,
    LeftParenth, RightParenth, LeftBracket, RightBracket,
    Or, And, Not,
    Equals, NotEquals, LessEquals, GreaterEquals, LessThan, GreaterThan,
    Plus, Minus, Multiply, Divide, Modulo,
    identifier, dataInt, dataDouble, dataString, dataBool
}
