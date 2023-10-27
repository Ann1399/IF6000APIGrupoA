using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ProyectoAPIGrupoA.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameStatus
    {
        [EnumMember(Value = "lobby")]
        lobby,
        [EnumMember(Value = "rounds")]
        rounds,
        [EnumMember(Value = "ended")]
        ended
    }
}
