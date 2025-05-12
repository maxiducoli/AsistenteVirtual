using System.Drawing;
using System.Windows.Forms;
using ChatBot.Models;

namespace ChatBot.Views.WinForms
{
    public partial class clsChatBubble : UserControl
    {
        private Label lblMensaje;

        public clsChatBubble(clsHistorialModel mensaje)
        {
            InitializeComponent();
            InicializarControles(mensaje);
        }

        private void InitializeComponent()
        {
            this.lblMensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMensaje
            // 
            this.lblMensaje.BorderStyle = BorderStyle.FixedSingle;
            this.lblMensaje.Font = new Font("Segoe UI", 10F);
            this.lblMensaje.Location = new Point(5, 5);
            this.lblMensaje.Margin = new Padding(10);
            this.lblMensaje.MaximumSize = new Size(350, 0);
            this.lblMensaje.MinimumSize = new Size(50, 30);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Padding = new Padding(5);
            this.lblMensaje.Size = new Size(100, 30);
            this.lblMensaje.TabIndex = 0;
            // 
            // clsChatBubble
            // 
            this.Controls.Add(this.lblMensaje);
            this.Name = "clsChatBubble";
            //this.Dock = DockStyle.Fill;
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            //this.Size = new Size(450, 50);
            //this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Width = 427;
            this.ResumeLayout(false);

        }

        private void InicializarControles(clsHistorialModel mensaje)
        {
            lblMensaje.Text = mensaje.Contenido;

            if (mensaje.EsPregunta)
            {
                // Mensaje del usuario
                
                lblMensaje.BackColor = Color.LightBlue;
                lblMensaje.Dock = DockStyle.Left;
                lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
                lblMensaje.BringToFront();
            }
            else
            {
                // Mensaje del bot
                lblMensaje.BackColor = Color.LightGreen;
                lblMensaje.Dock = DockStyle.Right;
                lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            }
            lblMensaje.AutoSize = true;
            // Forzar salto de línea +altura dinámica
            int alturaEstimada = TextRenderer.MeasureText(
                mensaje.Contenido,
                lblMensaje.Font,
                new Size(400, 0),
                TextFormatFlags.WordBreak |
                TextFormatFlags.TextBoxControl)
                .Height + 50;
            this.Height = alturaEstimada;

            //lblMensaje.BringToFront();
        }
    }
}