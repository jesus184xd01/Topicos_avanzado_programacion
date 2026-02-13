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
        private bool modoRadianes = true; // true = Radianes, false = Grados
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

            // Configurar estado inicial del botón RAD/DEG
            btn_rad.Text = "RAD";
            btn_rad.BackColor = Color.LightGreen;
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

                // DEBUG: Mostrar expresión original
                System.Diagnostics.Debug.WriteLine($"1. Expresión original: {expresion}");

                expresion = NormalizarExpresion(expresion);

                // DEBUG: Mostrar expresión normalizada
                System.Diagnostics.Debug.WriteLine($"2. Expresión normalizada: {expresion}");

                double resultado = EvaluarExpresion(expresion);

                // DEBUG: Mostrar resultado
                System.Diagnostics.Debug.WriteLine($"3. Resultado: {resultado}");

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

                // DEBUG: Mostrar error completo
                System.Diagnostics.Debug.WriteLine($"ERROR COMPLETO: {ex.ToString()}");

                MessageBox.Show($"Error al calcular: {ex.Message}\n\nExpresión: {txt_screen.Text}", "Error de Cálculo",
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
                // CORRECCIÓN: Usar TerminaEnNumero en lugar de char.IsDigit
                if (TerminaEnNumero(textoActual) || ultimo == ')' || ultimo == 'π')
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
                // CORRECCIÓN: Usar TerminaEnNumero en lugar de char.IsDigit
                if (TerminaEnNumero(textoActual) || ultimo == ')' || ultimo == 'π')
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
                // CORRECCIÓN: Usar TerminaEnNumero en lugar de char.IsDigit
                if (TerminaEnNumero(textoActual) || ultimo == ')' || ultimo == 'π')
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
            // Eliminar TODOS los espacios primero
            expresion = expresion.Replace(" ", "");

            // IMPORTANTE: Reemplazar 'x' minúscula por multiplicación ANTES de procesar decimales
            // Usar regex para reemplazar solo cuando x está entre números o después de un número
            expresion = Regex.Replace(expresion, @"(\d)\s*x\s*(\d)", "$1*$2", RegexOptions.IgnoreCase);

            // Reemplazar operadores visuales por operadores estándar
            expresion = expresion.Replace("×", "*")
                                 .Replace("÷", "/")
                                 .Replace("X", "*")  // X mayúscula también
                                 .Replace("π", Math.PI.ToString());

            // CORRECCIÓN PRINCIPAL: Normalizar decimales para evitar errores
            // Caso 1: .5 → 0.5 (agregar 0 antes del punto decimal)
            expresion = Regex.Replace(expresion, @"(?<![0-9])\.(?=[0-9])", "0.");

            // Caso 2: 5. → 5.0 (agregar 0 después del punto decimal si va seguido de operador)
            expresion = Regex.Replace(expresion, @"(\d)\.(?=[\+\-\*/\)\^])", "$1.0");

            // Caso 3: 5. al final de la expresión → 5.0
            expresion = Regex.Replace(expresion, @"(\d)\.$", "$1.0");

            return expresion;
        }

        private double EvaluarExpresion(string expresion)
        {
            expresion = expresion.Replace(" ", "");

            // Validar la expresión antes de procesarla
            if (!ValidarExpresion(expresion))
            {
                throw new Exception("Expresión inválida");
            }

            expresion = ProcesarRaices(expresion);
            expresion = ProcesarExponentes(expresion);

            // Normalizar para DataTable.Compute
            expresion = NormalizarParaDataTable(expresion);

            try
            {
                DataTable dt = new DataTable();
                var resultado = dt.Compute(expresion, "");
                return Convert.ToDouble(resultado);
            }
            catch (SyntaxErrorException ex)
            {
                throw new Exception("Error de sintaxis: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al evaluar: " + ex.Message);
            }
        }

        // NUEVO MÉTODO: Normalizar específicamente para DataTable
        private string NormalizarParaDataTable(string expresion)
        {
            // Manejar multiplicación por negativos: 5*-3 → 5*(-3)
            expresion = Regex.Replace(expresion, @"\*-(\d+\.?\d*)", "*(-$1)");

            // Manejar división por negativos: 10/-2 → 10/(-2)
            expresion = Regex.Replace(expresion, @"/-(\d+\.?\d*)", "/(-$1)");

            // Manejar suma de negativos: 5+-3 → 5+(-3)
            expresion = Regex.Replace(expresion, @"\+-(\d+\.?\d*)", "+(-$1)");

            return expresion;
        }

        // NUEVO MÉTODO: Validar expresión antes de evaluar
        private bool ValidarExpresion(string expresion)
        {
            // Verificar paréntesis balanceados
            int nivel = 0;
            foreach (char c in expresion)
            {
                if (c == '(') nivel++;
                if (c == ')') nivel--;
                if (nivel < 0) return false;
            }
            if (nivel != 0) return false;

            // Verificar que no haya múltiples puntos decimales en un número
            if (Regex.IsMatch(expresion, @"\d+\.\d*\.\d*"))
                return false;

            // Verificar que no termine con un operador (excepto paréntesis)
            if (expresion.Length > 0)
            {
                char ultimo = expresion[expresion.Length - 1];
                if (ultimo == '+' || ultimo == '-' || ultimo == '*' || ultimo == '/')
                    return false;
            }

            return true;
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

        // NUEVA FUNCIÓN: Determinar si el texto termina en un número (incluyendo decimales)
        private bool TerminaEnNumero(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;

            char ultimo = texto[texto.Length - 1];

            // Si termina en dígito, es un número
            if (char.IsDigit(ultimo)) return true;

            // Si termina en punto decimal, verificar si hay un número antes
            if (ultimo == '.' && texto.Length >= 2)
            {
                return char.IsDigit(texto[texto.Length - 2]);
            }

            return false;
        }

        private string ObtenerUltimoNumero()
        {
            string texto = txt_screen.Text;
            if (string.IsNullOrEmpty(texto)) return "";

            int ultimaPosicion = -1;

            // Buscar el último operador básico
            for (int i = texto.Length - 1; i >= 0; i--)
            {
                char c = texto[i];
                // Si encontramos un operador que no sea parte de un número
                if (EsOperadorBasico(c))
                {
                    // Verificar que no sea un signo negativo al inicio de un número
                    if (c == '-' && i > 0 && EsOperadorBasico(texto[i - 1]))
                    {
                        continue; // Es un número negativo, seguir buscando
                    }
                    ultimaPosicion = i;
                    break;
                }
            }

            if (ultimaPosicion >= 0)
                return texto.Substring(ultimaPosicion + 1).Trim();

            return texto.Trim();
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
            try
            {
                string ultimoNumero = ObtenerUltimoNumero();
                if (string.IsNullOrEmpty(ultimoNumero) || !double.TryParse(ultimoNumero, out double valor))
                {
                    txt_screen.Text = "Error";
                    return;
                }

                double resultado = 0;
                double valorEnRadianes = modoRadianes ? valor : valor * Math.PI / 180;

                if (modoInverso)
                {
                    // Funciones inversas
                    switch (funcion)
                    {
                        case "sen":
                            resultado = Math.Asin(valor);
                            if (!modoRadianes) resultado = resultado * 180 / Math.PI;
                            break;
                        case "cos":
                            resultado = Math.Acos(valor);
                            if (!modoRadianes) resultado = resultado * 180 / Math.PI;
                            break;
                        case "tan":
                            resultado = Math.Atan(valor);
                            if (!modoRadianes) resultado = resultado * 180 / Math.PI;
                            break;
                    }
                }
                else
                {
                    // Funciones normales
                    switch (funcion)
                    {
                        case "sen":
                            resultado = Math.Sin(valorEnRadianes);
                            break;
                        case "cos":
                            resultado = Math.Cos(valorEnRadianes);
                            break;
                        case "tan":
                            resultado = Math.Tan(valorEnRadianes);
                            break;
                    }
                }

                // Reemplazar el último número con el resultado
                string textoActual = txt_screen.Text;
                int posicion = textoActual.LastIndexOf(ultimoNumero);
                if (posicion >= 0)
                {
                    txt_screen.Text = textoActual.Substring(0, posicion) + FormatearResultado(resultado);
                }
                else
                {
                    txt_screen.Text = FormatearResultado(resultado);
                }

                nuevaEntrada = false;
            }
            catch (Exception ex)
            {
                txt_screen.Text = "Error";
                MessageBox.Show($"Error en función trigonométrica: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_rad_Click(object sender, EventArgs e)
        {
            // Alternar entre radianes y grados
            modoRadianes = !modoRadianes;

            if (modoRadianes)
            {
                btn_rad.Text = "RAD";
                btn_rad.BackColor = Color.LightGreen;
            }
            else
            {
                btn_rad.Text = "DEG";
                btn_rad.BackColor = Color.LightCoral;
            }
        }

    }
}