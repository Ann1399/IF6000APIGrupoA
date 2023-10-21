using System.ComponentModel;

namespace ProyectoAPIGrupoA.Models
{
    public class GameBase
    {
        private string name;
        private string owner;
        private string password;

        public GameBase(string name, string owner, string password)
        {
            this.name = name;
            this.owner = owner;
            this.password = password;
        }

        [DefaultValue("Epsilon Centauri")]
        public string Name { get => name; set => name = value; }

        [DefaultValue("Thanos")]
        public string Owner { get => owner; set => owner = value; }

        [DefaultValue("Shazam!")]
        public string Password { get => password; set => password = value; }
    }
}
