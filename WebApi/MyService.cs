using Microsoft.Extensions.Logging;

namespace WebApi
{
    public interface ILogService
    {
        void Log(string message);
    }


    public class NewLogService : ILogService
    {
        private readonly ILogger<NewLogService> logger;

        public NewLogService(ILogger<NewLogService> logger)
        {
            this.logger = logger;
        }
        public void Log(string message)
        {
            logger.LogInformation($"bazingaaaaaaaaa + {message}");
        }
    }

    public class MyService: ILogService
    {
        private readonly MyConfiguration myConfiguration;
        private readonly ILogger<MyService> logger;

        public MyService(MyConfiguration myConfiguration, ILogger<MyService> logger)
        {
            this.myConfiguration = myConfiguration;
            this.logger = logger;
        }

        public void Log(string message)
        {
            logger.LogInformation("{Message} : {MyProperty} {Level}", message, myConfiguration.MyProperty, myConfiguration.Level);
            logger.LogInformation("{Message} : {Configuration}", message, myConfiguration);

            Console.WriteLine(message);
        }
    }

    public class MyConfiguration
    {
        public int MyProperty { get; set; }

        public string Level { get; set; }
    }
}
