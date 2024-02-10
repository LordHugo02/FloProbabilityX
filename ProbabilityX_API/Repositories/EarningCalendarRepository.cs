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
    }
}