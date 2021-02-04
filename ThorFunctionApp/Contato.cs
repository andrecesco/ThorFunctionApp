using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;

namespace ThorFunctionApp
{
    public static class Contato
    {
        [FunctionName("SalvarContato")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Table("Contato", Connection = "AzureWebJobsStorage")] CloudTable cloudTable,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var contato = new ContatoEntity
            {
                Email = data?.email,
                Biotipo = data?.biotipo,
                Genero = data?.genero,
                Idade = data?.idade,
                Altura = data?.altura,
                Peso = data?.peso,
                FrequenciaTreino = data?.frequenciaTreino,
                MetaCalorica = data?.metaCalorica,
                MetaProteica = data?.metaProteica
            };

            if (!contato.Validar())
            {
                return new BadRequestObjectResult(contato.ObterMensagemDeErro());
            }

            await cloudTable.ExecuteAsync(TableOperation.Insert(contato));

            return new OkObjectResult($"ID: {contato.PartitionKey}");
        }
    }
}
