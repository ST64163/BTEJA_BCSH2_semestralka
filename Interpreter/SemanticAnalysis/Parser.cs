using IDE.Interpreter.AST.Expressions;
using IDE.Interpreter.AST.Statements;
using IDE.Interpreter.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IDE.Interpreter.SemanticAnalysis;

internal class Parser
{

    /**
     * Grammar: statements
     */
    internal void Parse(List<Token> tokens, out List<Statement> statements)
    {
        ParserState state = new(tokens);
        statements = ReadStatements(state);
    }

    /**
     * Grammar: {statement}
    */
    private List<Statement> ReadStatements(ParserState state)
    {
        List<Statement> statements = new();
        Statement? statement;
        do
        {
            statement = ReadStatement(state);
            if (statement != null)
                statements.Add(statement);
        } while (statement != null);

        return statements;
    }

    /**
     * variableDeclaration ';' | functionDeclaration | if | for | do | while | block | return | 'break' ';' | 'continue' ';' | variableAssignment ';' | functionInvocation ';'
     */
    private Statement? ReadStatement(ParserState state)
    {
        Statement? statement = null;

        Token? token = PeekToken(state);
        if (token != null)
        {
            switch (token.TokenType)
            {
                case TokenType.VAR:
                    statement = ReadVarDeclare(state);
                    RequireToken(TokenType.Semicolon, state);
                    break;

                case TokenType.FUN:
                    statement = ReadFunDeclare(state);
                    break;
                case TokenType.IF:
                    statement = ReadIf(state);
                    break;
                case TokenType.FOR:
                    statement = ReadFor(state);
                    break;
                case TokenType.DO:
                    statement = ReadDoWhile(state);
                    break;
                case TokenType.WHILE:
                    statement = ReadWhile(state);
                    break;
                case TokenType.LeftBracket:
                    statement = ReadBlock(state);
                    break;
                case TokenType.RETURN:
                    statement = ReadReturn(state);
                    break;

                case TokenType.BREAK: // TODO: check if not in function
                    RequireToken(TokenType.BREAK, state);
                    RequireToken(TokenType.Semicolon, state);
                    statement = new BreakStatement();
                    break;

                case TokenType.CONTINUE: // TODO: check if not in function
                    RequireToken(TokenType.CONTINUE, state);
                    RequireToken(TokenType.Semicolon, state);
                    statement = new ContinueStatement();
                    break;

                case TokenType.identifier:
                    statement = ReadVarAssignOrFunInvoke(state);
                    RequireToken(TokenType.Semicolon, state);
                    break;
            }
        }

        return statement;
    }

    /**
     * Grammar: 'var' identifier ':' identifier ['=' expression]
     */
    private VarDeclareStatement ReadVarDeclare(ParserState state)
    {
        string identifier;
        Type datatype;
        Expression? expression = null;

        RequireToken(TokenType.VAR, state);
        identifier = RequireIdentifierToken(state);
        RequireToken(TokenType.Colon, state);
        datatype = RequireDatatypeToken(state);
        if (IsTokenType(state, TokenType.Assign))
        {
            RequireToken(TokenType.Assign, state);
            expression = ReadExpression(state);
            // TODO: check expression.datatype == datatype
        }
        return new VarDeclareStatement(identifier, datatype, expression);
    }

    /**
     * Grammar: 'fun' identifier '(' paramsDeclaration ')' ':' identifier block
     */
    private FunDeclareStatement ReadFunDeclare(ParserState state)
    {
        string identifier;
        List<ParamExpression> parameters;
        Type datatype;
        BlockStatement block;

        RequireToken(TokenType.FUN, state);
        identifier = RequireIdentifierToken(state);
        RequireToken(TokenType.LeftParenth, state);
        parameters = ReadParamsDeclare(state);
        RequireToken(TokenType.RightParenth, state);
        RequireToken(TokenType.Colon, state);
        datatype = RequireDatatypeToken(state);
        block = ReadBlock(state);

        return new FunDeclareStatement(identifier, parameters, datatype, block);
    }

