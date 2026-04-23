using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurante
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string ambiente = ConfigurationManager.AppSettings["Ambiente"];

            if (ambiente == "remoto")
            {
                while (!HayInternet())
                {
                    var form = new FormSinConexion();
                    var resultado = form.ShowDialog();

                    if (resultado == DialogResult.Cancel)
                        return;
                }
            }

            Application.Run(new Form1());
        }

        private static bool HayInternet()
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