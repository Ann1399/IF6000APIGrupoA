using ProyectoAPIGrupoA.Models;

namespace ProyectoIIRedesAPI.Models
{
    public class gamePlayerName
    {
        private string playerName;

        public gamePlayerName(string playerName)
        {
            this.playerName = playerName;
        }

        [SwaggerSchemaExample("Thanos")]
        public string PlayerName { get => playerName; set => playerName = value; }
    }
}
