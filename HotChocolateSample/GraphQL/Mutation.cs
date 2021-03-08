using HotChocolateSample.Core;

namespace HotChocolateSample.GraphQL
{
    // sample use: 
    /*mutation {
        createCompany(inputCompany: {
            name: "Tnuva",
            revenue: 210000
        })
        {
            id
            name
            revenue
        }
    }
    */

    public class Mutation
    {
        private readonly ICompanyService _companyService;

        public Mutation(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public Company CreateCompany(CreateCompanyInput inputCompany)
        {
            return _companyService.Create(inputCompany);
        }

        // sample use: 
        /*mutation {
            createCompany(inputCompany: {
                name: "Tnuva",
                revenue: 210000
            })
            {
                id
                name
                revenue
            }
        }
        */
        //test
        public Company DeleteCompany(DeleteCompanyInput inputCompany)
        {
            return _companyService.Delete(inputCompany);
        }

        // sample use:
        /*
          mutation {
              deleteCompany(inputCompany: {
                id: 71
              })
              {
                id
                name
                revenue
              }
            }
         */
    }
}