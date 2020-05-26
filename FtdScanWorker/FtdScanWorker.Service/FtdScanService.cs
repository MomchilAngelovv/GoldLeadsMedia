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
            this.timer = new Timer(Scan, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(60));
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
            var httpCleint = new HttpClient();

            var coreApiUrl = this.configuration["CoreApiUrl"];
            var response = httpCleint.PostAsync($"{coreApiUrl}/Api/Partners/FtdScan", null).GetAwaiter().GetResult();
            var responseAsString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Console.WriteLine($"[Ftd Scan: {DateTime.UtcNow}] --- [Scan result: {responseAsString}]");
        }
    }
}
