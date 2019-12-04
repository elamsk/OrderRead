using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderRead.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

namespace OrderRead
{
    public class ServiceBusHosting : IHostedService
    {
        private IServiceProvider _provider;
        public ServiceBusHosting(IServiceProvider serviceprovider)
        {
            _provider = serviceprovider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using(var scope = _provider.CreateScope())
            {
                IServiceBusReceiver rec = scope.ServiceProvider.GetRequiredService<IServiceBusReceiver>();
                rec.ReceiveMessage();
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var aa = 1;
            return Task.CompletedTask;
        }
    }
}
