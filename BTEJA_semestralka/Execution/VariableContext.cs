using InterpreterSK.Execution.Elements;

namespace InterpreterSK.Execution;

internal class VariableContext
{
    internal List<Variable> Variables { get; }

    internal VariableContext(List<Variable> variables)
    {
        Variables = variables;
    }

    internal Variable GetVariable(string identifier)
    {
        Variable? variable = Variables.Find(variable => variable.Identifier == identifier);
        if (variable == null)
            throw new Exceptions.InvalidInvocationException($"Reading unknown variable: {identifier}");
        return variable;
    }
}
