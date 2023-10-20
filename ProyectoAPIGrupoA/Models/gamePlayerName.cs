using ProyectoAPIGrupoA.Models;

namespace ProyectoIIRedesAPI.Models
{
    public class gamePlayerName
    {
        [StringLength(20, MinimumLength = 3)]
        private string playerName;

        public gamePlayerName(string playerName)
        {
            this.playerName = playerName;
        }

        [SwaggerSchemaExample("Thanos")]
        [StringLength(20, MinimumLength = 3)]
        public string PlayerName { get => playerName; set => playerName = value; }
    }
}
