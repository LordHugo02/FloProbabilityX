using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Models;

namespace ProbabilityX_DataFetcher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EarningCalendarController : ControllerBase
    {
        private readonly IEarningCalendarService _earningCalendarService;

        public EarningCalendarController(IEarningCalendarService earningCalendarService)
        {
            _earningCalendarService = earningCalendarService;
        }

        [HttpPost]
        public async Task<IActionResult> ScrapperEarningCalendarNextWeek()
        {
            var earningCalendar = await _earningCalendarService.ScrapperNextWeekEarningCalendar();
            return Ok(earningCalendar);
        }

       
    }
}
