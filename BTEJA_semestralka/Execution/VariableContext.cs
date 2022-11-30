using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution;

internal class VariableContext
{
    internal List<Variable> Variables { get; }

    internal VariableContext(List<Variable> variables)
    {
        Variables = variables;
    }

    internal Variable GetVariable(string identifier, int rowNumber)
    {
        Variable? variable = Variables.Find(variable => variable.Identifier == identifier);
        if (variable == null)
            throw new Exceptions.InvalidInvocationException($"Reading unknown variable: {identifier}", rowNumber);
        return variable;
    }

    internal void AddVariable(Variable newVariable)
    {
        foreach (var oldVariable in Variables)
            if (oldVariable.Identifier == newVariable.Identifier)
            {
                Variables.Remove(oldVariable);
                break;
            }
        Variables.Add(newVariable);
    }

    internal VariableContext CreateCopy(ExecutionContext context)
    {
        List<Variable> variables = new();
        Variables.ForEach(variable => variables.Add(
            new Variable(variable.Identifier, variable.Datatype, variable.Expression, variable.Context ?? context)
            ));
        return new VariableContext(variables);
    }
}
