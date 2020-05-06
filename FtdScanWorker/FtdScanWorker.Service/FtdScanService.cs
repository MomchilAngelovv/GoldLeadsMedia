using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FtdScanWorker.Service
{
    public class FtdScanService : IHostedService, IDisposable
    {
        private Timer timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(Scan, null, TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(15));
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

            //var response = httpCleint.PostAsync("https://localhost:44322/api/clicks/test", null).GetAwaiter().GetResult();
            //var responseAsString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            //Console.WriteLine(responseAsString);
        }
    }
}
