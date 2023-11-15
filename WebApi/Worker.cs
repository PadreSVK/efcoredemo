using Microsoft.Extensions.Options;

namespace WebApi
{
    public class Worker : BackgroundService
    {
        private readonly ILogService myservice;
        private readonly MyAwesomeConfig options;

        public Worker(ILogService myService, IOptions<MyAwesomeConfig> options)
        //public Worker(IServiceProvider serviceProvider)
        {
            myservice = myService;
            this.options = options.Value;
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
