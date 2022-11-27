﻿namespace InterpreterSK.AST.Statements;

internal abstract class Statement : Node 
{
    internal override Type Analyze(Execution.ExecutionContext context)
    {
        if (AnalyzedType == null)
        { 
            AnalyzedType = typeof(Statement);
            Analyzation(context);
        }
        return AnalyzedType;
    }

    protected abstract void Analyzation(Execution.ExecutionContext context);
}
