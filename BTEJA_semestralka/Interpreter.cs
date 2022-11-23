namespace InterpreterSK;

using InterpreterSK.AST.Statements;
using InterpreterSK.LexicalAnalysis;
using InterpreterSK.SemanticAnalysis;
using InterpreterSK.Tokens;

public delegate void WriteHandler(object sender, string message);
public delegate string ReadHandler(object sender);

public class Interpreter
{
    private readonly Lexer lexer;
    private readonly Parser parser;

    public event WriteHandler? WriteEvent;
    public event ReadHandler? ReadEvent;

    public Interpreter()
    {
        lexer = new();
        parser = new();
    }

    public void Interpret(string code)
    {
        try
        {
            lexer.LoadProgram(code, out List<Token> tokens);
            /*
                tokens.ForEach(token =>
                    {
                        string tokenString = token.ToString();
                        string valueString = "";
                        if (token is IdentifierToken || token is IntToken || token is DoubleToken || token is BoolToken || token is StringToken)
                            valueString = " - " + token.Value.ToString();
                        if (token is ReservedToken || token is OperatorToken)
                            valueString = " - " + Token.TokenTypeToString.GetValueOrDefault(token.TokenType);
                        Write(tokenString + valueString);
                    });
                */
            Write("Lexer: OK");
            parser.Parse(tokens, out List<Statement> statements);
            Write("Parser: OK");
        }
        catch (Exception e)
        {
            Write(e.Message);
        }
    }

    internal string Read()
    {
        string message = "";
        if (ReadEvent != null)
            message = ReadEvent.Invoke(this);
        return message;
    }

    internal void Write(string message) => WriteEvent?.Invoke(this, message);
}
