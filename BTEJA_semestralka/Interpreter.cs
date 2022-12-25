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


    private bool printLogs = true;
    public bool PrintLogs { get => printLogs; set => printLogs = value; }

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
        {
            if (sourceCode == null || sourceCode != code || builtProgram == null)
                builtProgram = Rebuild(code);
            else if (printLogs)
                WriteLine("~ Already Built");
        }
    }

    private BlockStatement? Rebuild(string code)
    {
        try
        {
            if (printLogs) 
                WriteLine("~ Build start");
            sourceCode = code;
            lexer.LoadProgram(code, out List<Token> tokens);
            if (printLogs) 
                WriteLine("~ Lexical analysis: OK");
            parser.Parse(tokens, out BlockStatement program);
            if (printLogs) 
                WriteLine("~ Parsing: OK");
            ExecutionContext globalContext = new(this);
            program.Analyze(globalContext);
            if (printLogs) 
                WriteLine("~ Semantic analysis: OK");

            if (printLogs) 
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
            if (printLogs) 
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
