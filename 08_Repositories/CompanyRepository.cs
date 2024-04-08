using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.IRepositories;
using ProbabilityX_API.Models;
using ProbabilityX_API.Settings;

namespace ProbabilityX_API.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(ProbabilityXContext context) : base(context)
        {
        }

        public async Task<List<CompanyModel>> GetAllCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<CompanyModel> GetCompanyById(int companyId)
        {
            // Implement GetCompanyById using the context
            throw new NotImplementedException();
        }

        public async Task<CompanyModel> GetCompanyByName(string companyName)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyName == companyName);
            return company;
        }

        public async Task<CompanyModel> AddCompany(CompanyModel company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<CompanyModel> UpdateCompany(CompanyModel company)
        {
            // Implement UpdateCompany using the context
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            // Implement DeleteCompany using the context
            throw new NotImplementedException();
        }
    }
}
