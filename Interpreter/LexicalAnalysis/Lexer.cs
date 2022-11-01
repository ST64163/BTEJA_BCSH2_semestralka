using IDE.Interpreter.Tokens;
using System.Collections.Generic;
using System.Globalization;

namespace IDE.Interpreter.LexicalAnalysis;

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
        Token? newWord = null;
        bool endOfWord = false;
        do
        {
            currentChar = state.Input[state.Cursor];
            if (char.IsWhiteSpace(currentChar) || char.IsControl(currentChar))
                endOfWord = true;
            else if (!char.IsLetterOrDigit(currentChar))
            {
                if ((newWord = GetStringToken(currentChar, state)) != null)
                    endOfWord = true;
                else if ((newWord = GetOperatorToken(currentChar, state)) != null)
                    endOfWord = true;
            }

            if (endOfWord)
            {
                if (currentWord != "")
                    state.Tokens.Add(GetToken(currentWord, state));
                if (newWord != null)
                {
                    state.Tokens.Add(newWord);
                    newWord = null;
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

    private StringToken? GetStringToken(char currentChar, LexerState state)
    {
        if (currentChar == '"')
        {
            int start = state.Cursor + 1;
            int end = state.Input.IndexOf('"', start);
            if (end != -1)
            {
                state.Cursor = end;
                return new StringToken(state.Input[start..end]);
            }
        }
        return null;
    }

    private OperatorToken? GetOperatorToken(char currentChar, LexerState state)
    {
        OperatorToken? oprtr = null;
        if (!char.IsLetterOrDigit(currentChar))
        {
            if (state.Cursor + 1 < state.Input.Length)
            {
                oprtr = FindOperator("" + currentChar + state.Input[state.Cursor + 1]);
                if (oprtr != null)
                    state.Cursor++;
            }
            if (oprtr == null)
                oprtr = FindOperator("" + currentChar);
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
            else if (int.TryParse(currentWord, NumberStyles.Any, CultureInfo.InvariantCulture, out int whole))
                token = new IntToken(whole);
            else if (double.TryParse(currentWord, NumberStyles.Any, CultureInfo.InvariantCulture, out var real))
                token = new DoubleToken(real);
            else
                token = new IdentifierToken(currentWord);
        }
        return token;
    }

    private OperatorToken? FindOperator(string wanted)
        => OperatorToken.StringToOperator.TryGetValue(wanted, out TokenType type)
        ? new OperatorToken(type) : null;

    private ReservedToken? FindReservedWord(string wanted)
        => ReservedToken.StringToReservedWord.TryGetValue(wanted, out TokenType type)
        ? new ReservedToken(type) : null;

    private class LexerState
    {
        internal string Input { get; }
        internal int Cursor { get; set; } = 0;
        internal List<Token> Tokens { get; } = new();

        internal LexerState(string input) => Input = input;
    }
}
