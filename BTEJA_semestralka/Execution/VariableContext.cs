using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution;

internal class VariableContext
{
    internal List<Variable> GlobalVariables { get; }
    internal List<Variable> LocalVariables { get; }
    internal List<Variable> Variables { get => LocalVariables.Concat(GlobalVariables).ToList(); }

    internal VariableContext(List<Variable> variables)
    {
        GlobalVariables = variables;
        LocalVariables = new();
    }

    internal Variable GetVariable(string identifier, int rowNumber)
    {
        Variable? variable = Variables.Find(variable => variable.Identifier == identifier);
        if (variable == null)
            throw new Exceptions.InvalidInvocationException($"Reading unknown variable: {identifier}", rowNumber);
        return variable;
    }

    internal void AddVariable(Variable newVariable, int rowNumber)
    {
        foreach (var variable in LocalVariables)
            if (variable.Identifier == newVariable.Identifier)
                throw new Exceptions.InvalidSyntaxException($"Cannot declare two functions in the same context with the same name: {newVariable.Identifier}", rowNumber);
        LocalVariables.Add(newVariable);
    }

    internal VariableContext CreateCopy(ExecutionContext context)
    {
        List<Variable> variables = LocalVariables;
        GlobalVariables.ForEach(globalVariable =>
        {
            bool add = true;
            foreach (var localVariable in LocalVariables)
                if (localVariable.Identifier == globalVariable.Identifier)
                {
                    add = false;
                    break;
                }
            if (add)
                variables.Add(globalVariable);
        });
        return new VariableContext(variables);
    }
}
