
namespace InterpreterSK.AST.Statements;

internal abstract class JumpStatement : Statement 
{
    internal JumpStatement(int rowNumber) : base(rowNumber) {}

    internal override bool EndsInReturn(Execution.ExecutionContext _, Type __)
        => false;

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = true;
        return string.Concat(Enumerable.Repeat("-", level)) + GetType().Name + "\n";
    }
}
