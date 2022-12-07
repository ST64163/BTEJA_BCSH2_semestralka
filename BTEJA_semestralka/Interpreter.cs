namespace InterpreterSK;

using InterpreterSK.AST.Statements.Block;
using InterpreterSK.LexicalAnalysis;
using InterpreterSK.SemanticAnalysis;
using InterpreterSK.Tokens;
using ExecutionContext = Execution.ExecutionContext;

public delegate void WriteHandler(object sender, string message);
public delegate string ReadHandler(object sender);

public class Interpreter
{
    private readonly Lexer lexer;
    private readonly Parser parser;

    private string? sourceCode;
    private BlockStatement? builtProgram;

    public event WriteHandler? WriteEvent;
    public event ReadHandler? ReadLineEvent;

    public Interpreter()
    {
        lexer = new();
        parser = new();
    }

    public void Interpret(string code)
    {
        if (code != string.Empty)
        {
            if (sourceCode != null && sourceCode == code && builtProgram != null)
                Execute(builtProgram);
            else
            {
                builtProgram = Rebuild(code);
                if (builtProgram != null)
                    Execute(builtProgram);
            }
        }
    }

    public void Build(string code)
    {
        if (code != string.Empty)
            if (sourceCode == null || sourceCode != code || builtProgram == null)
                builtProgram = Rebuild(code);
    }

    private BlockStatement? Rebuild(string code)
    {
        try
        {
            WriteLine("~ Build start");
            sourceCode = code;
            lexer.LoadProgram(code, out List<Token> tokens);
            /*
            tokens.ForEach(token =>
                    {
                        string tokenString = token.ToString();
                        string valueString = "";
                        if (token is IdentifierToken || token is IntToken || token is DoubleToken || token is BoolToken || token is StringToken)
                            valueString = " - " + token.Value?.ToString();
                        if (token is ReservedToken || token is OperatorToken)
                            valueString = " - " + Token.TokenTypeToString.GetValueOrDefault(token.TokenType);
                        Write(tokenString + valueString);
                    });
            */
            WriteLine("~ Lexical analysis: OK");
            parser.Parse(tokens, out BlockStatement program);
            /*
             Write(program.ToString());
             */
            WriteLine("~ Parsing: OK");
            ExecutionContext globalContext = new(this);
            program.Analyze(globalContext);
            WriteLine("~ Semantic analysis: OK");

            WriteLine("~ Build complete");
            return program;
        }
        catch (Exception e)
        {
            sourceCode = null;
            WriteLine(e.Message);
            return null;
        }
    }

    private void Execute(BlockStatement program)
    {
        try
        {
            WriteLine("~ Program: ");
            program.Execute(new(this));
        }
        catch (Exception e)
        {
            WriteLine(e.Message);
        }
    }

    internal string ReadLine()
    {
        string message = ReadLineEvent?.Invoke(this) ?? "";
        return message;
    }

    internal void Write(string message)
    {
        WriteEvent?.Invoke(this, message);
    }

    internal void WriteLine(string message)
    {
        WriteEvent?.Invoke(this, message + "\n");
    }
}
