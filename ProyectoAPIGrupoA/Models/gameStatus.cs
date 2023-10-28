using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ProyectoAPIGrupoA.Models
{
    
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameStatus
    {
        /// <summary>
        /// Possible values are 'lobby' (default), 'rounds', and 'ended'.
        /// </summary>
        [EnumMember(Value = "lobby")]
        lobby,
        [EnumMember(Value = "rounds")]
        rounds,
        [EnumMember(Value = "ended")]
        ended
    }
}
