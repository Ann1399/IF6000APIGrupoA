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
        private roundVote votes;


        public round()
        {

        }
        public roundId Id { get =>id; set => id = value; }
        public gamePlayerName Leader { get => leader; set => leader = value; }

        public roundStatus Status { get => status; set => status = value; }
        public roundResult Result { get => result; set => result = value; }
        public roundPhase Phase { get => phase; set => phase = value; }
        public List<gamePlayerName> Group { get => group; set => group = value; }

        public roundVote Votes { get => votes; set => votes = value; }

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
        enenmies
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum roundPhase
    {
        vote1,
        vote2,
        vote3
    }

    
}
