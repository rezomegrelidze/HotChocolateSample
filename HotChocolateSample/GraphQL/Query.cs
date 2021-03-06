using System.Linq;
using HotChocolate.Types;
using HotChocolateSample.Core;

namespace HotChocolateSample.GraphQL
{
    public class Query
    {
        private ICompanyService _companyService;

        public Query(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [UsePaging(typeof(CompanyType))]
        [UseFiltering]
        public IQueryable<Company> Companies => _companyService.GetAll();
    }
}