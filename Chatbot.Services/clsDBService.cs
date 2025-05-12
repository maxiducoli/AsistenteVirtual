using MySql.Data.MySqlClient;
using System;
using Chatbot.Models;
namespace ChatBot.Services
{
    public class clsDBService
    {
        private string connectionString = "server=localhost;user=root;password=1111;database=CHATBOT;";

        // ✅ Guardar historial usando SP: INS_HISTORIAL_CHATBOT
        public void InsertarHistorial(
            string pregunta,
            string respuesta,
            string modelo = "qwen2-0_5b-instruct-fp16.gguf",
            int usuarioId = 1)
        {
            var conn = new MySqlConnection(connectionString);
            var cmd = new MySqlCommand("INS_HISTORIAL_CHATBOT", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_pregunta", pregunta);
            cmd.Parameters.AddWithValue("p_respuesta", respuesta);
            cmd.Parameters.AddWithValue("p_modelo", modelo);
            cmd.Parameters.AddWithValue("p_usuario_id", usuarioId);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                RegistrarError(ex.Message, usuarioId);
            }
        }

        // 🖍️ Actualizar respuesta: UPD_HISTORIAL_RESPUESTA
        public void ActualizarRespuesta(int id, string nuevaRespuesta)
        {
            var conn = new MySqlConnection(connectionString);
            var cmd = new MySqlCommand("UPD_HISTORIAL_RESPUESTA", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_id", id);
            cmd.Parameters.AddWithValue("p_nueva_respuesta", nuevaRespuesta);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                RegistrarError(ex.Message, p_pregunta_id: id);
            }
        }

        // ❌ Eliminado lógico: DEL_HISTORIAL_PREGUNTA
        public void EliminarPregunta(int id)
        {
            var conn = new MySqlConnection(connectionString);
            var cmd = new MySqlCommand("DEL_HISTORIAL_PREGUNTA", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_id", id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                RegistrarError(ex.Message, p_pregunta_id: id);
            }
        }

        // 📄 Obtener historial: GET_HISTORIAL_CHATBOT
        public MySqlDataReader ObtenerHistorial()
        {
            var conn = new MySqlConnection(connectionString);
            var cmd = new MySqlCommand("GET_HISTORIAL", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                RegistrarError(ex.Message);
                return null;
            }
        }

        // 🔍 Obtener registro por ID: GET_HISTORIAL_CHATBOT_X_ID
        public MySqlDataReader ObtenerRegistroPorId(int id)
        {
            var conn = new MySqlConnection(connectionString);
            var cmd = new MySqlCommand("GET_HISTORIAL_CHATBOT_ID", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id", id);

            try
            {
                conn.Open();
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                RegistrarError(ex.Message, p_pregunta_id: id);
                return null;
            }
        }

        // 📋 Registrar error: INS_LOG_ERROR
        public void RegistrarError(string mensaje, int usuarioId = 1, int? p_pregunta_id = null)
        {
            var conn = new MySqlConnection(connectionString);
            var cmd = new MySqlCommand("INS_LOG_ERROR", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_descripcion", mensaje);
            cmd.Parameters.AddWithValue("p_usuario_id", usuarioId);
            cmd.Parameters.AddWithValue("p_pregunta_id", p_pregunta_id ?? (object)DBNull.Value);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                // Evitar bucle infinito si falla el log
            }
        }

        // 📊 Obtener logs: GET_LOG_CHATBOT
        public MySqlDataReader ObtenerLogs()
        {
            var conn = new MySqlConnection(connectionString);
            var cmd = new MySqlCommand("GET_LOG_CHATBOT", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                RegistrarError(ex.Message);
                return null;
            }
        }
    }
}