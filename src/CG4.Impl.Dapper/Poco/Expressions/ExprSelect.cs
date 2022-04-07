using System.Collections;

namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprSelect : Expr, IEnumerable<ExprSelectedColumn>
    {
        private readonly List<ExprSelectedColumn> _selectedColumns = new();

        public void Add(ExprSelectedColumn selectedColumn)
        {
            _selectedColumns.Add(selectedColumn);
        }

        public override void Accept(IExprVisitor visitor) => visitor.VisitSelect(this);

        public IEnumerator<ExprSelectedColumn> GetEnumerator()
        {
            return _selectedColumns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _selectedColumns.GetEnumerator();
        }
    }
}
