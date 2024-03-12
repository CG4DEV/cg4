using System.Collections;
using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprOrderBy : Expr, IEnumerable<ExprOrderColumn>
    {
        private readonly List<ExprOrderColumn> _columns = new();

        public void Add(ExprOrderColumn orderColumn)
        {
            _columns.Add(orderColumn);
        }

        public override void Accept(IExprVisitor visitor) => visitor.VisitOrderBy(this);

        public IEnumerator<ExprOrderColumn> GetEnumerator()
        {
            return _columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _columns.GetEnumerator();
        }
    }
}
