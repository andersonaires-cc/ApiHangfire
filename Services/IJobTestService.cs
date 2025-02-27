namespace apiHangfire.Services
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        void ReccuringJob();
        void Delayedjob();
        void ContinuationJob();
    }
}
