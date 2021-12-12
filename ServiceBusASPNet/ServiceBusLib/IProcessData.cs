using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusLib
{
    public interface IProcessData
    {
        Task Process(MyPayload myPayload);
    }
}
