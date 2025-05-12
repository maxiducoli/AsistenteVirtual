using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace ChatBot.Models
    {
        public class clsHistorialModel
        {
            public string Rol { get; set; }       // user / assistant / system
            public string Contenido { get; set; }  // El texto de la pregunta/respuesta
            public bool EsPregunta => Rol == "user";
            public bool EsRespuesta => Rol == "assistant";
            public bool EsSistema => Rol == "system";
        }
    }

