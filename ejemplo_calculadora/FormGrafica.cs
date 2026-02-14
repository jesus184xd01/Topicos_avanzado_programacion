using System;
using System.Drawing;
using System.Windows.Forms;

namespace ejemplo_calculadora.UI
{
    /// <summary>
    /// Formulario que muestra la gráfica de funciones trigonométricas
    /// CON el punto específico que el usuario calculó
    /// </summary>
    public partial class FormGrafica : Form
    {
        #region Campos Privados

        private double[] _dataX;
        private double[] _dataY;
        private string _functionName;
        private bool _isInverse;

        // NUEVO: Punto específico del usuario
        private double? _userInputX;
        private double? _userInputY;
        private bool _isRadianMode;

        // Configuración visual
        private const int DEFAULT_WIDTH = 700;
        private const int DEFAULT_HEIGHT = 500;
        private const double SCALE_X = 25;
        private const double SCALE_Y = 50;
        private const int DATA_POINTS = 800;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor básico sin punto específico
        /// </summary>
        public FormGrafica(string functionName, bool isInverse)
            : this(functionName, isInverse, null, null, true)
        {
        }

        /// <summary>
        /// Constructor completo con punto específico del usuario
        /// </summary>
        /// <param name="functionName">Nombre de la función (sen, cos, tan)</param>
        /// <param name="isInverse">Si es función inversa</param>
        /// <param name="userX">Valor X ingresado por el usuario</param>
        /// <param name="userY">Valor Y calculado para ese X</param>
        /// <param name="isRadianMode">Si está en modo radianes</param>
        public FormGrafica(string functionName, bool isInverse, double? userX, double? userY, bool isRadianMode)
        {
            InitializeComponent();

            _functionName = functionName;
            _isInverse = isInverse;
            _userInputX = userX;
            _userInputY = userY;
            _isRadianMode = isRadianMode;

            ConfigureWindow();
            GenerateData();
        }

        #endregion

        #region Configuración

        private void ConfigureWindow()
        {
            string modeText = _isRadianMode ? "rad" : "deg";
            string title = $"Gráfica de {_functionName}{(_isInverse ? "⁻¹" : "")}";

            if (_userInputX.HasValue)
            {
                title += $" - Punto: ({_userInputX.Value:F2}, {_userInputY.Value:F4})";
            }

            this.Text = title;
            this.Size = new Size(DEFAULT_WIDTH, DEFAULT_HEIGHT);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            this.Paint += FormGrafica_Paint;
            this.KeyDown += FormGrafica_KeyDown;
            this.KeyPreview = true; // Permitir teclas
        }

        #endregion

        #region Generación de Datos

        private void GenerateData()
        {
            _dataX = new double[DATA_POINTS];
            _dataY = new double[DATA_POINTS];

            double step;
            double x;

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

            for (int i = 0; i < DATA_POINTS; i++)
            {
                _dataX[i] = x;
                _dataY[i] = CalculateFunctionValue(x);
                x += step;
            }
        }

