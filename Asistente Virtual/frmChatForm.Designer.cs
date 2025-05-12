using System;
using System.Windows.Forms;

namespace ChatBot.Views.WinForms
{
    partial class frmChatForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpChat = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpChat
            // 
            this.flpChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpChat.Location = new System.Drawing.Point(0, 0);
            this.flpChat.Name = "flpChat";
            this.flpChat.Size = new System.Drawing.Size(484, 601);
            this.flpChat.TabIndex = 4;
            // 
            // frmChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 601);
            this.Controls.Add(this.flpChat);
            this.Name = "frmChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chatbot";
            this.ResumeLayout(false);

        }

        private void txtPregunta_KeyDown(object sender, KeyEventArgs e)
       {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnviar_Click(null, null);
            }
        }

        #endregion
        private FlowLayoutPanel flpChat;
    }
}

