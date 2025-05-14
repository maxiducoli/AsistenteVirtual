using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ChatBot.Controllers;
using ChatBot.Models;
using ChatBot.Services;


namespace ChatBot.Views.WinForms
{
    public partial class frmChatForm : Form
    {
        Button btnEnviar;
        TextBox txtPregunta;
        private BindingList<clsHistorialModel> historialMensajes;
        // Esta lista guardará toda la conversación
        //private List<dynamic> historialMensajes = new List<dynamic>
        //{
        // new { role = "system", content = "Eres un asistente técnico especializado en soporte informático." }
        //};
        private readonly clsChatController _controller;

        public frmChatForm()
        {
            InitializeComponent();
            _controller = new clsChatController();
            // Inicializar el historial en memoria
            InicializarHistorial();
            // Inicializar la interfaz gráfica
            InicializarInterfaz();
        }
        private void InicializarHistorial()
        {
            historialMensajes = new BindingList<clsHistorialModel>
    {
        new clsHistorialModel
        {
            Rol = "system",
            Contenido = "Eres un asistente técnico especializado en soporte informático."
        }
    };
        }

        private void InicializarInterfaz()
        {
            // Formulario principal
            this.Size = new Size(450, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Chatbot Soporte Técnico";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // FlowLayoutPanel para mostrar las burbujas
            flpChat = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.Red,
                AutoSize = false,
                
            };


            // ❗ Evitar scroll horizontal
            flpChat.HorizontalScroll.Enabled = false;
            flpChat.HorizontalScroll.Visible = false;
            flpChat.VerticalScroll.Enabled = true;
            //flpChat.VerticalScroll.Visible = false;

            // TextBox para escribir preguntas
            txtPregunta = new TextBox
            {
                Dock = DockStyle.Left,
                Width = 350,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                Font = new Font("Segoe UI", 9.75f),
                Margin = new Padding(5),
                //PlaceholderText = "Escribe tu pregunta...",
            };

            // Botón Enviar
            btnEnviar = new Button
            {
                Text = "Enviar",
                Width = 60,
                Dock = DockStyle.Right,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                ForeColor = Color.White,
                BackColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Margin = new Padding(5)
            };
            btnEnviar.Click += btnEnviar_Click;

            // FlowLayoutPanel interno para entrada de texto
            var flowIngreso = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Height = 50,
                Width = 350,
                Margin = new Padding(0)
            };

            flowIngreso.Controls.Add(txtPregunta);
            flowIngreso.Controls.Add(btnEnviar);

            // Panel inferior para entrada
            var panelIngreso = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
            };
            panelIngreso.Controls.Add(flowIngreso);

            // Panel superior para el historial
            var panelEntrada = new Panel
            {
                Dock = DockStyle.Fill,
            };
            panelEntrada.Controls.Add(flpChat);

            // Agregar todo al formulario
            this.Controls.Add(panelEntrada);
            this.Controls.Add(panelIngreso);
            panelIngreso.BringToFront();
            panelEntrada.BringToFront();
        }

        //private async void btnEnviar_Click(object sender, EventArgs e)
        //{
        //    string pregunta = txtPregunta.Text.Trim();
        //    if (!string.IsNullOrEmpty(pregunta))
        //    {
        //        // Añadir pregunta del usuario
        //        var nuevoMensaje = new clsHistorialModel
        //        {
        //            Rol = "user",
        //            Contenido = pregunta
        //        };
        //        flpChat.Controls.Add(new clsChatBubble(nuevoMensaje));
        //        flpChat.Controls[flpChat.Controls.Count - 1].BringToFront();
        //        // Mostrar mensaje temporal del bot
        //        var mensajeTemporal = new clsHistorialModel
        //        {
        //            Rol = "assistant",
        //            Contenido = "Escribiendo..."
        //        };
        //        flpChat.Controls.Add(new clsChatBubble(mensajeTemporal));

        //        // Llamar al modelo IA
        //        string respuestaReal = await _controller.ProcesarPreguntaAsync(pregunta);

        //        // Eliminar burbuja temporal
        //        flpChat.Controls.RemoveAt(flpChat.Controls.Count - 1);

        //        // Añadir respuesta real del bot
        //        var mensajeReal = new clsHistorialModel
        //        {
        //            Rol = "assistant",
        //            Contenido = respuestaReal
        //        };
        //        flpChat.Controls.Add(new clsChatBubble(mensajeReal));
        //        flpChat.Controls[flpChat.Controls.Count - 1].BringToFront();
        //        // Forzar actualización visual
        //        flpChat.Invalidate(); // Marca el panel como sucio → fuerza repintado
        //        flpChat.Update();     // Refresca inmediatamente
        //        flpChat.ScrollControlIntoView(flpChat.Controls[flpChat.Controls.Count - 1]);

        //        txtPregunta.Clear();
        //    }
        //}


        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            string pregunta = txtPregunta.Text.Trim();
            if (!string.IsNullOrEmpty(pregunta))
            {
                // Añadir pregunta del usuario
                var nuevoMensaje = new clsHistorialModel
                {
                    Rol = "user",
                    Contenido = pregunta
                };
                flpChat.Controls.Add(new clsChatBubble(nuevoMensaje));

                // Mostrar "Escribiendo..."
                var mensajeTemporal = new clsHistorialModel
                {
                    Rol = "assistant",
                    Contenido = "Escribiendo..."
                };
                flpChat.Controls.Add(new clsChatBubble(mensajeTemporal));

                // Forzar actualización visual
                flpChat.Invalidate();
                flpChat.Update();
                flpChat.ScrollControlIntoView(flpChat.Controls[flpChat.Controls.Count - 1]);

                try
                {
                   
                    string respuestaReal = await _controller.ProcesarPreguntaAsync(pregunta,historialMensajes);

                    // Eliminar mensaje temporal
                    flpChat.Controls.RemoveAt(flpChat.Controls.Count - 1);

                    // Añadir respuesta real
                    var mensajeReal = new clsHistorialModel
                    {
                        Rol = "assistant",
                        Contenido = respuestaReal
                    };
                    flpChat.Controls.Add(new clsChatBubble(mensajeReal));
                    // Forzar actualización visual
                    flpChat.Invalidate();
                    flpChat.Update();
                    flpChat.ScrollControlIntoView(flpChat.Controls[flpChat.Controls.Count - 1]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener respuesta: " + ex.Message);
                }

                txtPregunta.Clear();
            }
        }
    }
}