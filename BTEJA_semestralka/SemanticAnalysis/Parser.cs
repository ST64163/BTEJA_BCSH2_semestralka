using InterpreterSK.AST.Statements;
using InterpreterSK.AST.Statements.Block;
using InterpreterSK.AST.Statements.Functions;
using InterpreterSK.AST.Statements.Loops;
using InterpreterSK.AST.Statements.Variables;
using InterpreterSK.Exceptions;
using InterpreterSK.Tokens;
using System.Text.RegularExpressions;
using InterpreterSK.AST.Statements.Jumps;
using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Expressions.Level1;
using InterpreterSK.AST.Expressions.Level6;
using InterpreterSK.AST.Expressions.Level5;
using InterpreterSK.AST.Expressions.Level4;
using InterpreterSK.AST.Expressions.Level3;
using InterpreterSK.AST.Expressions.Level2;
using InterpreterSK.AST.Statements.Branching;

namespace InterpreterSK.SemanticAnalysis;

internal class Parser
{

    /**
     * Grammar: statements
     */
    internal void Parse(List<Token> tokens, out BlockStatement program)
    {
        ParserState state = new(tokens);
        program = new(ReadStatements(state), 0);
    }

    /**
     * Grammar: {statement}
    */
    private List<Statement> ReadStatements(ParserState state)
    {
        List<Statement> statements = new();
        Statement? statement;
        int? firstRow = null;
        do
        {
            statement = ReadStatement(state, out int row);
            if (firstRow == null)
                firstRow = row;
            if (statement != null)
                statements.Add(statement);
        } while (statement != null);
        return statements;
    }

    /**
     * variableDeclaration ';' | functionDeclaration | if | for | do | while | block | return | 'break' ';' | 'continue' ';' | variableAssignment ';' | functionInvocation ';'
     */
    private Statement? ReadStatement(ParserState state, out int rowNumber)
    {
        Statement? statement = null;
        rowNumber = -1;

        Token? token = PeekToken(state);
        if (token != null)
        {
            switch (token.TokenType)
            {
                case TokenType.VAR:
                    statement = ReadVarDeclare(state, out int rowVarDeclare);
                    rowNumber = rowVarDeclare;
                    RequireToken(TokenType.Semicolon, state);
                    break;

                case TokenType.FUN:
                    statement = ReadFunDeclare(state, out int rowFunDeclare);
                    rowNumber = rowFunDeclare;
                    break;
                case TokenType.IF:
                    statement = ReadIf(state, out int rowIf);
                    rowNumber = rowIf;
                    break;
                case TokenType.FOR:
                    statement = ReadFor(state, out int rowFor);
                    rowNumber = rowFor;
                    break;
                case TokenType.DO:
                    statement = ReadDoWhile(state, out int rowDoWhile);
                    rowNumber = rowDoWhile;
                    break;
                case TokenType.WHILE:
                    statement = ReadWhile(state, out int rowWhile);
                    rowNumber = rowWhile;
                    break;
                case TokenType.LeftBracket:
                    statement = ReadBlock(state, out int rowBlock);
                    rowNumber = rowBlock;
                    break;
                case TokenType.RETURN:
                    statement = ReadReturn(state, out int rowReturn);
                    rowNumber = rowReturn;
                    break;

                case TokenType.BREAK:
                    RequireToken(TokenType.BREAK, state, out int rowBreak);
                    RequireToken(TokenType.Semicolon, state);
                    rowNumber = rowBreak;
                    statement = new BreakStatement(rowBreak);
                    break;

                case TokenType.CONTINUE:
                    RequireToken(TokenType.CONTINUE, state, out int rowContinue);
                    RequireToken(TokenType.Semicolon, state);
                    rowNumber = rowContinue;
                    statement = new ContinueStatement(rowContinue);
                    break;

                case TokenType.identifier:
                    statement = ReadVarAssignOrFunInvoke(state, out int row);
                    RequireToken(TokenType.Semicolon, state);
                    rowNumber = row;
                    break;
            }
        }

        return statement;
    }

