using ExamenIIRedesAPI.Models;
using Microsoft.OpenApi.Any;

namespace ProyectoIIRedesAPI.Models
{
    public class gamePwd
    {
        private string password; //Revisar Schema 

        public gamePwd(string password)
        {
            this.password = password;
        }

        [SwaggerSchemaExample("Shazam!")]
        public string Password { get => password; set => password = value; }
    }
}
