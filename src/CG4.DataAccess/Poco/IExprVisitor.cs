using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco.Visitors
{
    /// <summary>
    /// Visitor interface for SQL expression tree traversal.
    /// Implements the Visitor pattern to process different types of SQL expressions
    /// and generate appropriate SQL syntax for each expression type.
    /// </summary>
    public interface IExprVisitor
    {
        /// <summary>
        /// Visits a column reference expression.
        /// Used to generate qualified column names (e.g., "table.column").
        /// </summary>
        /// <param name="column">The column expression to visit.</param>
        void VisitColumn(ExprColumn column);

        /// <summary>
        /// Visits an equality predicate expression.
        /// Used to generate SQL equality comparisons (e.g., "column = value").
        /// </summary>
        /// <param name="eqPredicate">The equality predicate to visit.</param>
        void VisitEqPredicate(ExprBoolEqPredicate eqPredicate);

        /// <summary>
        /// Visits an inequality predicate expression.
        /// Used to generate SQL inequality comparisons (e.g., "column != value").
        /// </summary>
        /// <param name="eqPredicate">The inequality predicate to visit.</param>
        void VisitNotEqPredicate(ExprBoolNotEqPredicate eqPredicate);

        /// <summary>
        /// Visits a logical OR expression.
        /// Used to generate SQL OR conditions (e.g., "condition1 OR condition2").
        /// </summary>
        /// <param name="or">The OR expression to visit.</param>
        void VisitOr(ExprBoolOr or);

        /// <summary>
        /// Visits a logical AND expression.
        /// Used to generate SQL AND conditions (e.g., "condition1 AND condition2").
        /// </summary>
        /// <param name="and">The AND expression to visit.</param>
        void VisitAnd(ExprBoolAnd and);

        /// <summary>
        /// Visits a string constant expression.
        /// Used to generate properly escaped and quoted string literals in SQL.
        /// </summary>
        /// <param name="str">The string expression to visit.</param>
        void VisitStr(ExprStr str);

        /// <summary>
        /// Visits a long integer constant expression.
        /// Used to generate numeric literals in SQL.
        /// </summary>
        /// <param name="long">The long integer expression to visit.</param>
        void VisitLong(ExprLong @long);

        /// <summary>
        /// Visits a logical NOT expression.
        /// Used to generate SQL negation (e.g., "NOT condition").
        /// </summary>
        /// <param name="not">The NOT expression to visit.</param>
        void VisitNot(ExprNot not);

        /// <summary>
        /// Visits an integer constant expression.
        /// Used to generate integer literals in SQL.
        /// </summary>
        /// <param name="int">The integer expression to visit.</param>
        void VisitInt(ExprInt @int);

        /// <summary>
        /// Visits a decimal constant expression.
        /// Used to generate decimal literals in SQL with appropriate precision.
        /// </summary>
        /// <param name="decimal">The decimal expression to visit.</param>
        void VisitDecimal(ExprDecimal @decimal);

        /// <summary>
        /// Visits a boolean constant expression.
        /// Used to generate boolean literals in SQL (e.g., TRUE/FALSE).
        /// </summary>
        /// <param name="bool">The boolean expression to visit.</param>
        void VisitBool(ExprBool @bool);

        /// <summary>
        /// Visits a DateTime constant expression.
        /// Used to generate properly formatted date/time literals in SQL.
        /// </summary>
        /// <param name="dateTime">The DateTime expression to visit.</param>
        void VisitDateTime(ExprDateTime dateTime);

        /// <summary>
        /// Visits a DateTimeOffset constant expression.
        /// Used to generate date/time literals with timezone information in SQL.
        /// </summary>
        /// <param name="dateTimeOffset">The DateTimeOffset expression to visit.</param>
        void VisitDateTimeOffset(ExprDateTimeOffset dateTimeOffset);

        /// <summary>
        /// Visits a GUID constant expression.
        /// Used to generate properly formatted GUID/UUID literals in SQL.
        /// </summary>
        /// <param name="guid">The GUID expression to visit.</param>
        void VisitGuid(ExprGuid guid);

        /// <summary>
        /// Visits a NULL expression.
        /// Used to generate NULL values in SQL.
        /// </summary>
        /// <param name="null">The NULL expression to visit.</param>
        void VisitNull(ExprNull @null);

        ///// <summary>
        ///// Visits a less than predicate expression.
        ///// Used to generate SQL less than comparisons (e.g., "column < value").
        ///// </summary>
        ///// <param name="lessThan">The less than predicate to visit.</param>
        //void VisitLessThan(ExprBoolLessThan lessThan);

        ///// <summary>
        ///// Visits a less than or equal predicate expression.
        ///// Used to generate SQL less than or equal comparisons (e.g., "column <= value").
        ///// </summary>
        ///// <param name="lessThanOrEqual">The less than or equal predicate to visit.</param>
        //void VisitLessThanOrEqual(ExprBoolLessThanOrEqual lessThanOrEqual);

        ///// <summary>
        ///// Visits a greater than predicate expression.
        ///// Used to generate SQL greater than comparisons (e.g., "column > value").
        ///// </summary>
        ///// <param name="greaterThan">The greater than predicate to visit.</param>
        //void VisitGreaterThan(ExprBoolGreaterThan greaterThan);

        ///// <summary>
        ///// Visits a greater than or equal predicate expression.
        ///// Used to generate SQL greater than or equal comparisons (e.g., "column >= value").
        ///// </summary>
        ///// <param name="greaterThanOrEqual">The greater than or equal predicate to visit.</param>
        //void VisitGreaterThanOrEqual(ExprBoolGreaterThanOrEqual greaterThanOrEqual);

        void VisitOrderColumn(ExprOrderColumn order);

        void VisitListColumns(ExprListColumns listColumns);

        void VisitParam(ExprParam param);

        void VisitListJoins(ExprListJoins listJoins);

        void VisitTableName(ExprTableName table);

        void VisitFrom(ExprFrom from);

        void VisitJoin(ExprJoin join);

        void VisitInnerJoin(ExprInnerJoin innerJoin);

        void VisitLeftJoin(ExprLeftJoin leftJoin);

        void VisitRightJoin(ExprRightJoin rightJoin);

        void VisitSelectedColumn(ExprSelectedColumn selectedColumn);

        void VisitSelect(ExprSelect select);

        void VisitSql(ExprSql sql);

        void VisitWhere(ExprWhere where);

        void VisitOrderBy(ExprOrderBy orderBy);

        void VisitFunction(ExprFunction function);

        void VisitStar(ExprStar star);

        void VisitLike(ExprLike like);

        void VisitIn(ExprIn exprIn);

        void VisitArray(ExprArray array);

        void VisitBoolEmpty(ExprBoolEmpty empty);

        void VisitGreaterThanPredicate(ExprGreaterThan greaterPredicate);

        void VisitLessThanPredicate(ExprLessThan lessPredicate);

        void VisitGreaterThanOrEqPredicate(ExprGreaterThanOrEq greaterEqPredicate);

        void VisitLessThanOrEqPredicate(ExprLessThanOrEq lesssEqPredicate);

        void VisitConstValue(string value);
    }
}
