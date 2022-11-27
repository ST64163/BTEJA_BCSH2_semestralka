using InterpreterSK.Tokens;
using InterpreterSK.Tokens.StaticTokens;
using InterpreterSK.Tokens.ValueTokens;
using System.Globalization;

namespace InterpreterSK.LexicalAnalysis;

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
            if (currentChar.ToString() == Environment.NewLine)
                state.RowNumber++;
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
                return new StringToken(state.Input[start..end], state.RowNumber);
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
                oprtr = FindOperator("" + currentChar + state.Input[state.Cursor + 1], state);
                if (oprtr != null)
                    state.Cursor++;
            }
            if (oprtr == null)
                oprtr = FindOperator("" + currentChar, state);
        }
        return oprtr;
    }

    private Token GetToken(string currentWord, LexerState state)
    {
        Token? token = FindReservedWord(currentWord, state);
        if (token == null)
        {
            if (currentWord == "true")
                token = new BoolToken(true, state.RowNumber);
            else if (currentWord == "false")
                token = new BoolToken(false, state.RowNumber);
            else if (int.TryParse(currentWord, NumberStyles.Any, CultureInfo.InvariantCulture, out int whole))
                token = new IntToken(whole, state.RowNumber);
            else if (double.TryParse(currentWord, NumberStyles.Any, CultureInfo.InvariantCulture, out var real))
                token = new DoubleToken(real, state.RowNumber);
            else
                token = new IdentifierToken(currentWord, state.RowNumber);
        }
        return token;
    }

    private OperatorToken? FindOperator(string wanted, LexerState state)
        => OperatorToken.StringToOperator.TryGetValue(wanted, out TokenType type)
        ? new OperatorToken(type, state.RowNumber) : null;

    private ReservedToken? FindReservedWord(string wanted, LexerState state)
        => ReservedToken.StringToReservedWord.TryGetValue(wanted, out TokenType type)
        ? new ReservedToken(type, state.RowNumber) : null;

    private class LexerState
    {
        internal string Input { get; }
        internal int Cursor { get; set; } = 0;
        internal List<Token> Tokens { get; } = new();
        internal int RowNumber { get; set; } = 0;

        internal LexerState(string input) => Input = input;
    }
}
