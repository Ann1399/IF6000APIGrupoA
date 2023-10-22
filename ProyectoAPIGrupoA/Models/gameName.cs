namespace ProyectoAPIGrupoA.Models
{
    public class gameName
    {
        private string name;

        public gameName(string name)
        {
            this.name = name;
        }

        [SwaggerSchemaExample("Epsilon Centauri")]
        public string Name { get => name; set => name = value; }
    }
}
