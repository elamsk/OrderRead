using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace OrderRead.Infra
{
    public interface IServiceBusReceiver
    {
        void ReceiveMessage();

    }
    public class ServiceBusReceiver : IServiceBusReceiver
    {
        private string _connectionstring;
        private string _topic;
        private string _subscriptionname;
        private SubscriptionClient client;

        public ServiceBusReceiver(string connectionstring, string topic, string subscriptionname)
        {
            _connectionstring = connectionstring;
            _topic = topic;
            _subscriptionname = subscriptionname;
            client = new SubscriptionClient(_connectionstring, _topic, _subscriptionname);
        }

        public void ReceiveMessage()
        {
            MessageHandlerOptions options = new MessageHandlerOptions(exceptionhandler)
            {
                AutoComplete = true,
                MaxConcurrentCalls = 1
            };
            client.RegisterMessageHandler(getmessage, options);
        }

        private Task getmessage(Message arg1, CancellationToken arg2)
        {
            string inmsg = System.Text.Encoding.ASCII.GetString(arg1.Body);
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(inmsg);

            switch (obj.EventType.ToString())
            {
                case "OrderCreated":
                    break;
                default:
                    break;
            }
            client.CompleteAsync(arg1.SystemProperties.LockToken).GetAwaiter().GetResult();
            return Task.CompletedTask;
        }

        private Task exceptionhandler(ExceptionReceivedEventArgs arg)
        {
            var aa = 1;
            return Task.CompletedTask;
        }
    }
}
