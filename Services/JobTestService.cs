namespace apiHangfire.Services
{
    public class JobTestService: IJobTestService
    {
        private ILogger _logger;

        public JobTestService(ILogger<JobTestService> logger)
        {
            _logger = logger;
        }

        public void ContinuationJob()
        {
            _logger.LogInformation("Hello from a Continuation job !");
        }

        public void Delayedjob()
        {
            _logger.LogInformation("Hello from a Delayed job !");
        }

        public void FireAndForgetJob()
        {
            _logger.LogInformation("Hello from a Forget job !");
        }

        public void ReccuringJob()
        {
            _logger.LogInformation("Hello from a Scheduled job !");
        }
    }
}
