using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ExamenIIRedesAPI.Models
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
        [SwaggerSchemaExample("Explain the error to the user here")]
        public string Message { get => message; set => message = value; }

        [Required]
        public int Code { get => code; set => code = value; }
    }
}
