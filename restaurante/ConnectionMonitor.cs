using System;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace restaurante
{
    public static class ConnectionMonitor
    {
        private static System.Windows.Forms.Timer _timer;
        private static Form _formPrincipal;
        private static bool _mostrandoDialogo = false;

        public static void Iniciar(Form formPrincipal)
        {
            // ── Solo trackear si el ambiente es remoto ────────────
            string ambiente = ConfigurationManager.AppSettings["Ambiente"];
            if (ambiente != "remoto") return;

            _formPrincipal = formPrincipal;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 5000;
            _timer.Tick += OnTick;
            _timer.Start();
        }

        public static void Detener()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }

        private static void OnTick(object sender, EventArgs e)
        {
            if (_mostrandoDialogo) return;

            if (!HayConexion())
            {
                _mostrandoDialogo = true;
                _timer.Stop();

                var dlg = new FormSinConexion();
                var resultado = dlg.ShowDialog(_formPrincipal);

                if (resultado == DialogResult.Retry)
                {
                    _mostrandoDialogo = false;
                    _timer.Start();
                }
                else
                {
                    Detener();
                    Application.Exit();
                }
            }
        }

        private static bool HayConexion()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 2000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch { return false; }
        }
    }
}