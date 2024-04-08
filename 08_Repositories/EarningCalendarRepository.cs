using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.IRepositories;
using ProbabilityX_API.Models;
using ProbabilityX_API.Settings;

namespace ProbabilityX_API.Repositories
{
    public class EarningCalendarRepository : BaseRepository, IEarningCalendarRepository
    {
        public EarningCalendarRepository(ProbabilityXContext context) : base(context)
        {
        }
        public async Task<EarningCalendarModel> GetEarningCalendarResultByCompanyAndDateOut(EarningCalendarModel model)
        {
            var result = await _context.EarningsCalendar.FirstOrDefaultAsync(ec => ec.Id_Company == model.Id_Company && ec.ResultDate == model.ResultDate);
            return result;
        }

        public async Task<EarningCalendarModel> AddEarningCalendarResult(EarningCalendarModel model)
        {
            _context.EarningsCalendar.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        
       
    }
}