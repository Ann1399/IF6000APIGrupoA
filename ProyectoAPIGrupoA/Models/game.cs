using ProyectoIIRedesAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ExamenIIRedesAPI.Models
{
    public class game
    {
        private gameId id;
        private gameName name;
        private bool password;
        private List<gamePlayerName> players;

        public game(gameId id, gameName name, bool password)
        {
            this.id = id;
            this.name = name;
            this.password = password;
        }

        public gameId Id { get => id; set => id = value; }
        public gameName Name { get => name; set => name = value; }
        public bool Password { get => password; set => password = value; }
    }
}
