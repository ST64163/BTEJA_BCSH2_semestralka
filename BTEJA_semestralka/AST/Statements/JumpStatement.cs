
namespace InterpreterSK.AST.Statements;

internal abstract class JumpStatement : Statement 
{
    internal override bool EndsInReturn(Execution.ExecutionContext _, Type __)
        => false;
}