    /**
     * Grammar: 'var' identifier ':' identifier ['=' expression]
     */
    private VarDeclareStatement ReadVarDeclare(ParserState state, out int rowNumber)
    {
        string identifier;
        Type datatype;
        Expression? expression = null;

        RequireToken(TokenType.VAR, state, out int row);
        rowNumber = row;
        identifier = RequireIdentifierToken(state);
        RequireToken(TokenType.Colon, state);
        datatype = RequireDatatypeToken(state);
        if (IsTokenType(state, TokenType.Assign))
        {
            RequireToken(TokenType.Assign, state);
            expression = ReadExpression(state);
        }
        return new VarDeclareStatement(identifier, datatype, expression, rowNumber);
    }

    /**
     * Grammar: 'fun' identifier '(' paramsDeclaration ')' ':' identifier block
     */
    private FunDeclareStatement ReadFunDeclare(ParserState state, out int rowNumber)
    {
        string identifier;
        List<ParamDeclaration> parameters;
        Type datatype;
        BlockStatement block;

        RequireToken(TokenType.FUN, state, out int row);
        rowNumber = row;
        identifier = RequireIdentifierToken(state);
        RequireToken(TokenType.LeftParenth, state);
        parameters = ReadParamsDeclare(state);
        RequireToken(TokenType.RightParenth, state);
        RequireToken(TokenType.Colon, state);
        datatype = RequireDatatypeToken(state);
        block = ReadBlock(state, out int _);

        return new FunDeclareStatement(identifier, parameters, datatype, block, rowNumber);
    }

    /**
     * Grammar: [identifier ':' identifier {',' identifier ':' identifier}]
     */
    private List<ParamDeclaration> ReadParamsDeclare(ParserState state)
    {
        List<ParamDeclaration> parameters = new();
        string identifier;
        Type datatype;

        void RequireParam()
        {
            identifier = RequireIdentifierToken(state, out int row);
            RequireToken(TokenType.Colon, state);
            datatype = RequireDatatypeToken(state);
            parameters.Add(new ParamDeclaration(identifier, datatype, row));
        }

        if (IsTokenType(state, TokenType.identifier))
        { 
            RequireParam();
        }
        while (IsTokenType(state, TokenType.Comma))
        {
            RequireToken(TokenType.Comma, state);
            RequireParam();
        }

        return parameters;
    }

    /**
     * Grammar: 'if' '(' expression ')' statement {'else' 'if' '(' expression ')' statement} ['else' statement]
     */
    private IfStatement ReadIf(ParserState state, out int rowNumber)
    {
        List<(Expression?, Statement)> conditionments = new();
        Expression condition;
        Statement statement;

        (Expression, Statement) ReadIfBlock(out int rowIf)
        {
            RequireToken(TokenType.IF, state, out int row);
            rowIf = row;
            RequireToken(TokenType.LeftParenth, state);
            condition = ReadExpression(state);
            RequireToken(TokenType.RightParenth, state);
            statement = RequireStatement(state);
            return (condition, statement);
        }

        conditionments.Add(ReadIfBlock(out int row));
        rowNumber = row;
        while (IsTokenType(state, TokenType.ELSE))
        {
            RequireToken(TokenType.ELSE, state);
            if (IsTokenType(state, TokenType.IF))
                conditionments.Add(ReadIfBlock(out int _));
            else
            {
                statement = RequireStatement(state);
                conditionments.Add((null, statement));
                break;
            }
        }

        return new IfStatement(conditionments, rowNumber);
    }

    /**
     * Grammar: 'for' '(' identifier 'in' expression 'until' expression  ')' statement
     */
    private ForStatement ReadFor(ParserState state, out int rowNumber)
    {
        string identifier;
        Expression start, end;
        Statement statement;

        RequireToken(TokenType.FOR, state, out int row);
        rowNumber = row;
        RequireToken(TokenType.LeftParenth, state);
        identifier = RequireIdentifierToken(state);
        RequireToken(TokenType.IN, state);
        start = ReadExpression(state);
        RequireToken(TokenType.UNTIL, state);
        end = ReadExpression(state);
        RequireToken(TokenType.RightParenth, state);
        statement = RequireStatement(state);

        return new ForStatement(identifier, start, end, statement, rowNumber);
    }

    /**
     * Grammar: 'while' '(' expression ')' statement
     */
    private WhileStatement ReadWhile(ParserState state, out int rowNumber)
    {
        Expression condition;
        Statement statement;

        RequireToken(TokenType.WHILE, state, out int row);
        rowNumber = row;
        RequireToken(TokenType.LeftParenth, state);
        condition = ReadExpression(state);
        RequireToken(TokenType.RightParenth, state);
        statement = RequireStatement(state);

        return new WhileStatement(condition, statement, rowNumber);
    }

