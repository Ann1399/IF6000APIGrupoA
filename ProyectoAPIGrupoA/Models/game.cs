using Swashbuckle.AspNetCore.Annotations;
using System;
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
        private string createdAt { get; set; }
        private string updatedAt { get; set; }
        private List<string> players;
        private List<gamePlayerName> enemies;
        [DefaultValue("lobby")]
        private GameStatus status;
        private roundId currentRound;
        public game(string name, string owner, string password)
        {
            this.id = new gameId(); ;
            this.name = new gameName(name);
            this.players = new List<string>();
            if(password != null && password != "") {
                this.password = true;
            }
            this.players.Add(owner);
            this.currentRound = new roundId("0000000000000000000000000000000000000");
            this.enemies = new List<gamePlayerName>();
            this.status = GameStatus.Lobby;
            DateTime fechaActual = DateTime.Now;
            this.createdAt = fechaActual.ToString("yyyy-MM-dd HH:mm:ss");
            this.updatedAt = fechaActual.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public gameId Id { get => id; set => id = value; }
        public gameName Name { get => name; set => name = value; }
        public bool Password { get => password; set => password = value; }

        public GameStatus Status { get => status; set => status = value; }
        public roundId CurrentRound { get => currentRound; set => currentRound = value; }
        [StringLength(10, MinimumLength = 1)]
        //[SwaggerSchema(Description = "A collection of players.")]
        public List<string> Players { get => players; set => players = value; }
        [StringLength(5, MinimumLength = 0)]
        public List<gamePlayerName> Enemies { get => enemies; set => enemies = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum GameStatus
        {
            Lobby,
            Rounds,
            Ended
        }
    }
}
