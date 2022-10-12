namespace CG4.Impl.Dapper.Poco.Expressions;

public class ExprNot : ExprBoolFunc
{
    public ExprBoolean Body { get; set; }

    public override void Accept(IExprVisitor visitor) => visitor.VisitNot(this);
}