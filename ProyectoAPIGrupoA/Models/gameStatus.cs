using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProyectoAPIGrupoA.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameStatus
    {
        lobby,
        rounds,
        ended
    }
}