    /**
     * Grammar: [identifier ':' identifier {',' identifier ':' identifier}]
     */
    private List<ParamExpression> ReadParamsDeclare(ParserState state)
    {
        List<ParamExpression> parameters = new();
        string identifier;
        Type datatype;

        void RequireParam()
        {
            identifier = RequireIdentifierToken(state);
            RequireToken(TokenType.Colon, state);
            datatype = RequireDatatypeToken(state);
            parameters.Add(new ParamExpression(identifier, datatype));
        }

        if (IsTokenType(state, TokenType.identifier))
            RequireParam();
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
    private IfStatement ReadIf(ParserState state)
    {
        List<(Expression?, Statement)> conditionments = new();
        Expression condition;
        Statement statement;

        (Expression, Statement) ReadIfBlock()
        {
            RequireToken(TokenType.IF, state);
            RequireToken(TokenType.LeftParenth, state);
            condition = ReadExpression(state); // TODO: check if result (is/will be) bool
            RequireToken(TokenType.RightParenth, state);
            statement = RequireStatement(state);
            return (condition, statement);
        }

        conditionments.Add(ReadIfBlock());
        while (IsTokenType(state, TokenType.ELSE))
        {
            RequireToken(TokenType.ELSE, state);
            if (IsTokenType(state, TokenType.IF))
                conditionments.Add(ReadIfBlock());
            else
            {
                statement = RequireStatement(state);
                conditionments.Add((null, statement));
                break;
            }
        }

        return new IfStatement(conditionments);
    }

    /**
     * Grammar: 'for' '(' identifier 'in' expression 'until' expression  ')' statement
     */
    private ForStatement ReadFor(ParserState state)
    {
        string identifier;
        Expression start, end;
        Statement statement;

        RequireToken(TokenType.FOR, state);
        RequireToken(TokenType.LeftParenth, state);
        identifier = RequireIdentifierToken(state);
        RequireToken(TokenType.IN, state);
        start = ReadExpression(state); // TODO: check if (whole?) number
        RequireToken(TokenType.UNTIL, state);
        end = ReadExpression(state); // TODO: check if (whole?) number
        RequireToken(TokenType.RightParenth, state);
        statement = RequireStatement(state);

        return new ForStatement(identifier, start, end, statement);
    }

    /**
     * Grammar: 'while' '(' expression ')' statement
     */
    private WhileStatement ReadWhile(ParserState state)
    {
        Expression condition;
        Statement statement;

        RequireToken(TokenType.WHILE, state);
        RequireToken(TokenType.LeftParenth, state);
        condition = ReadExpression(state); // TODO: check if (is/will be) bool
        RequireToken(TokenType.RightParenth, state);
        statement = RequireStatement(state);

        return new WhileStatement(condition, statement);
    }

    /**
     * Grammar: 'do' block 'while' '(' expression ')' ';'
     */
    private DoWhileStatement ReadDoWhile(ParserState state)
    {
        Expression condition;
        BlockStatement block;

        RequireToken(TokenType.DO, state);
        block = ReadBlock(state);
        RequireToken(TokenType.WHILE, state);
        RequireToken(TokenType.LeftParenth, state);
        condition = ReadExpression(state); // TODO: check if (is/will be) bool
        RequireToken(TokenType.RightParenth, state);
        RequireToken(TokenType.Semicolon, state);

        return new DoWhileStatement(condition, block);
    }

    /**
     * Grammar: '{' statements '}'
     */
    private BlockStatement ReadBlock(ParserState state)
    {
        List<Statement> statements;

        RequireToken(TokenType.LeftBracket, state);
        statements = ReadStatements(state);
        RequireToken(TokenType.RightBracket, state);

        return new BlockStatement(statements);
    }

    /**
     * Grammar: 'return' expression ';'
     */
    private ReturnStatement ReadReturn(ParserState state)
    {
        Expression expression;

        RequireToken(TokenType.RETURN, state);
        expression = ReadExpression(state);
        RequireToken(TokenType.Semicolon, state);

        return new ReturnStatement(expression);
    }

    /**
     * Grammar: identifier '=' expression | identifier partialFunctionIvocation
     */
    private Statement ReadVarAssignOrFunInvoke(ParserState state)
    {
        string identifier;
        Expression? expression;
        Statement statement;

        VarAssignStatement ReadPartialVarAssign()
        {
            RequireToken(TokenType.Assign, state);
            expression = ReadExpression(state); // TODO: check if datatype is valid
            return new VarAssignStatement(identifier, expression);
        }

        identifier = RequireIdentifierToken(state);
        if (IsTokenType(state, TokenType.Assign))
            statement = ReadPartialVarAssign();
        else if (IsTokenType(state, TokenType.LeftParenth))
            statement = ReadPartialFunInvoke(state, identifier, out _);
        else
            throw new ParsingException("Expected = or (");

        return statement;
    }

    /**
     * Grammar: '(' [expression {',' expression}] ')'
     */
    private FunInvokeStatement ReadPartialFunInvoke(ParserState state, string identifier, out FunInvokeExpression functionExpression)
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

        functionExpression = new FunInvokeExpression(identifier, parameters);
        return new FunInvokeStatement(identifier, parameters);
    }

