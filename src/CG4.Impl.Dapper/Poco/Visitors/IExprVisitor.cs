using CG4.Impl.Dapper.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco
{
    public interface IExprVisitor
    {
        void VisitColumn(ExprColumn column);

        void VisitEqPredicate(ExprBoolEqPredicate eqPredicate);

        void VisitNotEqPredicate(ExprBoolNotEqPredicate eqPredicate);

        void VisitOr(ExprBoolOr or);

        void VisitAnd(ExprBoolAnd and);

        void VisitStr(ExprStr str);

        void VisitLong(ExprLong @long);

        void VisitNot(ExprNot not);

        void VisitInt(ExprInt @int);

        void VisitDateTime(ExprDateTimeOffset dateTime);

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

        void VisitNull(ExprNull exprNull);

        void VisitBool(ExprBool exprBool);

        void VisitLike(ExprLike like);

        void VisitIn(ExprIn exprIn);

        void VisitArray(ExprArray array);

        void VisitBoolEmpty(ExprBoolEmpty empty);

        void VisitGreaterThanPredicate(ExprGreaterThan greaterPredicate);

        void VisitLessThanPredicate(ExprLessThan lessPredicate);

        void VisitGreaterThanOrEqPredicate(ExprGreaterThanOrEq greaterEqPredicate);

        void VisitLessThanOrEqPredicate(ExprLessThanOrEq lesssEqPredicate);
    }
}
