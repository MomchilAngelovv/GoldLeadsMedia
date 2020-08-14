namespace FtdScanWorker.Service
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;

    public class FtdScanService : IHostedService, IDisposable
    {
        private Timer timer;

        private readonly IConfiguration configuration;

        public FtdScanService(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var timeBeforeStart = TimeSpan.FromMinutes(5);
            var timeInterval = TimeSpan.FromHours(4);

            this.timer = new Timer(Scan, null, timeBeforeStart, timeInterval);

            Console.WriteLine($"Scan will start after {timeBeforeStart} [hh/mm/ss] and will scan every {timeInterval} [hh/mm/ss]");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }

        private void Scan(object state)
        {
            try
            {
                var httpCleint = new HttpClient();
                var coreApiUrl = "https://coreapi.goldleadsmedia.com";

                var response = httpCleint.PostAsync($"{coreApiUrl}/Brokers/FtdScan", null).GetAwaiter().GetResult();
                var responseAsString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Console.WriteLine($"[Ftd Scan time: {DateTime.UtcNow}] --- [Scan result: {responseAsString}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong:");
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
