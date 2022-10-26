
using BTEJA_BCSH2_semestralka.LexicalAnalysis.Tokens;
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Expressions;
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Functions;
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Statements;
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace BTEJA_BCSH2_semestralka.SemanticAnalysis;

internal class Parser
{

    /**
     * Grammar: statement
     */
    internal void Parse(List<Token> tokens, out BlockStatement statement)
    {
        ParserState state = new(tokens);
        statement = (BlockStatement)ReadStatement(state);
    }

    /**
     * Grammar: { variableDeclaration ';' | functionDeclaration	| if | for | do | while	| block | return | 'break' ';' | 'continue' ';' | variableAssignment ';' | functionInvocation ';'}
    */
    private Statement ReadStatement(ParserState state)
    {
        List<Statement> statements = new();
        Statement? statement = null;
        do
        {
            Token? token = PeekToken(state);
            if (token == null)
                break;
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

                case TokenType.BREAK:
                    RequireToken(TokenType.BREAK, state);
                    RequireToken(TokenType.Semicolon, state);
                    // TODO: statement = new BreakStatement();
                    break;

                case TokenType.CONTINUE:
                    RequireToken(TokenType.CONTINUE, state);
                    RequireToken(TokenType.Semicolon, state);
                    // TODO: statement = new ContinueStatement();
                    break;

                case TokenType.identifier:
                    statement = ReadVarAssignOrFunInvoke(state);
                    RequireToken(TokenType.Semicolon, state);
                    break;
            }
            if (statement != null)
                statements.Add(statement);
        } while (statement != null);
        return new BlockStatement(statements);
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
        if (IsTokenType(TokenType.Assign, state))
        {
            RequireToken(TokenType.Assign, state);
            expression = ReadExpression(state);
            // TODO: check expression.datatype == datatype
        }
        Variable variable = new Variable(identifier, datatype, expression);
        return new VarDeclareStatement(variable);
    }

    /**
     * Grammar: 'fun' identifier '(' paramsDeclaration ')' ':' identifier block
     */
    private Statement ReadFunDeclare(ParserState state)
    {
        string identifier;
        List<Variable> prms;
        Type datatype;
        BlockStatement block;

        RequireToken(TokenType.FUN, state);
        identifier = RequireIdentifierToken(state);
        RequireToken(TokenType.LeftParenth, state);
        prms = ReadParamsDeclare(state);
        RequireToken(TokenType.Colon, state);
        datatype = RequireDatatypeToken(state);
        block = (BlockStatement)ReadBlock(state);

        Function function = new(identifier, prms, datatype, block);
        return new FunDeclareStatement(function);
    }

    /**
     * Grammar: [identifier ':' identifier {',' identifier ':' identifier}]
     */
    private List<Variable> ReadParamsDeclare(ParserState state)
    {
        List<Variable> variables = new();
        Variable variable;
        string identifier;
        Type datatype;

        void RequireParam() 
        {
            identifier = RequireIdentifierToken(state);
            RequireToken(TokenType.Colon, state);
            datatype = RequireDatatypeToken(state);
            variable = new(identifier, datatype, null);
            variables.Add(variable);
        }

        if (IsTokenType(TokenType.identifier, state))
            RequireParam();
        while (IsTokenType(TokenType.Comma, state))
        {
            RequireToken(TokenType.Comma, state);
            RequireParam();
        } 

        return variables;
    }

    /**
     * Grammar: 'if' '(' expression ')' statement {'else' 'if' '(' expression ')' statement} ['else' statement]
     */
    private Statement ReadIf(ParserState state)
    {
        List<(Expression?, Statement)> conditionments = new();
        Expression condition;
        Statement statement;

        void RequireIfBlock()
        {
            RequireToken(TokenType.IF, state);
            RequireToken(TokenType.LeftParenth, state);
            condition = ReadExpression(state); // check if result is bool
            RequireToken(TokenType.RightParenth, state);
            statement = ReadStatement(state);
            conditionments.Add((condition, statement));
        }

        RequireIfBlock();
        while (IsTokenType(TokenType.ELSE, state))
        {
            RequireToken(TokenType.ELSE, state);
            if (IsTokenType(TokenType.IF, state))
                RequireIfBlock();
            else
            {
                statement = ReadStatement(state);
                conditionments.Add((null, statement));
                break;
            }
        }

        return new IfStatement(conditionments);
    }

    private Statement ReadFor(ParserState state) => throw new NotImplementedException();
    private Statement ReadWhile(ParserState state) => throw new NotImplementedException();
    private Statement ReadDoWhile(ParserState state) => throw new NotImplementedException();
    private Statement ReadBlock(ParserState state) => throw new NotImplementedException();
    private Statement ReadReturn(ParserState state) => throw new NotImplementedException();
    private Statement ReadVarAssignOrFunInvoke(ParserState state) => throw new NotImplementedException();

    private Expression ReadExpression(ParserState state) => throw new NotImplementedException();

    // SUPPORT FUNCTIONS

    private Type RequireDatatypeToken(ParserState state)
    {
        return (string)RequireValueToken(state) switch 
        {
            "Int" => typeof(int),
            "Double" => typeof(double),
            "String" => typeof(string),
            "Boolean" => typeof(bool),
            _ => throw new ParserException("Ivalid datatype"),
        };
    }

    private object RequireValueToken(ParserState state)
    {
        object value;
        if (PeekToken(state) == null || (value = ReadToken(state).Value) == null )
            throw new ParserException("Expected <value>");
        return value;
    }

    private void RequireToken(TokenType type, ParserState state)
    {
        if (!IsTokenType(TokenType.identifier, state))
            throw new ParserException($"Expected: {Token.TokenTypeToString[type]}");
    }

    private string RequireIdentifierToken(ParserState state)
    {
        if (!IsTokenType(TokenType.identifier, state))
            throw new ParserException("Expected: <identifier>");
        // TODO: check identifier regex
        return (string)ReadToken(state).Value;
    }

    private bool IsTokenType(TokenType type, ParserState state) 
        => PeekToken(state) != null && PeekToken(state).TokenType == type;

    private Token? PeekToken(ParserState state) => (state.Tokens.Count != 0) ? state.Tokens.First() : null;

    private Token ReadToken(ParserState state)
    {
        Token token = state.Tokens.First();
        state.Tokens.RemoveAt(0);
        return token;
    }

    // SUPPORT CLASSES

    private class ParserState
    { 
        internal List<Token> Tokens { get; }
        internal ParserState(List<Token> tokens) => Tokens = tokens;
    }

    internal class ParserException : Exception
    {
        internal ParserException(string message) : base(message) { }
    }
}
