﻿using apiHangfire.Services;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTestController : ControllerBase
    {
        private readonly IJobTestService _jobTestService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        public JobTestController
        (
            IJobTestService jobTestService, 
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob() 
        {
            _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet("/Delayedjob")]
        public ActionResult CreateDelayedjob() 
        {
            _backgroundJobClient.Schedule(() => _jobTestService.Delayedjob(), TimeSpan.FromSeconds(60));
            return Ok();
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobTestService.ReccuringJob(), Cron.Minutely);
            return Ok();
        }

        [HttpGet("/ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _jobTestService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobTestService.ContinuationJob());

            return Ok();
        }

    }
}
