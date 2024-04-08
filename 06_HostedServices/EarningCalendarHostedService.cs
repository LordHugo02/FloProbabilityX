using Microsoft.Extensions.DependencyInjection;
using ProbabilityX_API.HostedServices;
using ProbabilityX_API.IServices;
using System;

namespace ProbabilityX_API.HostedServices
{
    public class ScrapperNextWeekEarningCalendarHostedService : BaseHostedService
    {
        public ScrapperNextWeekEarningCalendarHostedService(IServiceProvider services)
            : base(services)
        {
            StartInSeconds = 0; // démarrage au lancement
            PeriodInSeconds = 0; // uniquement au lancement
        }

        protected override void DoWork(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var updateStatuesService = scope.ServiceProvider.GetRequiredService<IEarningCalendarService>();
                updateStatuesService.ScrapperNextWeekEarningCalendar();
            }
        }
    }
}