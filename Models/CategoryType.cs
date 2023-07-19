using System.Text.Json.Serialization;

namespace dotnet_nopreco.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CategoryType
    {
        Despensa = 1,
        Geladeira = 2,
        Bebida = 3,
        Limpeza = 4,
        Higiene = 5,
        Utilidade = 6
    }
}