using ServiceBusLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receiver
{
    public class ProcessData : IProcessData
    {
        public Task Process(MyPayload myPayload)
        {
            //TO DO: write data to db or else 

            throw new NotImplementedException();
        }
    }
}
