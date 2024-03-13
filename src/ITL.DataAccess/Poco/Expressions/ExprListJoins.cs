using System.Collections;
using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprListJoins : Expr, IEnumerable<ExprJoin>
    {
        private readonly List<ExprJoin> _joins = new();

        public override void Accept(IExprVisitor visitor) => visitor.VisitListJoins(this);

        public void Add(ExprJoin exprJoin)
        {
            _joins.Add(exprJoin);
        }

        public IEnumerator<ExprJoin> GetEnumerator()
        {
            return _joins.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _joins.GetEnumerator();
        }
    }
}