    /**
     * Grammar: secondLevel {('||' | '&&') secondLevel}
     */
    private Expression ReadExpression(ParserState state)
    {
        Expression left;
        Expression right;

        left = ReadLevel2(state);
        while (IsTokenType(state, TokenType.Or, TokenType.And))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel2(state);
            left = type == TokenType.Or
                ? new OrExpression(left, right)
                : new AndExpression(left, right);
        }

        return left;
    }

    /**
     * Grammar: thirdLevel {('==' | '!=' | '<=' | '>=' | '<' | '>') thirdLevel}
     */
    private Expression ReadLevel2(ParserState state)
    {
        Expression left;
        Expression right;

        left = ReadLevel3(state);
        while (IsTokenType(state, TokenType.Equals, TokenType.NotEquals,
            TokenType.LessEquals, TokenType.GreaterEquals, TokenType.LessThan, TokenType.GreaterThan))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel3(state);
            left = type switch
            {
                TokenType.Equals => new EqualsExpression(left, right),
                TokenType.NotEquals => new NotEqualsExpression(left, right),
                TokenType.LessEquals => new LessEqualsExpression(left, right),
                TokenType.GreaterEquals => new GreaterEqualsExpression(left, right),
                TokenType.LessThan => new LessThanExpression(left, right),
                TokenType.GreaterThan => new GreaterThanExpression(left, right),
                _ => throw new ParsingException("Unexpected error"),
            };
        }

        return left;
    }

    /**
     * Grammar: [('+' | '-' | '!')] fourthLevel {('+' | '-') fourthLevel}
     */
    private Expression ReadLevel3(ParserState state)
    {
        TokenType? unaryOperator = null;
        Expression left;
        Expression right;

        if (IsTokenType(state, TokenType.Plus, TokenType.Minus, TokenType.Not))
            unaryOperator = ReadToken(state).TokenType;
        left = ReadLevel4(state);
        if (unaryOperator != null)
            if (unaryOperator == TokenType.Plus)
                left = new PlusUnaryExpression(left);
            else if (unaryOperator == TokenType.Minus)
                left = new MinusUnaryExpression(left);
            else
                left = new NotExpression(left);

        while (IsTokenType(state, TokenType.Plus, TokenType.Minus))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel3(state);
            left = type == TokenType.Plus
                ? new PlusExpression(left, right)
                : new MinusExpression(left, right);
        }

        return left;
    }

    /**
     * Grammar: fifthLevel {('*' | '/') fifthLevel}
     */
    private Expression ReadLevel4(ParserState state)
    {
        Expression left;
        Expression right;

        left = ReadLevel5(state);
        while (IsTokenType(state, TokenType.Multiply, TokenType.Divide))
        {
            TokenType type = ReadToken(state).TokenType;
            right = ReadLevel5(state);
            left = type == TokenType.Multiply
                ? new MultiplyExpression(left, right)
                : new DivideExpression(left, right);
        }

        return left;
    }
     
    /**
     * Grammar: sixthLevel {'%' sixthLevel}
     */
    private Expression ReadLevel5(ParserState state)
    {
        Expression expression;

        expression = ReadLevel6(state);
        while (IsTokenType(state, TokenType.Modulo))
        {
            RequireToken(TokenType.Modulo, state);
            expression = new ModuloExpression(expression, ReadLevel6(state));
        }

        return expression;
    }

    /**
     * Grammar: functionInvocation | value | identifier | '(' expression ')'
     */
    private Expression ReadLevel6(ParserState state)
    {
        Expression expression;

        Token? token = PeekToken(state);
        if (token == null)
            throw new ParsingException("Expected <function>, <value>, <identifier> or ( <expression> )");
        switch (token.TokenType)
        {
            case TokenType.identifier:
                string identifier = RequireIdentifierToken(state);
                if (IsTokenType(state, TokenType.LeftParenth))
                {
                    ReadPartialFunInvoke(state, identifier, out FunInvokeExpression funInvoke);
                    expression = funInvoke;
                }
                else
                    expression = new VarInvokeExpression(identifier);
                break;

            case TokenType.dataBool:
                bool boolean = (bool)RequireValueToken(state);
                expression = new BoolExpression(boolean);
                break;
            case TokenType.dataDouble:
                double real = (double)RequireValueToken(state);
                expression = new DoubleExpression(real);
                break;
            case TokenType.dataInt:
                int whole = (int)RequireValueToken(state);
                expression = new IntExpression(whole);
                break;
            case TokenType.dataString:
                string sentence = (string)RequireValueToken(state);
                expression = new StringExpression(sentence);
                break;
            case TokenType.LeftParenth:
                RequireToken(TokenType.LeftParenth, state);
                expression = ReadExpression(state);
                RequireToken(TokenType.RightParenth, state);
                break;
            default:
                throw new ParsingException("Expected <function>, <value>, <identifier> or ( <expression> )");
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
        catch(Exception _) 
        {
            state.Tokens = originalTokens; 
            return null;
        }
        return expression;
    }

    private Statement RequireStatement(ParserState state)
    {
        Statement? statement = ReadStatement(state);
        if (statement == null)
            throw new ParsingException("Expected <statement>");
        return statement;
    }

    private Type RequireDatatypeToken(ParserState state)
    {
        return (string)RequireValueToken(state) switch
        {
            "Int" => typeof(int),
            "Double" => typeof(double),
            "String" => typeof(string),
            "Boolean" => typeof(bool),
            _ => throw new ParsingException("Ivalid datatype"),
        };
    }

    private object RequireValueToken(ParserState state)
    {
        object value;
        if (PeekToken(state) == null || (value = ReadToken(state).Value) == null)
            throw new ParsingException("Expected <value>");
        return value;
    }

    private void RequireToken(TokenType type, ParserState state)
    {
        if (!IsTokenType(state, type))
            throw new ParsingException($"Expected: {Token.TokenTypeToString[type]}");
        ReadToken(state);
    }

    private string RequireIdentifierToken(ParserState state)
    {
        if (!IsTokenType(state, TokenType.identifier))
            throw new ParsingException("Expected: <identifier>");
        // TODO: check identifier regex
        return (string)ReadToken(state).Value;
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

    private Token? PeekToken(ParserState state) => state.Tokens.Count != 0 ? state.Tokens.First() : null;

    private Token ReadToken(ParserState state)
    {
        Token token = state.Tokens.First();
        state.Tokens.RemoveAt(0);
        return token;
    }

    // SUPPORT CLASSES

    private class ParserState
    {
        internal List<Token> Tokens { get; set; }
        internal ParserState(List<Token> tokens) => Tokens = tokens;
    }
}
