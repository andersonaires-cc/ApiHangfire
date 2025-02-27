using apiHangfire.Services;
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
        public JobTestController(IJobTestService jobTestService, IBackgroundJobClient backgroundJobClient)
        {
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
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

    }
}
