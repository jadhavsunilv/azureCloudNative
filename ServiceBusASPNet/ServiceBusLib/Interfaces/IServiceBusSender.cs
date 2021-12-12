using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusLib.Interfaces
{
    public interface IServiceBusSender
    {
        Task SendMessage(MyPayload payload);
    }
}
