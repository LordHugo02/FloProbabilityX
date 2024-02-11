using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProbabilityX_API.HostedServices
{
    public abstract class BaseHostedService : IHostedService, IDisposable
    {
        public IServiceProvider Services { get; }

        protected Timer ServiceTimer { get; set; }
        protected int StartInSeconds { get; set; } = 30;
        protected int PeriodInSeconds { get; set; } = 60;

        public BaseHostedService(IServiceProvider services)
        {
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ServiceTimer = new Timer(DoWork, null, TimeSpan.FromSeconds(StartInSeconds), TimeSpan.FromSeconds(PeriodInSeconds));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            ServiceTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        protected abstract void DoWork(object state);

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ServiceTimer?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}