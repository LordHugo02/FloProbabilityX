using System.Threading.Tasks;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.IRepositories
{
    public interface ICompanyRepository
    {
        Task<List<CompanyModel>> GetAllCompanies();
        Task<CompanyModel> GetCompanyById(int companyId);
        Task<CompanyModel> GetCompanyByName(string companyName);
        Task<CompanyModel> AddCompany(CompanyModel company);
        Task<CompanyModel> UpdateCompany(CompanyModel company);
        Task<bool> DeleteCompany(int companyId);
    }
}
