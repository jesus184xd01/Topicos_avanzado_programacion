using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservación
{
    public partial class Log_in_form : Form
    {
        private ProgressBar progressBar1; // Declarar como campo de la clase

        public Log_in_form()
        {
            InitializeComponent();

            // Crear el ProgressBar por código
            progressBar1 = new ProgressBar();
            progressBar1.Name = "progressBar1";
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            progressBar1.Height = 23;

            // Agregar al formulario
            this.Controls.Add(progressBar1);
        }

        // cambio del form de log in al de reservacion
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Log_in_form_Load(object sender, EventArgs e)
        {
            setting_screen();
            centrar_panel();
            centrar_imagen();
            ConfigurarProgressBar(); // Configurar posición del ProgressBar
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

        private void ConfigurarProgressBar()
        {
            // Posicionar el ProgressBar debajo del botón de login
            progressBar1.Width = login_btn.Width;
            progressBar1.Left = login_btn.Left;
            progressBar1.Top = login_btn.Bottom + 10;
        }

        private void panel_container_Paint(object sender, PaintEventArgs e)
        {
        }

        // Método async para no bloquear la interfaz
        private async void login_btn_Click(object sender, EventArgs e)
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

            if (!Validar_credenciales(nombre, correo, password))
            {
                return;
            }

            // Mostrar ProgressBar y deshabilitar botón
            progressBar1.Visible = true;
            login_btn.Enabled = false;
            login_btn.Text = "Enviando...";

            // Enviar correo de confirmación de forma asíncrona
            string asunto = "Confirmación de registro";
            string mensaje = $"Hola {nombre},\n\nTu registro ha sido exitoso.\n\nGracias por usar nuestro sistema de reservaciones.";

            await EnviarCorreoAsync(correo, asunto, mensaje);

            // Ocultar ProgressBar y rehabilitar botón
            progressBar1.Visible = false;
            login_btn.Enabled = true;
            login_btn.Text = "Iniciar sesión"; // O el texto original del botón
        }

        private bool CorreoValido(string correo)
        {
            try
            {
                MailAddress m = new MailAddress(correo);

                // Verificar que el dominio exista
                if (!DominioExiste(correo))
                {
                    MessageBox.Show("El dominio del correo electrónico no existe.\n\nVerifica que hayas escrito correctamente tu correo.",
                        "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return m.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        private bool Validar_credenciales(string user_name, string email, string password)
        {
            var user = Validator.Validate(
                user_name,
                email,
                password
                );
            if (user != null)
            {
                Reserva_form form = new Reserva_form(user);
                form.Show();
                this.Hide();
                return true;
            }
            else
            {

                MessageBox.Show("No logeado");
                return false;
            }
        }

        // Verifica si el dominio del correo existe
        private bool DominioExiste(string correo)
        {
            try
            {
                string dominio = correo.Split('@')[1];
                IPHostEntry host = Dns.GetHostEntry(dominio);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Verifica si hay conexión a internet
        private bool TieneConexionInternet()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Versión asíncrona del método EnviarCorreo
        private async Task EnviarCorreoAsync(string correoDestino, string asunto, string mensaje)
        {
            await Task.Run(() =>
            {
                // Validar conexión a internet antes de intentar enviar
                if (!TieneConexionInternet())
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("No hay conexión a internet. No se pudo enviar el correo de confirmación.",
                            "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    });
                    return;
                }

                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("ramirezramirezhaziel@gmail.com"); // correo de la app
                    mail.To.Add(correoDestino); // correo ingresado por el usuario
                    mail.Subject = asunto;
                    mail.Body = mensaje;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential(
                        "ramirezramirezhaziel@gmail.com",
                        "zdps qaqp pipc cbhe" // Contraseña de aplicación
                    );
                    smtp.EnableSsl = true; // Gmail requiere SSL
                    smtp.Send(mail); // Enviar correo

                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("Correo enviado a " + correoDestino +
                            "\n\nSi no lo recibes, verifica tu bandeja de spam.",
                            "Correo enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("Error al enviar correo: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            });
        }
    }
}