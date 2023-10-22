﻿using System.ComponentModel.DataAnnotations;

namespace ProyectoAPIGrupoA.Models
{
    public class BaseResponse
    {

        private string msg;
        private int status;
        private Object data;
        private Object others;

        public BaseResponse(string msg, int status, object? data)
        {
            this.msg = msg;
            this.status = status;
            this.data = data;
        }

        public BaseResponse(string msg, int status)
        {
            this.msg = msg;
            this.status = status;
            object[] arregloDeObjetosVacio = new object[0];
            this.data = arregloDeObjetosVacio;
        }
        [Required]
        public string Msg { get => msg; set => msg = value; }

        [Required]
        public int Status { get => status; set => status = value; }
        public Object Data { get => data; set => data = value; }
        public Object Others {  get => others; set => others = value; }
    }
}
