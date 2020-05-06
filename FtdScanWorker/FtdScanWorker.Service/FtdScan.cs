using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FtdScanWorker.Service
{
    public class FtdScan : IHostedService, IDisposable
    {
        private readonly Timer timer;

        public FtdScan()
        {
            this.timer = new Timer(Scan, null, TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void Scan(object state)
        {
            var httpCleint = new HttpClient();

            var response = httpCleint.PostAsync("https://localhost:44322/api/clicks/test", null).GetAwaiter().GetResult();
            var responseAsString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine(responseAsString);
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
    }
}
