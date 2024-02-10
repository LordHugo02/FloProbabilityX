using ProbabilityX_API.Models;

namespace ProbabilityX_API.IServices
{
    public interface ICompanyService
    {
        Task<List<CompanyModel>> GetAllCompanies();
        Task<CompanyModel> GetCompanyById(int companyId);  // Add parameter for company ID
        Task<CompanyModel> GetCompanyByName(string companyName);
        Task<CompanyModel> AddCompany(CompanyModel company);
        Task<CompanyModel> UpdateCompany(CompanyModel company);
        Task<bool> DeleteCompany(int companyId);  // Return a boolean indicating success/failure
    }
}
