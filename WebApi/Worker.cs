namespace WebApi
{
    public class Worker : BackgroundService
    {
        private readonly ILogService myservice;

        public Worker(ILogService myService, IConfiguration configuration)
        //public Worker(IServiceProvider serviceProvider)
        {
            myservice = myService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested is false)
            {
                myservice.Log("ahopjjojojo");
                await Task.Delay(1000);
            }
        }
    }
}
