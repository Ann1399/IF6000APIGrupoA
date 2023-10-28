using Newtonsoft.Json;

namespace ProyectoAPIGrupoA.Models
{
    public class roundVote
    {
        //[JsonProperty("votes")]
        private List<bool> roundVotes;
        public roundVote()
        {
            this.roundVotes = new List<bool>();
        }
        public List<bool> RoundVotes { get => roundVotes; set => roundVotes = value; }
    }
}
