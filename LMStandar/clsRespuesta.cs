using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotModels
{
    public class Respuesta
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
