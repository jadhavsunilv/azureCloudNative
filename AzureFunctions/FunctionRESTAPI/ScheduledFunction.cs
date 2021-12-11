
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ServerlessFuncs
{
    public static class ScheduledFunction
    {
        [FunctionName("TimerTriggerCSharp")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            if (myTimer.IsPastDue)
            {
                log.LogInformation("Timer is running late!");
            }
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
