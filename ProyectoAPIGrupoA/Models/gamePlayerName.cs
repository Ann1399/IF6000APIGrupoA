using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPIGrupoA.Models
{
    public class gamePlayerName
    {
        [StringLength(20, MinimumLength = 3)]
        [JsonProperty("player")]
        private string playerName;

        public gamePlayerName(string playerName)
        {
            this.playerName = playerName;
        }
        public string PlayerName { get => playerName; set => playerName = value; }
    }
}
