using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Models
{
    public class clsLogModel
    {
        public class LogModel
        {
            public int IdLog { get; set; }
            public string Descripcion { get; set; }
            public string Fecha { get; set; }
            public string Hora { get; set; }
            public string Tipo { get; set; } = "ERROR";
            public int? UsuarioId { get; set; }
            public int? PreguntaId { get; set; }
        }
    }
}
