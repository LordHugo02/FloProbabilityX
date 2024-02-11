using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.IRepositories;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProbabilityX_API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<CompanyModel>> GetAllCompanies()
        {
            return await _companyRepository.GetAllCompanies();
        }

        public async Task<CompanyModel> GetCompanyById(int companyId)
        {
            return await _companyRepository.GetCompanyById(companyId);
        }
        
        public async Task<CompanyModel> GetCompanyByName(string companyName)
        {
            return await _companyRepository.GetCompanyByName(companyName);
        }

        public async Task<CompanyModel> AddCompany(CompanyModel company)
        {
            // Vérifier si l'entreprise existe déjà par son nom
            var existingCompany = await GetCompanyByName(company.CompanyName);

            if (existingCompany == null)
            {
                // L'entreprise n'existe pas encore, vous pouvez l'ajouter
                return await _companyRepository.AddCompany(company);
            }
            else
            {
                // L'entreprise existe déjà, vous pouvez prendre des mesures appropriées
                Console.WriteLine("L'entreprise existe déjà dans la base de données.");
                // Vous pouvez choisir de ne pas ajouter la nouvelle entreprise ou prendre d'autres mesures nécessaires
                return null; // Indiquer que l'ajout n'a pas été effectué en raison de la duplication
            }
        }


        public async Task<CompanyModel> UpdateCompany(CompanyModel company)
        {
            return await _companyRepository.UpdateCompany(company);

        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            return await _companyRepository.DeleteCompany(companyId);

        }
    }
}