    /**
     * Grammar: 'do' block 'while' '(' expression ')' ';'
     */
    private DoWhileStatement ReadDoWhile(ParserState state, out int rowNumber)
    {
        Expression condition;
        BlockStatement block;

        RequireToken(TokenType.DO, state, out int row);
        rowNumber = row;
        block = ReadBlock(state, out int _);
        RequireToken(TokenType.WHILE, state);
        RequireToken(TokenType.LeftParenth, state);
        condition = ReadExpression(state);
        RequireToken(TokenType.RightParenth, state);
        RequireToken(TokenType.Semicolon, state);

        return new DoWhileStatement(condition, block, rowNumber);
    }

    /**
     * Grammar: '{' statements '}'
     */
    private BlockStatement ReadBlock(ParserState state, out int rowNumber)
    {
        List<Statement> statements;

        RequireToken(TokenType.LeftBracket, state, out int row);
        rowNumber = row;
        statements = ReadStatements(state);
        RequireToken(TokenType.RightBracket, state);

        return new BlockStatement(statements, rowNumber);
    }

    /**
     * Grammar: 'return' expression ';'
     */
    private ReturnStatement ReadReturn(ParserState state, out int rowNumber)
    {
        Expression expression;

        RequireToken(TokenType.RETURN, state, out int row);
        rowNumber = row;
        expression = ReadExpression(state);
        RequireToken(TokenType.Semicolon, state);

        return new ReturnStatement(expression, rowNumber);
    }

    /**
     * Grammar: identifier '=' expression | identifier partialFunctionIvocation
     */
    private Statement ReadVarAssignOrFunInvoke(ParserState state, out int rowNumber)
    {
        string identifier;
        Expression? expression;
        Statement statement;
        rowNumber = -1;

        VarAssignStatement ReadPartialVarAssign(int rowVarAssign)
        {
            RequireToken(TokenType.Assign, state);
            expression = ReadExpression(state);
            return new VarAssignStatement(identifier, expression, rowVarAssign);
        }

        identifier = RequireIdentifierToken(state, out int row);
        rowNumber = row;
        if (IsTokenType(state, TokenType.Assign))
            statement = ReadPartialVarAssign(rowNumber);
        else if (IsTokenType(state, TokenType.LeftParenth))
            statement = ReadPartialFunInvoke(state, identifier, out FunInvokeExpression _, rowNumber);
        else
            throw new InvalidSyntaxException("Expected = or (", row);

        return statement;
    }

    /**
     * Grammar: '(' [expression {',' expression}] ')'
     */
    private FunInvokeStatement ReadPartialFunInvoke(ParserState state, string identifier, 
        out FunInvokeExpression functionExpression, int rowNumber)
    {
        List<Expression> parameters = new();
        Expression? expression;

        RequireToken(TokenType.LeftParenth, state);
        expression = GetExpression(state);
        if (expression != null)
            parameters.Add(expression);
        while (IsTokenType(state, TokenType.Comma))
        {
            RequireToken(TokenType.Comma, state);
            expression = ReadExpression(state);
            parameters.Add(expression);
        }
        RequireToken(TokenType.RightParenth, state);

        functionExpression = new FunInvokeExpression(identifier, parameters, rowNumber);
        return new FunInvokeStatement(identifier, parameters, rowNumber);
    }

    /**
     * Grammar: secondLevel {('||' | '&&') secondLevel}
     */
    private Expression ReadExpression(ParserState state)
    {
        Expression left;
        Expression right;

        left = ReadLevel2(state, out int row);
        while (IsTokenType(state, TokenType.Or, TokenType.And))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel2(state, out int _);
            left = type == TokenType.Or
                ? new OrCondition(left, right, row)
                : new AndCondition(left, right, row);
        }

