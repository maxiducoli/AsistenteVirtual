using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotModels
{
    public class Historial
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
