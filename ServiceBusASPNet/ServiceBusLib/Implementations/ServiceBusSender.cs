using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusLib.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusLib.Implementations
{
    public class ServiceBusSender : IServiceBusSender
    {
        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "myqueue";
        private string ServiceBus_Connection = "Endpoint=sb://techmind.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=INpu4EgVeNtbMVq1jPRP03a52pTDrkOVuR2oaNbqpak=";
        public ServiceBusSender()
        {
            _queueClient = new QueueClient(ServiceBus_Connection, QUEUE_NAME);
        }
        public async Task SendMessage(MyPayload payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }
    }
}
