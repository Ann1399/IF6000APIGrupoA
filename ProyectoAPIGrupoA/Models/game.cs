using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoAPIGrupoA.Models
{
    public class game
    {
        private gameId id;
        private gameName name;
        private bool password;
        private List<gamePlayerName> players;
        private List<gamePlayerName> enemies;
        [DefaultValue("lobby")]
        private GameStatus status;
        private roundId currentRound;

        public game()
        {


        }

        public gameId Id { get => id; set => id = value; }
        public gameName Name { get => name; set => name = value; }
        public bool Password { get => password; set => password = value; }

        public GameStatus Status { get => status; set => status = value; }
        public roundId CurrentRound { get => currentRound; set => currentRound = value; }
        [StringLength(10, MinimumLength = 1)]
        public List<gamePlayerName> Players { get => players; set => players = value; }
        [StringLength(5, MinimumLength = 0)]
        public List<gamePlayerName> Enemies { get => enemies; set => enemies = value; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum GameStatus
        {
            Lobby,
            Rounds,
            Ended
        }
    }
}
