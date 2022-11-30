
using InterpreterSK.AST.Expressions;
using InterpreterSK.AST.Statements.Block;
using InterpreterSK.AST.Statements.Jumps;

namespace InterpreterSK.Execution.Elements;

internal class Function : ExecutionElement
{
    internal BlockStatement Block { get; }

    internal List<Variable> Parameters { get; }

    private static int CurrentRepetition { get; set; } = 0;

    internal Function(string identifier, Type datatype, List<Variable> parameters, BlockStatement block, ExecutionContext context) 
        : base(identifier, datatype, context) 
    {
        Parameters = parameters;
        Block = block;
    }

    internal Function(string identifier, Type datatype, List<Variable> parameters, BlockStatement block)
        : base(identifier, datatype, null)
    {
        Parameters = parameters;
        Block = block;
    }

    internal object Call(ExecutionContext outerContext, List<Expression> parameters, int rowNumber)
    {
        if (CurrentRepetition++ >= outerContext.RepetitionLimit)
            throw new Exceptions.StackOverflowException("Function invocation caused stack to overflow", rowNumber);
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);
        InsertParameters(innerContext, parameters, true, rowNumber);
        object result = Block.Execute(innerContext);
        if (result is not ReturnStatement)
            throw new Exception("Unexpected behaviour");
        CurrentRepetition = 0;
        return ((ReturnStatement)result).Value;
    }

    internal Type Analyze(ExecutionContext outerContext, List<Expression> parameters, int rowNumber)
    {
        ExecutionContext innerContext = outerContext.CreateInnerContext(this);
        InsertParameters(innerContext, parameters, false, rowNumber);
        Block.Analyze(innerContext);
        return Datatype;
    }

    private void CheckParameters(List<Expression> expressions, int rowNumber)
    {
        if (expressions.Count != Parameters.Count)
            throw new Exceptions.InvalidInvocationException(
                $"Wrong number of parameters, expected: {Parameters.Count}, given: {expressions.Count}", rowNumber);
    }

    private void InsertParameters(ExecutionContext context, List<Expression> expressions, bool execute, int rowNumber)
    {
        CheckParameters(expressions, rowNumber);
        Variable parameter;
        Expression expression;
        object value;
        Type type;
        for (int i = 0; i < Parameters.Count; i++)
        {
            parameter = Parameters[i];
            expression = expressions[i];
            if (execute)
            {
                value = expression.Execute(context);
                type = value.GetType();
                parameter.Expression = new LiteralExpression(value);
            }
            else
            { 
                type = expression.Analyze(context);
                parameter.Expression = expression;
            }
            if (parameter.Datatype != type)
                throw new Exceptions.InvalidDatatypeException("Cannot assign expression to parameter of different datatype", expression.RowNumber);
            context.VariableContext.AddVariable(parameter);
        }
    }
}
