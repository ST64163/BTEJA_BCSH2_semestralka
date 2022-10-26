
using System;

namespace BTEJA_BCSH2_semestralka.SemanticAnalysis.Variables;

internal class Variable
{
    internal string Identifier { get; }
    internal Type Type { get; }
    internal object? Value { get; }

    internal Variable(string identifier, Type type, object? value)
    {
        Identifier = identifier;
        Type = type;
        Value = value;
    }
}
