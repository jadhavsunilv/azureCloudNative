using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceBusLib.Interfaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusLib.Implementations
{
    public class ServiceBusReceiver : IServiceBusReceiver
    {
        private readonly IProcessData _processData;
        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "myqueue";
        private readonly ILogger _logger;
        private string ServiceBus_Connection = "Endpoint=sb://techmind.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=INpu4EgVeNtbMVq1jPRP03a52pTDrkOVuR2oaNbqpak=";
        public ServiceBusReceiver(IProcessData processData,
            IConfiguration configuration,
            ILogger<ServiceBusReceiver> logger)
        {
            _processData = processData;
            _configuration = configuration;
            _logger = logger;

            // ServiceBus_Connection= _configuration.GetConnectionString("ServiceBusConnectionString")
            _queueClient = new QueueClient(ServiceBus_Connection, QUEUE_NAME);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var myPayload = JsonConvert.DeserializeObject<MyPayload>(Encoding.UTF8.GetString(message.Body));
            await _processData.Process(myPayload);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogDebug($"- Endpoint: {context.Endpoint}");
            _logger.LogDebug($"- Entity Path: {context.EntityPath}");
            _logger.LogDebug($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await _queueClient.CloseAsync();
        }
    }
}
