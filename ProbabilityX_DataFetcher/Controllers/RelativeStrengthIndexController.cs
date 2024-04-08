using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Models;

namespace ProbabilityX_DataFetcher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelativeStrengthIndexController : ControllerBase
    {
        private readonly IRelativeStrengthIndexService _relativeStrengthIndexService;

        public RelativeStrengthIndexController(IRelativeStrengthIndexService relativeStrengthIndexService)
        {
            _relativeStrengthIndexService = relativeStrengthIndexService;
        }

        [HttpPost]
        public async Task<IActionResult> GetExtremeRelativeStrengthIndex()
        {
            var relativeStrengthIndex = await _relativeStrengthIndexService.GetExtremeRelativeStrengthIndex();
            return Ok(relativeStrengthIndex);
        }

       
    }
}
