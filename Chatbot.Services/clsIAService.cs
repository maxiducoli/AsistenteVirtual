using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using ChatBot.Models;
namespace ChatBot.Services
{
    public class clsIAService
    {
        private readonly HttpClient _client;

        public clsIAService()
        {
            _client = new HttpClient();
        }

        public async Task<string> EnviarPreguntaConHistorialAsync(string pregunta, List<dynamic> historial)
        {
            var payload = new
            {
                model = "qwen2-0_5b-instruct-fp16.gguf",
                messages = historial,
                temperature = 0.7,
                max_tokens = 200
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:1234/v1/chat/completions", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            dynamic resultado = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

            if (resultado != null && resultado.choices != null && resultado.choices.Count > 0)
            {
                return resultado.choices[0].message.content;
            }

            return "No pude entender tu pregunta.";
        }

        public async Task<string> ObtenerRespuestaAsync(string pregunta)
        {
            var payload = new Dictionary<string, object>
            {
                { "model", "qwen2-0.5b-instruct" },
                { "messages", new object[]
                    {
                        new { role = "system", content = "Eres un asistente técnico especializado en soporte informático." },
                        new { role = "user", content = pregunta }
                    }
                },
                { "temperature", 0.7 },
                { "max_tokens", 200 },
                { "stream", false }
            };

            // Serializa con Newtonsoft.Json → compatible con .NET Framework
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("http://localhost:1234/v1/chat/completions", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserializamos la respuesta
                dynamic resultado = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                if (resultado != null && resultado.choices != null && resultado.choices.Count > 0)
                {
                    return resultado.choices[0].message.content;
                }

                return "No pude entender tu pregunta.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con el modelo IA: " + ex.Message);
            }
        }

        public async Task<string> ObtenerRespuestaAsync(string pregunta, BindingList<clsHistorialModel> historialMensajes)
        {
            // Convertir BindingList a formato válido para JSON
            var mensajesParaEnviar = new List<object>();
            foreach (var msg in historialMensajes)
            {
                mensajesParaEnviar.Add(new
                {
                    role = msg.Rol,
                    content = msg.Contenido
                });
            }

            var payload = new
            {
                model = "qwen2-0_5b-instruct-fp16.gguf",
                messages = mensajesParaEnviar.ToArray(),
                temperature = 0.7,
                max_tokens = 200
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:1234/v1/chat/completions", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            dynamic resultado = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

            if (resultado != null && resultado.choices != null && resultado.choices.Count > 0)
            {
                return resultado.choices[0].message.content;
            }

            return "No pude entender tu pregunta.";
        }
    }
}