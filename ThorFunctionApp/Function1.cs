using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ThorFunctionApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("myqueue-items", Connection = "")]string myQueueItem,
            [Blob("samples-workitems/{queueTrigger}", Connection = "StorageConnectionAppSetting")] string contato,
            ILogger log)
        {

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
