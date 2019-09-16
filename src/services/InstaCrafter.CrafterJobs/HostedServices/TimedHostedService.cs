using System;
using System.Threading;
using System.Threading.Tasks;
using InstaCrafter.CrafterJobs.DataProvider;
using InstaCrafter.CrafterJobs.DtoModels;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.EventBus.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.CrafterJobs.HostedServices
{
    internal abstract class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IDataAccessProvider<InstaCrafterJobDto> _jobsRepo;
        private readonly IEventBus _eventBus;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, 
            IDataAccessProvider<InstaCrafterJobDto> jobsRepo, 
            IEventBus eventBus)
        {
            _logger = logger;
            _jobsRepo = jobsRepo;
            _eventBus = eventBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, 
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogDebug("Timed Background Service is working.");
            var jobs = _jobsRepo.GetItems();
            foreach (var jobDto in jobs)
            {
                _logger.LogDebug($"Publishing new crafter job: {jobDto}");
                _eventBus.Publish(new NewCrafterJobMessage(jobDto.JobType, jobDto.UserName));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}