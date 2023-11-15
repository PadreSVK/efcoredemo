
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Text;

namespace WebApi
{
    public class BetterWorker : BackgroundService
    {
        private readonly ILogService myservice;

        public BetterWorker(ILogService myService)
        {
            myservice = myService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested is false)
            {
                myservice.Log("BetterWorker log");
                await Task.Delay(1000);
            }
        }
    }
}
