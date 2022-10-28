
namespace IDE.Interpreter.AST.Expressions;

internal class ModuloExpression : BinaryExpression
{
    public ModuloExpression(Expression left, Expression right) : base(left, right) { }

    internal override object Evaluate() => (double)Left.Evaluate() % (double)Right.Evaluate();
}
