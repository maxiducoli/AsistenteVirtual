using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO.Pipes;

namespace LMStudioClient
{
    // Esta clase se encarga de interactuar con el modelo de lenguaje
    // y enviar preguntas junto con el historial de mensajes.
    // Se puede utilizar para crear un asistente virtual o chatbot.
    public class LMStandar
    {
        public async Task<string> EnviarPreguntaConHistorialAsync(
        string nuevaPregunta,
        List<object> historialMensajes)
        {
            var mensajes = new List<object>();

            // Añadir instrucción inicial (solo una vez)
            mensajes.Add(new { role = "system", content = "Eres un asistente técnico especializado en soporte informático." });

            // Añadir historial previo (si existe)
            if (historialMensajes != null && historialMensajes.Count > 0)
            {
                foreach (var mensaje in historialMensajes)
                {
                    mensajes.Add(mensaje);
                }
            }

            // Añadir nueva pregunta del usuario
            mensajes.Add(new { role = "user", content = nuevaPregunta });

            // Preparar payload
            var payload = new
            {
                model = "qwen2-0_5b-instruct-fp16.gguf",
                messages = mensajes.ToArray(),
                temperature = 0.7,
                max_tokens = 300
            };

            var client = new HttpClient();
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:1234/v1/chat/completions", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    if (doc.RootElement.TryGetProperty("choices", out JsonElement choicesArray) &&
                        choicesArray.GetArrayLength() > 0)
                    {
                        JsonElement firstChoice = choicesArray[0];
                        if (firstChoice.TryGetProperty("message", out JsonElement message) &&
                            message.TryGetProperty("content", out JsonElement contentElement))
                        {
                            return contentElement.GetString();
                        }
                    }
                }

                return "Error al obtener la respuesta del modelo.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        
    }
}