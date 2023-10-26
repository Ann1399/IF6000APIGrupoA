using Newtonsoft.Json;

namespace ProyectoAPIGrupoA.Models
{
    public class roundVote
    {
        [JsonProperty("votes")]
        private List<bool> roundVotes;

        public List<bool> RoundVotes { get => roundVotes; set => roundVotes = value; }
    }
}
