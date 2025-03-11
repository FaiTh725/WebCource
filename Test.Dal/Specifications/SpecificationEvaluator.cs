using Test.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Test.Dal.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(
            IQueryable<T> inputQueryable,
            Specification<T> specification) 
            where T : class
        {
            IQueryable<T> queryable = inputQueryable;

            if(specification.CreteriaExpression is not null)
            {
                queryable = queryable.Where(specification.CreteriaExpression);
            }

            queryable = specification.IncludeExpressions.Aggregate(
                queryable,
                (current, includeExpression) =>
                current.Include(includeExpression));

            if(specification.OrderByExpression is not null)
            {
                queryable = queryable
                    .OrderBy(specification.OrderByExpression);
            }

            if (specification.OrderByDescendingExpression is not null)
            {
                queryable = queryable
                    .OrderBy(specification.OrderByDescendingExpression);
            }

            return queryable;
        }
    }
}
