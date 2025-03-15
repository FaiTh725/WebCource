using TestVariantEntity = Test.Domain.Entities.TestVariant;
using Test.Domain.Primitives;

namespace Test.Application.Queries.TestVariant.Specifications
{
    public class GetVariantsByListId : Specification<TestVariantEntity>
    {
        public GetVariantsByListId(
            List<long> listId)
        {
            AddCreteria(x => listId.Contains(x.Id));
        }
    }
}
