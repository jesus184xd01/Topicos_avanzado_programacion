using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservación
{
    public partial class Log_in_form : Form
    {
        public Log_in_form()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Log_in_form_Load(object sender, EventArgs e)
        {
            setting_screen();
            centrar_panel();
            centrar_imagen();
        }

        private void setting_screen()
        {
            this.Size = new Size(360, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            login_btn.BackColor = Color.FromArgb(255, 56, 92);
            login_btn.ForeColor = Color.White;

            this.Text = "Log in Form";
        }

        private void centrar_panel()
        {
            panel_container.BackColor = Color.Transparent;
            panel_container.Left = (this.ClientSize.Width - panel_container.Width) / 2;
            panel_container.Top = (this.ClientSize.Height - panel_container.Height + 150) / 2;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(255, 90, 95),
                Color.FromArgb(255, 56, 92),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void centrar_imagen()
        {
            logo_img.SizeMode = PictureBoxSizeMode.Zoom;
            logo_img.BackColor = Color.Transparent;
            logo_img.Size = new Size(200, 200);
            logo_img.Left = (this.ClientSize.Width - logo_img.Width) / 2;
            logo_img.Top = 0;
        }

        private void panel_container_Paint(object sender, PaintEventArgs e)
        {

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            string nombre = user_txt.Text.Trim();
            string correo = email_txt.Text.Trim();
            string password = password_txt.Text.Trim();


            if (nombre == "" || correo == "" || password == "")
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }

            if (!CorreoValido(correo))
            {
                MessageBox.Show("Correo electrónico no válido");
                return;
            }



            MessageBox.Show("Datos correctos");

            var user = Validator.Validate(
                nombre,
                correo,
                password
                );
            if (user != null)
            {
                Reserva_form form = new Reserva_form();
                form.Show();
                this.Hide();
            }
            else
            {

                MessageBox.Show("No logeado");
            }
        }

        private bool CorreoValido(string correo)
        {
            try
            {
                MailAddress m = new MailAddress(correo);
                return m.Address == correo;
            }
            catch
            {
                return false;
            }
        }
    }
}
