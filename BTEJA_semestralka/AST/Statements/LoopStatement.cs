namespace InterpreterSK.AST.Statements;

internal abstract class LoopStatement : Statement
{
    internal Statement Statement { get; }

    protected int CurrentRepetition { get; set; } = 0;

    internal LoopStatement(Statement statement, int rowNumber) : base(rowNumber)
    {
        Statement = statement;
    }

    internal override bool EndsInReturn(Execution.ExecutionContext context, Type datatype)
        => Statement.EndsInReturn(context, datatype);

    internal override string GetToString(int level, out bool isLeaf)
    {
        isLeaf = false;
        return string.Concat(Enumerable.Repeat("-", level++)) + GetType().Name + "\n"
            + Statement.GetToString(level, out bool leaf) + (!leaf ? "\n" : "");
    }
}
