using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace ejemplo_calculadora
{
    public partial class Form1 : Form
    {


        #region Variables Privadas
        private bool nuevaEntrada = true;
        private bool modoInverso = false;
        private const int ANCHO_VENTANA = 330;
        private const int ALTO_VENTANA = 520;
        private PrivateFontCollection pfc = new PrivateFontCollection();
        #endregion

        #region Constructor e Inicialización
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InicializarCalculadora();
            CargarFuenteDigital();
        }

        private void CargarFuenteDigital()
        {
            try
            {
                // Combina la ruta relativa al ejecutable
                string rutaFuente = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fonts", "digital-7.ttf");


                // Validar existencia
                if (!File.Exists(rutaFuente))
                {
                    MessageBox.Show("No se encontró la fuente digital-7.ttf", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cargar la fuente
                pfc.AddFontFile(rutaFuente);

                // Aplicar la fuente al txt_screen
                txt_screen.Font = new Font(pfc.Families[0], 36, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando fuente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InicializarCalculadora()
        {
            ConfigurarVentana();
            ConfigurarPanel();
            ConfigurarPantalla();
            ConfigurarIconosBotones();
        }
        #endregion

        #region Configuración de Interfaz
        private void ConfigurarVentana()
        {
            this.Size = new Size(ANCHO_VENTANA, ALTO_VENTANA);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Calculadora Científica";
        }

        private void ConfigurarPanel()
        {
            panel1.Left = (this.ClientRectangle.Width - panel1.Width) / 2;
            panel1.Top = 10;
        }

        private void ConfigurarPantalla()
        {
            txt_screen.ReadOnly = true;
            txt_screen.TextAlign = HorizontalAlignment.Right;
            txt_screen.Height = 60;
            txt_screen.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            txt_screen.Text = "0";
        }

        private void ConfigurarIconosBotones()
        {
            btn_raiz.Text = "√";
            btn_divid.Text = "÷";
        }
        #endregion

        #region Eventos de Números
        private void numero_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (txt_screen.Text == "Error" || txt_screen.Text == "∞")
            {
                LimpiarPantalla();
            }

            if (nuevaEntrada || txt_screen.Text == "0")
            {
                txt_screen.Text = btn.Text;
                nuevaEntrada = false;
            }
            else
            {
                // Evitar múltiples puntos decimales en el mismo número
                if (btn.Text == "." && ObtenerUltimoNumero().Contains(".")) return;
                txt_screen.Text += btn.Text;
            }
        }

        private void btn_point_Click(object sender, EventArgs e)
        {
            numero_Click(sender, e);
        }
        #endregion

        #region Eventos de Operaciones Básicas
        private void operacion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string operador = btn.Text;
            string textoActual = txt_screen.Text;

            if (string.IsNullOrEmpty(textoActual) || textoActual == "Error" || textoActual == "∞")
            {
                if (operador == "-")
                {
                    txt_screen.Text = "-";
                    nuevaEntrada = false;
                }
                return;
            }

            // Permitir número negativo al inicio
            if (textoActual == "0" && operador == "-")
            {
                txt_screen.Text = "-";
                nuevaEntrada = false;
                return;
            }

            // Si el último carácter es un operador
            if (textoActual.Length > 0 && EsOperadorBasico(textoActual[textoActual.Length - 1]))
            {
                if (operador == "-" && textoActual[textoActual.Length - 1] != '-')
                    txt_screen.Text += operador;
                else
                    txt_screen.Text = textoActual.Substring(0, textoActual.Length - 1) + operador;
            }
            else
            {
                txt_screen.Text += operador;
            }

            nuevaEntrada = false;
        }

        private void btn__equal_Click(object sender, EventArgs e)
        {
            try
            {
                string expresion = txt_screen.Text;
                if (string.IsNullOrEmpty(expresion) || expresion == "0") return;

                expresion = NormalizarExpresion(expresion);
                double resultado = EvaluarExpresion(expresion);

                if (double.IsNaN(resultado))
                    txt_screen.Text = "Error";
                else if (double.IsInfinity(resultado))
                    txt_screen.Text = "∞";
                else
                    txt_screen.Text = FormatearResultado(resultado);

                nuevaEntrada = true;
            }
            catch (Exception ex)
            {
                txt_screen.Text = "Error";
                nuevaEntrada = true;
                MessageBox.Show($"Error al calcular: {ex.Message}", "Error de Cálculo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Funciones Especiales
        private void btn_shift_Click(object sender, EventArgs e)
        {
            modoInverso = !modoInverso;
            if (modoInverso)
            {
                btn_sen.Text = "sin⁻¹";
                btn_cos.Text = "cos⁻¹";
                btn_tan.Text = "tan⁻¹";
                btn_shift.BackColor = Color.LightBlue;
            }
            else
            {
                btn_sen.Text = "SEN";
                btn_cos.Text = "COS";
                btn_tan.Text = "TAN";
                btn_shift.BackColor = SystemColors.Control;
            }
        }

        private void btn_ac_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btn_parentesis_abre_Click(object sender, EventArgs e)
        {
            string textoActual = txt_screen.Text;
            if (textoActual == "0" || textoActual == "Error" || textoActual == "∞")
                txt_screen.Text = "(";
            else
            {
                char ultimo = textoActual[textoActual.Length - 1];
                if (char.IsDigit(ultimo) || ultimo == ')' || ultimo == 'π')
                    txt_screen.Text += "×(";
                else
                    txt_screen.Text += "(";
            }
            nuevaEntrada = false;
        }

        private void btn_parentesis_cierra_Click(object sender, EventArgs e)
        {
            string textoActual = txt_screen.Text;
            int abiertos = 0, cerrados = 0;
            foreach (char c in textoActual)
            {
                if (c == '(') abiertos++;
                if (c == ')') cerrados++;
            }
            if (abiertos > cerrados) txt_screen.Text += ")";
            nuevaEntrada = false;
        }

        private void btn_raiz_Click_1(object sender, EventArgs e)
        {
            string textoActual = txt_screen.Text;
            if (textoActual == "0" || textoActual == "Error" || textoActual == "∞")
                txt_screen.Text = "√(";
            else
            {
                char ultimo = textoActual[textoActual.Length - 1];
                if (char.IsDigit(ultimo) || ultimo == ')' || ultimo == 'π')
                    txt_screen.Text += "×√(";
                else
                    txt_screen.Text += "√(";
            }
            nuevaEntrada = false;
        }

        private void btn_exp_Click(object sender, EventArgs e)
        {
            string textoActual = txt_screen.Text;
            if (textoActual == "0" || textoActual == "Error" || textoActual == "∞")
                txt_screen.Text = "1^(";
            else
                txt_screen.Text += "^(";
            nuevaEntrada = false;
        }

        private void btn_pi_Click(object sender, EventArgs e)
        {
            string textoActual = txt_screen.Text;
            if (textoActual == "0" || textoActual == "Error" || textoActual == "∞")
                txt_screen.Text = "π";
            else
            {
                char ultimo = textoActual[textoActual.Length - 1];
                if (char.IsDigit(ultimo) || ultimo == ')' || ultimo == 'π')
                    txt_screen.Text += "×π";
                else
                    txt_screen.Text += "π";
            }
            nuevaEntrada = false;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            string textoActual = txt_screen.Text;
            if (string.IsNullOrEmpty(textoActual) || textoActual == "0") return;

            int longitud = textoActual.Length;
            char ultimo = textoActual[longitud - 1];

            // Manejar π
            if (ultimo == 'π')
            {
                txt_screen.Text = textoActual.Substring(0, longitud - 1);
            }
            // Manejar exponentes con paréntesis "^("
            else if (ultimo == '(' && longitud >= 2 && textoActual[longitud - 2] == '^')
            {
                txt_screen.Text = textoActual.Substring(0, longitud - 2);
            }
            // Manejar raíces "√("
            else if (ultimo == '(' && longitud >= 2 && textoActual[longitud - 2] == '√')
            {
                txt_screen.Text = textoActual.Substring(0, longitud - 2);
            }
            else
            {
                txt_screen.Text = textoActual.Substring(0, longitud - 1);
            }

            if (string.IsNullOrEmpty(txt_screen.Text))
            {
                txt_screen.Text = "0";
                nuevaEntrada = true;
            }
        }
        #endregion

        #region Métodos de Evaluación
        private string NormalizarExpresion(string expresion)
        {
            expresion = expresion.Replace("×", "*")
                                 .Replace("÷", "/")
                                 .Replace("π", Math.PI.ToString());
            return expresion;
        }

        private double EvaluarExpresion(string expresion)
        {
            expresion = expresion.Replace(" ", "");
            expresion = ProcesarRaices(expresion);
            expresion = ProcesarExponentes(expresion);
            DataTable dt = new DataTable();
            var resultado = dt.Compute(expresion, "");
            return Convert.ToDouble(resultado);
        }

        private string ProcesarRaices(string expresion)
        {
            while (expresion.Contains("√"))
            {
                int idx = expresion.IndexOf("√");
                if (idx + 1 >= expresion.Length || expresion[idx + 1] != '(')
                    throw new Exception("Formato de raíz inválido. Use √(número)");

                int cierre = EncontrarCierreParentesis(expresion, idx + 2);
                string contenido = expresion.Substring(idx + 2, cierre - (idx + 2));
                double valor = EvaluarExpresion(contenido);
                double resultado = Math.Sqrt(valor);

                expresion = expresion.Substring(0, idx) + resultado.ToString() +
                            expresion.Substring(cierre + 1);
            }
            return expresion;
        }

        private string ProcesarExponentes(string expresion)
        {
            while (expresion.Contains("^"))
            {
                int idx = expresion.IndexOf("^");
                int baseStart = ObtenerInicioBase(expresion, idx);
                string baseStr = expresion.Substring(baseStart, idx - baseStart);

                string exponente;
                int expFin;
                if (idx + 1 < expresion.Length && expresion[idx + 1] == '(')
                {
                    int cierre = EncontrarCierreParentesis(expresion, idx + 2);
                    exponente = expresion.Substring(idx + 2, cierre - (idx + 2));
                    expFin = cierre + 1;
                }
                else
                {
                    int expStart = idx + 1;
                    expFin = expStart;
                    while (expFin < expresion.Length && (char.IsDigit(expresion[expFin]) || expresion[expFin] == '.')) expFin++;
                    exponente = expresion.Substring(expStart, expFin - expStart);
                }

                double baseVal = EvaluarExpresion(baseStr);
                double expVal = EvaluarExpresion(exponente);
                double resultado = Math.Pow(baseVal, expVal);

                expresion = expresion.Substring(0, baseStart) + resultado.ToString() + expresion.Substring(expFin);
            }
            return expresion;
        }

        private int ObtenerInicioBase(string expresion, int posicionExp)
        {
            int inicio = posicionExp - 1;
            if (inicio >= 0 && expresion[inicio] == ')')
            {
                int nivel = 1;
                inicio--;
                while (inicio >= 0 && nivel > 0)
                {
                    if (expresion[inicio] == ')') nivel++;
                    else if (expresion[inicio] == '(') nivel--;
                    inicio--;
                }
                inicio++;
            }
            else
            {
                while (inicio >= 0 && (char.IsDigit(expresion[inicio]) || expresion[inicio] == '.')) inicio--;
                inicio++;
            }
            return inicio;
        }

        private int EncontrarCierreParentesis(string expresion, int inicio)
        {
            int nivel = 1;
            int i = inicio;
            while (i < expresion.Length && nivel > 0)
            {
                if (expresion[i] == '(') nivel++;
                else if (expresion[i] == ')') nivel--;
                i++;
            }
            if (nivel != 0) throw new Exception("Paréntesis desbalanceados");
            return i - 1;
        }
        #endregion

        #region Métodos Auxiliares
        private void LimpiarPantalla()
        {
            txt_screen.Text = "0";
            nuevaEntrada = true;
        }

        private bool EsOperadorBasico(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '×' || c == '÷';
        }

        private string ObtenerUltimoNumero()
        {
            string texto = txt_screen.Text;
            int ultimaPosicion = -1;
            for (int i = texto.Length - 1; i >= 0; i--)
            {
                if (EsOperadorBasico(texto[i]) && i > 0)
                {
                    ultimaPosicion = i;
                    break;
                }
            }
            if (ultimaPosicion >= 0) return texto.Substring(ultimaPosicion + 1);
            return texto;
        }

        private string FormatearResultado(double valor)
        {
            if (Math.Abs(valor - Math.Floor(valor)) < 1e-10 && Math.Abs(valor) < 1e10)
                return ((long)valor).ToString();
            if (Math.Abs(valor) < 1e-6 || Math.Abs(valor) > 1e10)
                return valor.ToString("E6");
            return valor.ToString("G10");
        }
        #endregion

        #region Eventos de Panel
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        #endregion

        private void btn_off_Click(object sender, EventArgs e)
        {
            // Mostrar mensaje en la pantalla
            txt_screen.Text = "¡Hasta luego!";

            // Deshabilitar temporalmente los botones para que no se pueda seguir escribiendo
            foreach (Control ctl in panel1.Controls)
            {
                if (ctl is Button)
                    ctl.Enabled = false;
            }

            // Usar un temporizador para cerrar la calculadora después de 1 segundo
            Timer timer = new Timer();
            timer.Interval = 1000; // 1000 ms = 1 segundo
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                this.Close(); // Cierra la calculadora
            };
            timer.Start();
        }

        private void btn_sen_Click(object sender, EventArgs e)
        {
            EjecutarFuncionTrig("sen"); // aquí le decimos que se presionó seno
        }

        private void btn_cos_Click(object sender, EventArgs e)
        {
            EjecutarFuncionTrig("cos"); // aquí le decimos que se presionó coseno
        }

        private void btn_tan_Click(object sender, EventArgs e)
        {
            EjecutarFuncionTrig("tan"); // aquí le decimos que se presionó tangente
        }

        private void EjecutarFuncionTrig(string funcion)
        {
            if (modoInverso)
            {
                // Si está activado el inverso, se sabe que es sen⁻¹, cos⁻¹, tan⁻¹
                txt_screen.Text = $"Inv({funcion}) activada"; // placeholder
            }
            else
            {
                // Función normal: sen, cos, tan
                txt_screen.Text = $"{funcion} activada"; // placeholder
            }
        }

    }
}