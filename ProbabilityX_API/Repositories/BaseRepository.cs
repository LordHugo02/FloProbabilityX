using ProbabilityX_API.Extensions;

namespace ProbabilityX_API.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ProbabilityXContext _context;

        public BaseRepository(ProbabilityXContext context)
        {
            _context = context;
        }
    }
}
