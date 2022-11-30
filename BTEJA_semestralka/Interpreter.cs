namespace InterpreterSK;

using InterpreterSK.AST.Statements.Block;
using InterpreterSK.Execution;
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
    public event ReadHandler? ReadEvent;

    public Interpreter()
    {
        lexer = new();
        parser = new();
    }

    public void Interpret(string code)
    {
        if (code == string.Empty)
            return;
        if (sourceCode != null && sourceCode == code && builtProgram != null)
        {
            Execute(builtProgram);
        }
        else
        {
            builtProgram = Build(code);
            if (builtProgram != null) 
                Execute(builtProgram);
        }
    }

    public void Debug(string code)
    {
        if (code == string.Empty)
            return;
        if (sourceCode == null || sourceCode != code || builtProgram == null)
            builtProgram = Build(code);
    }

    private BlockStatement? Build(string code)
    {
        try
        {
            sourceCode = code;
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
            parser.Parse(tokens, out BlockStatement program);
            Write("Parser: OK");
            ExecutionContext globalContext = new(this);
            program.Analyze(globalContext);
            Write("Analysis: OK");

            Write("Build complete");
            return program;
        }
        catch (Exception e)
        {
            sourceCode = null;
            Write(e.Message);
            return null;
        }
    }

    private void Execute(BlockStatement program)
    {
        try
        {
            program.Execute(new(this));
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
