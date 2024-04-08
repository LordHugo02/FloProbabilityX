using ProbabilityX_API.Models;

namespace ProbabilityX_API.IServices
{
    public interface IRelativeStrengthIndexService
    {
        Task<List<RelativeStrengthIndexModel>> GetExtremeRelativeStrengthIndex();
    }
}
