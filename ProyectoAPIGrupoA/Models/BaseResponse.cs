using System.ComponentModel.DataAnnotations;

namespace ProyectoAPIGrupoA.Models
{
    public class BaseResponse
    {

        private string msg;
        private int status;

        public BaseResponse(string msg, int status)
        {
            this.msg = msg;
            this.status = status;
        }

        [Required]
        public string Msg { get => msg; set => msg = value; }

        [Required]
        public int Status { get => status; set => status = value; }
    }
}
