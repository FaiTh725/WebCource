using System.Linq.Expressions;

namespace Test.Domain.Primitives
{
    public abstract class Specification<T>
    {
        public Expression<Func<T, bool>>? CreteriaExpression { get; private set; }

        public List<Expression<Func<T, object>>> IncludeExpressions { get; private set; } = new();

        public List<string> IncludeExpressionsStrings { get; private set; } = new();

        public Expression<Func<T, object>>? OrderByExpression { get; private set; }

        public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        protected void AddInclude(string expressionInclude)
        {
            IncludeExpressionsStrings.Add(expressionInclude);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderByExpression = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpressionDescending)
        {
            OrderByDescendingExpression = orderByExpressionDescending;
        }

        protected void AddCreteria(Expression<Func<T, bool>>? creteriaExpression)
        {
            CreteriaExpression = creteriaExpression;
        }
    }
}
