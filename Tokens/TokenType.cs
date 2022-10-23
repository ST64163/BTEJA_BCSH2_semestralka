
namespace BTEJA_BCSH2_semestralka.Tokens;

internal enum TokenType
{
    VAR, FUN,
    IF, ELSE, FOR, IN, UNTIL, WHILE, DO,
    BREAK, CONTINUE, RETURN, TRUE, FALSE,
    Colon, Semicolon, Comma, Assign,
    LeftParenth, RightParenth, LeftBracket, RightBracket,
    Or, And,
    Equals, NotEquals, LessEquals, GreaterEquals, LessThan, GreaterThan,
    Plus, Minus, Multiply, Divide, Modulo,
    identifier, dataInt, dataDouble, dataString,
    dataBool = TRUE | FALSE
}
