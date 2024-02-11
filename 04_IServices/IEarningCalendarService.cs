using ProbabilityX_API.Models;

namespace ProbabilityX_API.IServices
{
    public interface IEarningCalendarService
    {
        Task<List<EarningCalendarModel>> ScrapperNextWeekEarningCalendar();
    }
}