        return left;
    }

    /**
     * Grammar: thirdLevel {('==' | '!=' | '<=' | '>=' | '<' | '>') thirdLevel}
     */
    private Expression ReadLevel2(ParserState state, out int rowNumber)
    {
        Expression left;
        Expression right;

        left = ReadLevel3(state, out int row);
        rowNumber = row;
        while (IsTokenType(state, TokenType.Equals, TokenType.NotEquals,
            TokenType.LessEquals, TokenType.GreaterEquals, TokenType.LessThan, TokenType.GreaterThan))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel3(state, out int _);
            left = type switch
            {
                TokenType.Equals => new EqualsCondition(left, right, rowNumber),
                TokenType.NotEquals => new NotEqualsCondition(left, right, rowNumber),
                TokenType.LessEquals => new LessEqualsCondition(left, right, rowNumber),
                TokenType.GreaterEquals => new GreaterEqualsCondition(left, right, rowNumber),
                TokenType.LessThan => new LessThanCondition(left, right, rowNumber),
                TokenType.GreaterThan => new GreaterThanCondition(left, right, rowNumber),
                _ => throw new Exception("Unexpected behaviour"),
            };
        }

        return left;
    }

    /**
     * Grammar: [('+' | '-' | '!')] fourthLevel {('+' | '-') fourthLevel}
     */
    private Expression ReadLevel3(ParserState state, out int rowNumber)
    {
        TokenType? unaryOperator = null;
        Expression left;
        Expression right;

        if (IsTokenType(state, TokenType.Plus, TokenType.Minus, TokenType.Not))
            unaryOperator = ReadToken(state).TokenType;
        left = ReadLevel4(state, out int row);
        rowNumber = row;
        if (unaryOperator != null)
        { 
            if (unaryOperator == TokenType.Plus)
                left = new PlusUnaryExpression(left, rowNumber);
            else if (unaryOperator == TokenType.Minus)
                left = new MinusUnaryExpression(left, rowNumber);
            else
                left = new NotCondition(left, rowNumber);
        }

        while (IsTokenType(state, TokenType.Plus, TokenType.Minus))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel4(state, out int _);
            left = type == TokenType.Plus
                ? new PlusExpression(left, right, rowNumber)
                : new MinusExpression(left, right, rowNumber);
        }

        return left;
    }

    /**
     * Grammar: fifthLevel {('*' | '/') fifthLevel}
     */
    private Expression ReadLevel4(ParserState state, out int rowNumber)
    {
        Expression left;
        Expression right;

        left = ReadLevel5(state, out int row);
        rowNumber = row;
        while (IsTokenType(state, TokenType.Multiply, TokenType.Divide))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel5(state, out int _);
            left = type == TokenType.Multiply
                ? new MultiplyExpression(left, right, rowNumber)
                : new DivideExpression(left, right, rowNumber);
        }

        return left;
    }

    /**
     * Grammar: sixthLevel {'%' sixthLevel}
     */
    private Expression ReadLevel5(ParserState state, out int rowNumber)
    {
        Expression expression;

        expression = ReadLevel6(state, out int row);
        rowNumber = row;
        while (IsTokenType(state, TokenType.Modulo))
        {
            RequireToken(TokenType.Modulo, state);
            expression = new ModuloExpression(expression, ReadLevel6(state, out int _), rowNumber);
        }

        return expression;
    }

    /**
     * Grammar: functionInvocation | value | identifier | '(' expression ')'
     */
    private Expression ReadLevel6(ParserState state, out int rowNumber)
    {
        Expression expression;

        Token? token = PeekToken(state, out int row);
        if (token == null)
            throw new InvalidSyntaxException("Expected <function>, <value>, <identifier> or ( <expression> )", state.LastRow);
        switch (token.TokenType)
        {
            case TokenType.identifier:
                string identifier = RequireIdentifierToken(state, out int rowIdentifier);
                rowNumber = rowIdentifier;
                if (IsTokenType(state, TokenType.LeftParenth))
                {
                    ReadPartialFunInvoke(state, identifier, out FunInvokeExpression funInvoke, rowIdentifier);
                    expression = funInvoke;
                }
                else
                    expression = new VarInvokeExpression(identifier, rowIdentifier);
                break;

            case TokenType.dataBool:
                bool boolean = (bool)RequireValueToken(state, out int rowBool);
                rowNumber = rowBool;
                expression = new BoolExpression(boolean, rowBool);
                break;
            case TokenType.dataDouble:
                double real = (double)RequireValueToken(state, out int rowDouble);
                rowNumber = rowDouble;
                expression = new DoubleExpression(real, rowDouble);
                break;
            case TokenType.dataInt:
                int whole = (int)RequireValueToken(state, out int rowInt);
                rowNumber = rowInt;
                expression = new IntExpression(whole, rowInt);
                break;
            case TokenType.dataString:
                string sentence = (string)RequireValueToken(state, out int rowString);
                rowNumber = rowString;
                expression = new StringExpression(sentence, rowString);
                break;
            case TokenType.LeftParenth:
                RequireToken(TokenType.LeftParenth, state, out int rowParenth);
                rowNumber = rowParenth;
                expression = ReadExpression(state);
                RequireToken(TokenType.RightParenth, state);
                break;
            default:
                throw new InvalidSyntaxException("Expected <function>, <value>, <identifier> or ( <expression> )", row);
        }

        return expression;
    }

    // SUPPORT FUNCTIONS

    private Expression? GetExpression(ParserState state)
    {
        Expression? expression;
        List<Token> originalTokens = state.Tokens;
        List<Token> tokensCopy = new();
        state.Tokens.ForEach(token => tokensCopy.Add(token));
        try
        {
            state.Tokens = tokensCopy;
            expression = ReadExpression(state);
        }
        catch (Exception)
        {
            state.Tokens = originalTokens;
            return null;
        }
        return expression;
    }

    private Statement RequireStatement(ParserState state)
    {
        Statement? statement = ReadStatement(state, out int row);
        if (statement == null)
            throw new InvalidSyntaxException("Expected <statement>", row);
        return statement;
    }

    private Type RequireDatatypeToken(ParserState state)
    {
        object value = RequireValueToken(state, out int row);
        if (value.GetType() != typeof(string))
            throw new InvalidSyntaxException("Expected <datatype>", row);
        return value switch
        {
            "Int" => typeof(int),
            "Double" => typeof(double),
            "String" => typeof(string),
            "Boolean" => typeof(bool),
            _ => throw new InvalidDatatypeException("Unknown datatype", row),
        };
    }

    private object RequireValueToken(ParserState state, out int rowNumber)
    {
        object? value;
        if (PeekToken(state) == null || (value = ReadToken(state, out int rowRead).Value) == null)
            throw new InvalidSyntaxException("Expected <value>", state.LastRow);
        rowNumber = rowRead;
        return value;
    }

    private void RequireToken(TokenType type, ParserState state)
        => RequireToken(type, state, out int _);

    private void RequireToken(TokenType type, ParserState state, out int rowNumber)
    {
        if (!IsTokenType(state, type))
        {
            PeekToken(state, out int rowPeek);
            throw new InvalidSyntaxException($"Expected: {Token.TokenTypeToString[type]}", rowPeek);
        }
        ReadToken(state, out int row);
        rowNumber = row;
    }

    private string RequireIdentifierToken(ParserState state)
        => RequireIdentifierToken(state, out int _);

    private string RequireIdentifierToken(ParserState state, out int rowNumber)
    {
        if (!IsTokenType(state, TokenType.identifier))
        {
            PeekToken(state, out int rowPeek);
            throw new InvalidSyntaxException("Expected: <identifier>", rowPeek);
        }
        string? identifier = (string?)ReadToken(state, out int row).Value;
        rowNumber = row;
        if (identifier == null || !Regex.Match(identifier, "[a-zA-Z_][a-zA-Z0-9_]*").Success)
            throw new InvalidSyntaxException("Invalid identifier", row);
        return identifier;
    }

    private bool IsTokenType(ParserState state, params TokenType[] types)
    {
        Token? token = PeekToken(state);
        if (token == null)
            return false;
        foreach (TokenType type in types)
            if (token.TokenType == type)
                return true;
        return false;
    }

    private Token? PeekToken(ParserState state)
        => PeekToken(state, out int _);

    private Token? PeekToken(ParserState state, out int rowNumber)
    { 
        Token? token = state.Tokens.Count != 0 ? state.Tokens.First() : null;
        rowNumber = token?.RowNumber ?? state.LastRow;
        return token;
    }

    private Token ReadToken(ParserState state)
        => ReadToken(state, out int _);

    private Token ReadToken(ParserState state, out int rowNumber)
    {
        Token token = state.Tokens.First();
        state.Tokens.RemoveAt(0);
        rowNumber = token.RowNumber;
        return token;
    }

    // SUPPORT CLASSES

    private class ParserState
    {
        internal List<Token> Tokens { get; set; }
        internal int LastRow { get; }
        internal ParserState(List<Token> tokens)
        {
            Tokens = tokens;
            LastRow = (tokens.Count > 0) 
                ? tokens.Last().RowNumber : -1;
        }
    }
}
