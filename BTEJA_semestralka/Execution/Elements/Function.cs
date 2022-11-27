
using InterpreterSK.AST.Statements.Block;

namespace InterpreterSK.Execution.Elements;

internal class Function : ExecutionElement
{
    internal BlockStatement Block { get; }

    internal Function(string identifier, Type datatype, BlockStatement block) : base(identifier, datatype) 
    {
        Block = block;
    }


    internal Type Analyze(ExecutionContext context)
    {
        // TODO check if contains return (and valid datatype)
        return typeof(Function);
    }

    internal object Execute(ExecutionContext context)
    {
        // TODO
        return Block.Execute(context);
    }
}
