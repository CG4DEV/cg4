using System;
using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    /// <summary>
    /// Expression constant for decimal values.
    /// </summary>
    public class ExprDecimal : ExprConst
    {
        /// <summary>
        /// Gets or sets the decimal value.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Accepts an expression visitor.
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        public override void Accept(IExprVisitor visitor)
        {
            ((dynamic)visitor).VisitDecimal(this);
        }
    }
}
