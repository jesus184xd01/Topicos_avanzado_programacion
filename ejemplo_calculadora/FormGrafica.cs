using System;
using System.Drawing;
using System.Windows.Forms;
using ejemplo_calculadora;

namespace ejemplo_calculadora.UI
{
    /// <summary>
    /// Formulario que muestra la gráfica de funciones trigonométricas
    /// </summary>
    public partial class FormGrafica : Form
    {
        #region Campos Privados

        private double[] _dataX;
        private double[] _dataY;
        private string _functionName;
        private bool _isInverse;

        // Configuración visual
        private const int DEFAULT_WIDTH = 600;
        private const int DEFAULT_HEIGHT = 400;
        private const double SCALE_X = 25;
        private const double SCALE_Y = 50;
        private const int DATA_POINTS = 800;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor del formulario de gráfica
        /// </summary>
        /// <param name="functionName">Nombre de la función (sen, cos, tan)</param>
        /// <param name="isInverse">Indica si es función inversa</param>
        public FormGrafica(string functionName, bool isInverse)
        {
            InitializeComponent();

            _functionName = functionName;
            _isInverse = isInverse;

            ConfigureWindow();
            GenerateData();
        }

        #endregion

        #region Configuración

        private void ConfigureWindow()
        {
            this.Text = $"Gráfica de {_functionName}{(_isInverse ? "⁻¹" : "")}";
            this.Size = new Size(DEFAULT_WIDTH, DEFAULT_HEIGHT);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true; // Reduce parpadeo

            // Registrar evento de pintado
            this.Paint += FormGrafica_Paint;
        }

        #endregion

        #region Generación de Datos

        /// <summary>
        /// Genera los puntos de datos para la función
        /// </summary>
        private void GenerateData()
        {
            _dataX = new double[DATA_POINTS];
            _dataY = new double[DATA_POINTS];

            double step;
            double x;

            // Configurar rango según función
            if (!_isInverse)
            {
                step = 0.05;
                x = -10;
            }
            else
            {
                step = 0.01;
                x = (_functionName == "tan") ? -10 : -1;
            }

            // Calcular puntos
            for (int i = 0; i < DATA_POINTS; i++)
            {
                _dataX[i] = x;
                _dataY[i] = CalculateFunctionValue(x);
                x += step;
            }
        }

        /// <summary>
        /// Calcula el valor de la función en un punto
        /// </summary>
        private double CalculateFunctionValue(double x)
        {
            if (!_isInverse)
            {
                // Funciones normales
                switch (_functionName.ToLower())
                {
                    case "sen":
                    case "sin":
                        return Math.Sin(x);
                    case "cos":
                        return Math.Cos(x);
                    case "tan":
                        return Math.Tan(x);
                    default:
                        return 0;
                }
            }
            else
            {
                // Funciones inversas
                switch (_functionName.ToLower())
                {
                    case "sen":
                    case "sin":
                        return Math.Asin(x);
                    case "cos":
                        return Math.Acos(x);
                    case "tan":
                        return Math.Atan(x);
                    default:
                        return 0;
                }
            }
        }

        #endregion

        #region Renderizado

        /// <summary>
        /// Dibuja la gráfica en el formulario
        /// </summary>
        private void FormGrafica_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            // Configurar calidad de renderizado
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dibujar ejes y curva
            DrawAxes(g, width, height);
            DrawCurve(g, width, height);
            DrawLabels(g, width, height);
        }

        /// <summary>
        /// Dibuja los ejes coordenados
        /// </summary>
        private void DrawAxes(Graphics g, int width, int height)
        {
            using (Pen axisPen = new Pen(Color.Gray, 1))
            {
                // Eje X
                g.DrawLine(axisPen, 0, height / 2, width, height / 2);

                // Eje Y
                g.DrawLine(axisPen, width / 2, 0, width / 2, height);
            }
        }

        /// <summary>
        /// Dibuja la curva de la función
        /// </summary>
        private void DrawCurve(Graphics g, int width, int height)
        {
            using (Pen curvePen = new Pen(Color.Lime, 2))
            {
                for (int i = 0; i < _dataX.Length - 1; i++)
                {
                    // Validar valores
                    if (!IsValidPoint(_dataY[i]) || !IsValidPoint(_dataY[i + 1]))
                        continue;

                    // Calcular coordenadas de pantalla
                    float x1 = (float)(width / 2 + _dataX[i] * SCALE_X);
                    float y1 = (float)(height / 2 - _dataY[i] * SCALE_Y);

                    float x2 = (float)(width / 2 + _dataX[i + 1] * SCALE_X);
                    float y2 = (float)(height / 2 - _dataY[i + 1] * SCALE_Y);

                    // Validar coordenadas de pantalla
                    if (!IsValidScreenCoordinate(x1, y1) || !IsValidScreenCoordinate(x2, y2))
                        continue;

                    g.DrawLine(curvePen, x1, y1, x2, y2);
                }
            }
        }

        /// <summary>
        /// Dibuja etiquetas en la gráfica
        /// </summary>
        private void DrawLabels(Graphics g, int width, int height)
        {
            using (Font font = new Font("Arial", 10))
            using (Brush brush = new SolidBrush(Color.White))
            {
                // Etiqueta de la función
                string label = $"y = {_functionName}{(_isInverse ? "⁻¹" : "")}(x)";
                g.DrawString(label, font, brush, 10, 10);

                // Etiquetas de ejes
                g.DrawString("X", font, brush, width - 20, height / 2 + 5);
                g.DrawString("Y", font, brush, width / 2 + 5, 10);
            }
        }

        #endregion

        #region Métodos de Validación

        /// <summary>
        /// Valida si un punto es válido (no NaN ni Infinito)
        /// </summary>
        private bool IsValidPoint(double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value);
        }

        /// <summary>
        /// Valida si una coordenada de pantalla está dentro de límites razonables
        /// </summary>
        private bool IsValidScreenCoordinate(float x, float y)
        {
            const float MAX_COORDINATE = 5000;
            return Math.Abs(x) <= MAX_COORDINATE && Math.Abs(y) <= MAX_COORDINATE;
        }

        #endregion
    }
}