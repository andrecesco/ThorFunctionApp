using System;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;

namespace ThorFunctionApp
{
    public class ContatoEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }
        public string Email { get; set; }
        public string Biotipo { get; set; }
        public string Genero { get; set; }
        public int? Idade { get; set; }
        public int? Altura { get; set; }
        public int? Peso { get; set; }
        public string FrequenciaTreino { get; set; }
        public double? MetaCalorica { get; set; }
        public double? MetaProteica { get; set; }
        private string MensagemDeErro { get; set; }

        public ContatoEntity()
        {
            PartitionKey = Guid.NewGuid().ToString();
            RowKey = DateTime.Now.Ticks.ToString();
            Timestamp = DateTime.Now;
        }

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            throw new NotImplementedException();
        }

        public bool Validar()
        {
            if (String.IsNullOrWhiteSpace(Email))
            {
                MensagemDeErro = "O e-mail é obrigatório";
                return false;
            }

            if (String.IsNullOrWhiteSpace(Biotipo))
            {
                MensagemDeErro = "O biotipo é obrigatório";
                return false;
            }

            if (String.IsNullOrWhiteSpace(Genero))
            {
                MensagemDeErro = "O genêro é obrigatório";
                return false;
            }

            if (!Idade.HasValue)
            {
                MensagemDeErro = "A idade é obrigatória";
                return false;
            }

            if (!Altura.HasValue)
            {
                MensagemDeErro = "A altura é obrigatória";
                return false;
            }

            if (!Peso.HasValue)
            {
                MensagemDeErro = "O peso é obrigatório";
                return false;
            }

            if (String.IsNullOrWhiteSpace(FrequenciaTreino))
            {
                MensagemDeErro = "A frequência de treino é obrigatória";
                return false;
            }

            if (!MetaCalorica.HasValue)
            {
                MensagemDeErro = "A meta calórica é obrigatória";
                return false;
            }

            if (!MetaProteica.HasValue)
            {
                MensagemDeErro = "A meta proteíca é obrigatória";
                return false;
            }

            return true;
        }

        public string ObterMensagemDeErro()
        {
            if (!string.IsNullOrWhiteSpace(MensagemDeErro))
            {
                return MensagemDeErro;
            }
            else
            {
                throw new Exception("Método foi chamado, porém não tem mensagem de erro.");
            }
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var retorno = new Dictionary<string, EntityProperty>
            {
                { nameof(PartitionKey), EntityProperty.GeneratePropertyForString(PartitionKey) },
                { nameof(RowKey), EntityProperty.GeneratePropertyForString(RowKey) },
                { nameof(Timestamp), EntityProperty.GeneratePropertyForDateTimeOffset(Timestamp) },
                { nameof(ETag), EntityProperty.GeneratePropertyForString(ETag) },
                { nameof(Email), EntityProperty.GeneratePropertyForString(Email) },
                { nameof(Biotipo), EntityProperty.GeneratePropertyForString(Biotipo) },
                { nameof(Genero), EntityProperty.GeneratePropertyForString(Genero) },
                { nameof(Idade), EntityProperty.GeneratePropertyForInt(Idade) },
                { nameof(Altura), EntityProperty.GeneratePropertyForInt(Altura) },
                { nameof(Peso), EntityProperty.GeneratePropertyForInt(Peso) },
                { nameof(FrequenciaTreino), EntityProperty.GeneratePropertyForString(FrequenciaTreino) },
                { nameof(MetaCalorica), EntityProperty.GeneratePropertyForDouble(MetaCalorica) },
                { nameof(MetaProteica), EntityProperty.GeneratePropertyForDouble(MetaProteica) }
            };

            return retorno;
        }
    }
}
