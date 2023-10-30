using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ProyectoAPIGrupoA.Models
{
    public class round
    {
        [IgnoreDataMember]
        private roundId id;
        private gamePlayerName leader;
        private roundStatus status;
        private roundResult result;
        private roundPhase phase;
        private List<gamePlayerName> group;
        [IgnoreDataMember]
        private gameId gameId;
        private roundVote votes;
        [IgnoreDataMember]
        private List<string> alreadyVote;
        [IgnoreDataMember]
        private List<bool> actions;

        public round(gameId id)
        {
            this.id = new roundId();
            this.gameId = id;
            this.status = roundStatus.waiting_on_leader;
            this.result = roundResult.none;
            this.phase = roundPhase.vote1;
            this.group = new List<gamePlayerName>();
            this.votes = new roundVote();
            this.actions = new List<bool>();
            this.alreadyVote = new List<string>();
        }
        public roundStatus Status { get => status; set => status = value; }
        public roundPhase Phase { get => phase; set => phase = value; }
        public roundResult Result { get => result; set => result = value; }
        public gamePlayerName Leader { get => leader; set => leader = value; }
        public List<gamePlayerName> Group { get => group; set => group = value; }

        public roundVote Votes { get => votes; set => votes = value; }
        public roundId Id { get => id; set => id = value; }

        [IgnoreDataMember]
        public gameId GameId { get => gameId; set => gameId = value; }
        [IgnoreDataMember]
        public List<bool> Actions { get => actions; set => actions = value; }
        [IgnoreDataMember]
        public List<string> AlreadyVote { get => alreadyVote; set => alreadyVote = value; }
    }



    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum roundStatus
    {
        waiting_on_leader,
        voting,
        waiting_on_group,
        ended
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum roundResult
    {
        none,
        citizen,
        enemies
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum roundPhase
    {
        vote1,
        vote2,
        vote3
    }

    
}
