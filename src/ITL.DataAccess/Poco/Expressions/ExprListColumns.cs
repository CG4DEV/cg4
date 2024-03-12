using System.Collections;
using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprListColumns : Expr, IEnumerable<ExprColumn>
    {
        private readonly List<ExprColumn> _columns = new();

        public override void Accept(IExprVisitor visitor) => visitor.VisitListColumns(this);

        public void Add(ExprColumn column)
        {
            _columns.Add(column);
        }

        public IEnumerator<ExprColumn> GetEnumerator()
        {
            return _columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _columns.GetEnumerator();
        }
    }
}
