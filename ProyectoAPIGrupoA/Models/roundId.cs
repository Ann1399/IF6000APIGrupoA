

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ProyectoAPIGrupoA.Models
{
    public class roundId
    {

        private string roundid;
        private static Random random = new Random();
        
        public roundId()
        {
            this.roundid = idRound();
        }
        public roundId(string id)
        {
            this.roundid = id;
        }
        [SwaggerSchemaExample("7D76A7E6-2EF5-43D0-B54D-F6F3584E9E44")]
        public string Id { get => roundid; set => roundid = value; }

        public string idRound()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var a = new string(Enumerable.Repeat(characters, 40)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            while (Util.Utility.existRoundId(a) == true)
            {
                a = new string(Enumerable.Repeat(characters, 40)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return a;
        }

    }
}
