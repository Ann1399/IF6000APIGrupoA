
namespace ProyectoAPIGrupoA.Models
{
    public class gameId
    {
        private string id;
        private static Random random = new Random();
        public gameId()
        {
            this.id = idGames();
        }

        [SwaggerSchemaExample("WWMmTUllMLbJjKO2JlmdTonmJpYVUL9OMD83vue1")]
        public string Id { get => id; set => id = value; }

        public string idGames()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var a = new string (Enumerable.Repeat(characters, 40)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            while (Util.Utility.existGameId(a)==true)
            {
                a = new string(Enumerable.Repeat(characters, 40)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return a;
        }
    }
}
