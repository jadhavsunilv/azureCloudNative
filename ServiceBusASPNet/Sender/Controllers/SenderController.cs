using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusLib;
using ServiceBusLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        IServiceBusSender _sender;
        public SenderController(IServiceBusSender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Create([FromQuery]string message)
        {
            MyPayload payload = new MyPayload { Message = message, CreatedDate = DateTime.Now };
            await _sender.SendMessage(payload);
            return Ok();
        }
    }
}
