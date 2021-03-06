using System.Linq;

namespace HotChocolateSample.Core
{
    public interface ICompanyService
    {
        IQueryable<Company> GetAll();
        Company Create(CreateCompanyInput inputCompany);
        Company Delete(DeleteCompanyInput inputCompany);
    }

    public class CompanyService : ICompanyService
    {
        private readonly CompanyDbContext _context;

        public CompanyService(CompanyDbContext context)
        {
            _context = context;
        }

        public IQueryable<Company> GetAll()
        {
            return _context.Companies;
        }

        public Company Create(CreateCompanyInput inputCompany)
        {
            var company = _context.Companies.Add(new Company {Name = inputCompany.Name, Revenue = inputCompany.Revenue});
            _context.SaveChanges();
            return company.Entity;
        }

        public Company Delete(DeleteCompanyInput inputCompany)
        {
            var company = _context.Companies.Find(inputCompany.Id);
            _context.Companies.Remove(company);
            _context.SaveChanges();
            return company;
        }
    }


    public class DeleteCompanyInput
    {
        public int Id { get; set; }
    }

    public class CreateCompanyInput
    {
        public string Name { get; set; }
        public decimal Revenue { get; set; }
    }
}