        private double CalculateFunctionValue(double x)
        {
            if (!_isInverse)
            {
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

        private void FormGrafica_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawAxes(g, width, height);
            DrawCurve(g, width, height);
            DrawLabels(g, width, height);

            // NUEVO: Dibujar punto del usuario
            if (_userInputX.HasValue && _userInputY.HasValue)
            {
                DrawUserPoint(g, width, height);
            }
        }

        private void DrawAxes(Graphics g, int width, int height)
        {
            using (Pen axisPen = new Pen(Color.Gray, 1))
            {
                g.DrawLine(axisPen, 0, height / 2, width, height / 2);
                g.DrawLine(axisPen, width / 2, 0, width / 2, height);
            }
        }

        private void DrawCurve(Graphics g, int width, int height)
        {
            using (Pen curvePen = new Pen(Color.Lime, 2))
            {
                for (int i = 0; i < _dataX.Length - 1; i++)
                {
                    if (!IsValidPoint(_dataY[i]) || !IsValidPoint(_dataY[i + 1]))
                        continue;

                    float x1 = (float)(width / 2 + _dataX[i] * SCALE_X);
                    float y1 = (float)(height / 2 - _dataY[i] * SCALE_Y);

                    float x2 = (float)(width / 2 + _dataX[i + 1] * SCALE_X);
                    float y2 = (float)(height / 2 - _dataY[i + 1] * SCALE_Y);

                    if (!IsValidScreenCoordinate(x1, y1) || !IsValidScreenCoordinate(x2, y2))
                        continue;

                    g.DrawLine(curvePen, x1, y1, x2, y2);
                }
            }
        }

        /// <summary>
        /// NUEVO: Dibuja el punto específico que el usuario calculó
        /// </summary>
        private void DrawUserPoint(Graphics g, int width, int height)
        {
            if (!_userInputX.HasValue || !_userInputY.HasValue)
                return;

            double userX = _userInputX.Value;
            double userY = _userInputY.Value;

            // Convertir a coordenadas de pantalla
            float screenX = (float)(width / 2 + userX * SCALE_X);
            float screenY = (float)(height / 2 - userY * SCALE_Y);

            // Validar que esté en pantalla
            if (screenX < 0 || screenX > width || screenY < 0 || screenY > height)
                return;

            // Dibujar punto grande
            using (Brush pointBrush = new SolidBrush(Color.Red))
            using (Pen pointBorder = new Pen(Color.White, 2))
            {
                float pointSize = 12;
                g.FillEllipse(pointBrush, screenX - pointSize / 2, screenY - pointSize / 2, pointSize, pointSize);
                g.DrawEllipse(pointBorder, screenX - pointSize / 2, screenY - pointSize / 2, pointSize, pointSize);
            }

            // Dibujar líneas punteadas al punto
            using (Pen dashedPen = new Pen(Color.Yellow, 1))
            {
                dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                // Línea vertical desde el eje X
                g.DrawLine(dashedPen, screenX, height / 2, screenX, screenY);

                // Línea horizontal desde el eje Y
                g.DrawLine(dashedPen, width / 2, screenY, screenX, screenY);
            }

            // Dibujar valores
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.Yellow))
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
            {
                string coordText = $"({userX:F2}, {userY:F4})";
                SizeF textSize = g.MeasureString(coordText, font);

                // Posicionar el texto
                float textX = screenX + 15;
                float textY = screenY - 15;

                // Ajustar si se sale de la pantalla
                if (textX + textSize.Width > width) textX = screenX - textSize.Width - 15;
                if (textY < 0) textY = screenY + 15;

                // Dibujar fondo del texto
                g.FillRectangle(bgBrush, textX - 3, textY - 3, textSize.Width + 6, textSize.Height + 6);

                // Dibujar texto
                g.DrawString(coordText, font, textBrush, textX, textY);
            }
        }

        private void DrawLabels(Graphics g, int width, int height)
        {
            using (Font font = new Font("Arial", 10))
            using (Brush brush = new SolidBrush(Color.White))
            {
                string label = $"y = {_functionName}{(_isInverse ? "⁻¹" : "")}(x)";
                g.DrawString(label, font, brush, 10, 10);

                g.DrawString("X", font, brush, width - 20, height / 2 + 5);
                g.DrawString("Y", font, brush, width / 2 + 5, 10);

                // NUEVO: Mostrar modo angular
                string modeLabel = $"Modo: {(_isRadianMode ? "Radianes" : "Grados")}";
                g.DrawString(modeLabel, font, brush, 10, height - 25);

                // NUEVO: Instrucciones
                if (_userInputX.HasValue)
                {
                    using (Font smallFont = new Font("Arial", 8))
                    {
                        g.DrawString("Presiona ESC para cerrar", smallFont, brush, width - 200, height - 25);
                    }
                }
            }
        }

        #endregion

        #region Métodos de Validación

        private bool IsValidPoint(double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value);
        }

        private bool IsValidScreenCoordinate(float x, float y)
        {
            const float MAX_COORDINATE = 5000;
            return Math.Abs(x) <= MAX_COORDINATE && Math.Abs(y) <= MAX_COORDINATE;
        }

        #endregion

        #region Eventos de Teclado

        private void FormGrafica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion
    }
}