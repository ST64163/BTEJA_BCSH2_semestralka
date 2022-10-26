
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Statements;
using BTEJA_BCSH2_semestralka.SemanticAnalysis.Variables;
using System;
using System.Collections.Generic;

namespace BTEJA_BCSH2_semestralka.SemanticAnalysis.Functions;

internal class Function
{
    internal string Name { get; }
    internal List<Variable> Parameters { get; }
    internal Type ReturnType { get; }
    internal BlockStatement Block { get; }

    internal Function(string name, List<Variable> parameters, Type returnType, BlockStatement block)
    {
        Name = name;
        ReturnType = returnType;
        Parameters = parameters;
        Block = block;
    }
}
