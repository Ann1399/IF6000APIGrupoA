using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ProyectoAPIGrupoA.Models
{
    public class game
    {
        private gameId id;
        private gameName name;
        private string owner;
        private bool password;
        [Newtonsoft.Json.JsonIgnore]
        private string pdw;
        private string createdAt { get; set; }
        private string updatedAt { get; set; }
        private List<gamePlayerName> players;
        private List<gamePlayerName> enemies;
        [DefaultValue("lobby")]
        private GameStatus status;
        private roundId currentRound;


        public game(string name, string owner, string password)
        {
            this.id = new gameId(); ;
            this.name = new gameName(name);
            this.owner = owner;
            this.players = new List<gamePlayerName>();
            if(password != null && password != "") {
                this.password = true;
                this.pdw = password;
            }
            this.players.Add(new gamePlayerName(owner));
            this.currentRound = new roundId("0000000000000000000000000");
            this.enemies = new List<gamePlayerName>();
            this.status = GameStatus.lobby;
            DateTime fechaActual = DateTime.Now;
            this.createdAt = fechaActual.ToString("yyyy-MM-dd HH:mm:ss");
            this.updatedAt = fechaActual.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public gameName Name { get => name; set => name = value; }
        public string Owner{ get => owner; set => owner = value; }
        public GameStatus Status { get => status; set => status = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public bool Password { get => password; set => password = value; }
        [StringLength(10, MinimumLength = 1)]
        public List<gamePlayerName> Players { get => players; set => players = value; }
        [StringLength(5, MinimumLength = 0)]
        public List<gamePlayerName> Enemies { get => enemies; set => enemies = value; }
        public roundId CurrentRound { get => currentRound; set => currentRound = value; }
        public gameId Id { get => id; set => id = value; }     
        
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public enum GameStatus
        {
            lobby,
            rounds,
            ended
        }
    }
}
