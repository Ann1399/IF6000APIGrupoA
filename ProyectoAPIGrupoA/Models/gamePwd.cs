using ProyectoAPIGrupoA.Models;
using Microsoft.OpenApi.Any;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(20, MinimumLength = 3)]
        public string Password { get => password; set => password = value; }
    }
}
