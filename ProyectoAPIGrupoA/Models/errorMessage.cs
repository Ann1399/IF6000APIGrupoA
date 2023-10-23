using System.ComponentModel.DataAnnotations;

namespace ProyectoAPIGrupoA.Models
{
    public class errorMessage
    {
        private string message;
        private int code;

        public errorMessage(string message, int code)
        {
            this.message = message;
            this.code = code;
        }

        [Required]
        public int Status { get => code; set => code = value; }

        [Required]
        [SwaggerSchemaExample("Explain the error to the user here")]
        public string Msg { get => message; set => message = value; }
 
    }
}
