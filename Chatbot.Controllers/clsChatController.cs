using System;
using System.Threading.Tasks;
using ChatBot.Services;
using MySql.Data.MySqlClient;
using Chatbot.Models;
using System.ComponentModel;
using ChatBot.Models;
namespace ChatBot.Controllers
{
    public class clsChatController
    {
        private readonly clsIAService _iaService;
        private readonly clsDBService _dbService;

        public clsChatController()
        {
            _iaService = new clsIAService();
            _dbService = new clsDBService();
        }

        public async Task<string> ProcesarPreguntaAsync(string texto, BindingList<clsHistorialModel> historialMensajes)
        {
            try
            {
                return await _iaService.ObtenerRespuestaAsync(texto, historialMensajes);
            }
            catch (Exception ex)
            {
                _dbService.RegistrarError(ex.Message);
                return "Hubo un error al procesar tu pregunta.";
            }
        }

        // 🤖 Procesar pregunta → guardar en BDD
        public async Task<string> ProcesarPreguntaAsync(string texto, int usuarioId = 1)
        {
            try
            {
                string respuesta = await _iaService.ObtenerRespuestaAsync(texto);
                _dbService.InsertarHistorial(texto, respuesta, usuarioId: usuarioId);
                return respuesta;
            }
            catch (Exception ex)
            {
                _dbService.RegistrarError(ex.Message, usuarioId);
                return "Hubo un error al procesar tu pregunta.";
            }
        }

        // 🖍️ Actualizar una respuesta ya guardada
        public void ActualizarRespuesta(int id, string nuevaRespuesta)
        {
            _dbService.ActualizarRespuesta(id, nuevaRespuesta);
        }

        // ❌ Eliminar lógicamente una pregunta
        public void EliminarPregunta(int id)
        {
            _dbService.EliminarPregunta(id);
        }

        // 📄 Traer historial desde la BDD
        public MySqlDataReader ObtenerHistorial()
        {
            return _dbService.ObtenerHistorial();
        }

        // 🔍 Ver detalles de una pregunta específica
        public MySqlDataReader ObtenerRegistroPorId(int id)
        {
            return _dbService.ObtenerRegistroPorId(id);
        }

        // 📊 Mostrar logs (opcional)
        public MySqlDataReader ObtenerLogs()
        {
            return _dbService.ObtenerLogs();
        }
    }
}