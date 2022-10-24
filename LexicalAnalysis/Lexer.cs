
using BTEJA_BCSH2_semestralka.Tokens;
using System.Collections.Generic;

namespace BTEJA_BCSH2_semestralka.LexicalAnalysis;

internal class Lexer
{
    
    internal void LoadProgram(string input, out List<Token> tokens)
    {
        LexerState state = new(input);
        LoadTokens(state);
        tokens = state.Tokens;
    }

    private void LoadTokens(LexerState state)
    {
        string currentWord = "";
        char currentChar;
        Token? oprtr = null;
        bool endOfWord = false;
        do
        {
            currentChar = state.Input[state.Cursor];
            if (char.IsWhiteSpace(currentChar) || char.IsControl(currentChar))
                endOfWord = true;
            else if ((oprtr = GetOperatorToken(currentChar, state)) != null)
                endOfWord = true;

            if (endOfWord)
            {
                state.Tokens.Add(GetToken(currentWord, state));
                if (oprtr != null) 
                {
                    state.Tokens.Add(oprtr);
                    oprtr = null;
                }
                currentWord = "";
                endOfWord = false;
            }
            else
                currentWord += currentChar;
        } while (++state.Cursor < state.Input.Length);
        
        if (currentWord != "")
            state.Tokens.Add(GetToken(currentWord, state));
    }

    private OperatorToken? GetOperatorToken(char currentChar, LexerState state)
    {
        if (char.IsLetterOrDigit(currentChar))
            return null;
        string wanted = currentChar.ToString();
        OperatorToken? oprtr = FindOperator(wanted);
        if (oprtr != null && state.Cursor + 1 < state.Input.Length)
        {
            wanted += state.Input[state.Cursor + 1];
            OperatorToken? pairOperator = FindOperator(wanted);
            if (pairOperator != null)
                oprtr = pairOperator;
        }
        return oprtr;
    }

    private Token GetToken(string currentWord, LexerState state)
    {
        Token? token = FindReservedWord(currentWord);
        if (token == null)
        {
            if (currentWord == "true")
                token = new BoolToken(true);
            else if (currentWord == "false")
                token = new BoolToken(false);
            else if (currentWord.Length > 1 && currentWord.StartsWith('"') && currentWord.EndsWith('"'))
                token = new StringToken(currentWord[1..^1]);
            else if (double.TryParse(currentWord, out double real))
                token = new DoubleToken(real);
            else if (int.TryParse(currentWord, out int whole))
                token = new IntToken(whole);
            else
                token = new IdentifierToken(currentWord);
        }
        return token;
    }

    private OperatorToken? FindOperator(string wanted)
        => (OperatorToken.StringToOperator.TryGetValue(wanted, out TokenType type))
        ? new OperatorToken(type) : null;

    private ReservedToken? FindReservedWord(string wanted)
        => (ReservedToken.StringToReservedWord.TryGetValue(wanted, out TokenType type))
        ? new ReservedToken(type) : null;

    private class LexerState
    {
        internal string Input { get; }
        internal int Cursor { get; set; } = 0;
        internal List<Token> Tokens { get; } = new();

        internal LexerState(string input) => Input = input;
    }
}
