using System.Threading.Tasks;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.IRepositories
{
    public interface IEarningCalendarRepository
    {

        Task<EarningCalendarModel> AddEarningCalendarResult(EarningCalendarModel model);

        Task<EarningCalendarModel> GetEarningCalendarResultByCompanyAndDateOut(EarningCalendarModel model);

    }
}
