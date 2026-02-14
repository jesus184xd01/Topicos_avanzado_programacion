using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;

namespace ejemplo_calculadora
{
    public partial class Form1 : Form
    {
        #region Campos Privados

        private CalculatorEngine _engine;
        private CalculatorState _state;
        private PrivateFontCollection _fontCollection;

        #endregion

        #region Constructor e Inicialización

        public Form1()
        {
            InitializeComponent();
            _engine = new CalculatorEngine();
            _state = new CalculatorState();
            _fontCollection = new PrivateFontCollection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            ConfigureWindow();
            ConfigurePanel();
            ConfigureDisplay();
            ConfigureButtonIcons();
            LoadDigitalFont();
            UpdateDisplay();
        }

        #endregion

        #region Configuración de Interfaz

        private void ConfigureWindow()
        {
            this.Size = new Size(CalculatorConstants.WINDOW_WIDTH, CalculatorConstants.WINDOW_HEIGHT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Calculadora Científica";
        }

        private void ConfigurePanel()
        {
            panel1.Left = (this.ClientRectangle.Width - panel1.Width) / 2;
            panel1.Top = 10;
        }

        private void ConfigureDisplay()
        {
            txt_screen.ReadOnly = true;
            txt_screen.TextAlign = HorizontalAlignment.Right;
            txt_screen.Height = 60;
            txt_screen.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            txt_screen.Text = "0";
        }

        private void ConfigureButtonIcons()
        {
            btn_raiz.Text = CalculatorConstants.Symbols.SquareRoot;
            btn_divid.Text = CalculatorConstants.Symbols.Divide;
            UpdateRadianModeButton();
        }

        private void LoadDigitalFont()
        {
            try
            {
                string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                              CalculatorConstants.FONT_PATH);

                if (!File.Exists(fontPath))
                {
                    MessageBox.Show("No se encontró la fuente digital-7.ttf", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _fontCollection.AddFontFile(fontPath);
                txt_screen.Font = new Font(_fontCollection.Families[0],
                                          CalculatorConstants.DISPLAY_FONT_SIZE,
                                          FontStyle.Regular);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando fuente: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Actualización de UI

        private void UpdateDisplay()
        {
            txt_screen.Text = _state.CurrentExpression;
        }

        private void UpdateRadianModeButton()
        {
            if (_state.IsRadianMode)
            {
                btn_rad.Text = CalculatorConstants.ButtonLabels.Radians;
                btn_rad.BackColor = Color.LightGreen;
            }
            else
            {
                btn_rad.Text = CalculatorConstants.ButtonLabels.Degrees;
                btn_rad.BackColor = Color.LightCoral;
            }
        }

        private void UpdateInverseModeButtons()
        {
            if (_state.IsInverseMode)
            {
                btn_sen.Text = CalculatorConstants.ButtonLabels.ArcSin;
                btn_cos.Text = CalculatorConstants.ButtonLabels.ArcCos;
                btn_tan.Text = CalculatorConstants.ButtonLabels.ArcTan;
                btn_shift.BackColor = Color.LightBlue;
            }
            else
            {
                btn_sen.Text = CalculatorConstants.ButtonLabels.Sin;
                btn_cos.Text = CalculatorConstants.ButtonLabels.Cos;
                btn_tan.Text = CalculatorConstants.ButtonLabels.Tan;
                btn_shift.BackColor = SystemColors.Control;
            }
        }

        #endregion

        #region Eventos de Números

        private void numero_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (_state.IsError())
            {
                _state.Reset();
            }

            if (_state.IsNewEntry || _state.CurrentExpression == "0")
            {
                _state.UpdateExpression(btn.Text);
                _state.IsNewEntry = false;
            }
            else
            {
                if (btn.Text == "." && !ExpressionHelper.CanAddDecimalPoint(_state.CurrentExpression))
                {
                    return;
                }
                _state.AppendToExpression(btn.Text);
            }

            UpdateDisplay();
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
            string operatorSymbol = btn.Text;
            string currentText = _state.CurrentExpression;

            if (string.IsNullOrEmpty(currentText) || _state.IsError())
            {
                if (operatorSymbol == "-")
                {
                    _state.UpdateExpression("-");
                    _state.IsNewEntry = false;
                }
                UpdateDisplay();
                return;
            }

            if (currentText == "0" && operatorSymbol == "-")
            {
                _state.UpdateExpression("-");
                _state.IsNewEntry = false;
                UpdateDisplay();
                return;
            }

            if (currentText.Length > 0 && ExpressionHelper.IsBasicOperator(currentText[currentText.Length - 1]))
            {
                if (operatorSymbol == "-" && currentText[currentText.Length - 1] != '-')
                    _state.AppendToExpression(operatorSymbol);
                else
                    _state.UpdateExpression(currentText.Substring(0, currentText.Length - 1) + operatorSymbol);
            }
            else
            {
                _state.AppendToExpression(operatorSymbol);
            }

            _state.IsNewEntry = false;
            UpdateDisplay();
        }

        private void btn__equal_Click(object sender, EventArgs e)
        {
            try
            {
                string expression = _state.CurrentExpression;
                if (string.IsNullOrEmpty(expression) || expression == "0") return;

                // Detectar si hay funciones trigonométricas para ofrecer ver gráfica
                string lastNumber = ExpressionHelper.GetLastNumber(expression);

                // Si es solo un número después de usar función trigonométrica
                // Mostrar menú de opciones
                if (!string.IsNullOrEmpty(lastNumber) && double.TryParse(lastNumber, out _))
                {
                    // Verificar si la expresión es simple (un número)
                    if (lastNumber == expression.Trim())
                    {
                        // Mostrar menú de opciones para graficar
                        ShowGraphMenu();
                        return;
                    }
                }

                // Cálculo normal
                CalculateExpression();
            }
            catch (Exception ex)
            {
                _state.UpdateExpression(CalculatorConstants.ErrorMessages.GenericError);
                _state.IsNewEntry = true;
                UpdateDisplay();

                MessageBox.Show($"Error al calcular: {ex.Message}\n\nExpresión: {_state.CurrentExpression}",
                              "Error de Cálculo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Cálculo y Evaluación

        /// <summary>
        /// Realiza el cálculo de la expresión actual
        /// </summary>
        private void CalculateExpression()
        {
            try
            {
                string expression = _state.CurrentExpression;
                expression = _engine.NormalizeExpression(expression);
                double result = _engine.Evaluate(expression);

                if (double.IsNaN(result))
                {
                    _state.UpdateExpression(CalculatorConstants.ErrorMessages.GenericError);
                }
                else if (double.IsInfinity(result))
                {
                    _state.UpdateExpression(CalculatorConstants.ErrorMessages.Infinity);
                }
                else
                {
                    _state.UpdateExpression(_engine.FormatResult(result));
                }

                _state.IsNewEntry = true;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                _state.UpdateExpression(CalculatorConstants.ErrorMessages.GenericError);
                _state.IsNewEntry = true;
                UpdateDisplay();
                throw;
            }
        }

        /// <summary>
        /// Muestra menú para seleccionar qué función graficar
        /// </summary>
        private void ShowGraphMenu()
        {
            // Crear el diálogo
            Form menuForm = new Form
            {
                Text = "Selecciona una función",
                Size = new Size(320, 240),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            // Etiqueta de título
            Label lblTitle = new Label
            {
                Text = "¿Qué función deseas graficar?",
                Location = new Point(20, 15),
                Size = new Size(280, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Botón SEN
            Button btnSen = new Button
            {
                Text = _state.IsInverseMode ? "📈 Arcsen(x)" : "📈 Sen(x)",
                Location = new Point(30, 55),
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(100, 180, 255),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSen.FlatAppearance.BorderSize = 0;
            btnSen.Click += (s, ev) =>
            {
                menuForm.Close();
                MostrarGrafica("sen");
            };

            // Botón COS
            Button btnCos = new Button
            {
                Text = _state.IsInverseMode ? "📈 Arccos(x)" : "📈 Cos(x)",
                Location = new Point(170, 55),
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(255, 150, 100),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnCos.FlatAppearance.BorderSize = 0;
            btnCos.Click += (s, ev) =>
            {
                menuForm.Close();
                MostrarGrafica("cos");
            };

            // Botón TAN
            Button btnTan = new Button
            {
                Text = _state.IsInverseMode ? "📈 Arctan(x)" : "📈 Tan(x)",
                Location = new Point(30, 110),
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(100, 200, 100),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnTan.FlatAppearance.BorderSize = 0;
            btnTan.Click += (s, ev) =>
            {
                menuForm.Close();
                MostrarGrafica("tan");
            };

            // Botón Cancelar
            Button btnCancel = new Button
            {
                Text = "❌ Cancelar",
                Location = new Point(170, 110),
                Size = new Size(120, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(220, 220, 220),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, ev) =>
            {
                menuForm.Close();
            };

            // Información adicional
            Label lblInfo = new Label
            {
                Text = $"Modo: {(_state.IsRadianMode ? "Radianes" : "Grados")}",
                Location = new Point(20, 165),
                Size = new Size(280, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Agregar controles
            menuForm.Controls.Add(lblTitle);
            menuForm.Controls.Add(btnSen);
            menuForm.Controls.Add(btnCos);
            menuForm.Controls.Add(btnTan);
            menuForm.Controls.Add(btnCancel);
            menuForm.Controls.Add(lblInfo);

            // Mostrar el diálogo
            menuForm.ShowDialog();
        }

        #endregion

        #region Funciones Especiales

        private void btn_shift_Click(object sender, EventArgs e)
        {
            _state.ToggleInverseMode();
            UpdateInverseModeButtons();
        }

        private void btn_ac_Click(object sender, EventArgs e)
        {
            _state.Reset();
            UpdateDisplay();
        }

        private void btn_parentesis_abre_Click(object sender, EventArgs e)
        {
            string currentText = _state.CurrentExpression;

            if (currentText == "0" || _state.IsError())
            {
                _state.UpdateExpression("(");
            }
            else
            {
                char lastChar = currentText[currentText.Length - 1];
                if (ExpressionHelper.EndsWithNumber(currentText) || lastChar == ')' || lastChar == 'π')
                    _state.AppendToExpression(CalculatorConstants.Symbols.Multiply + "(");
                else
                    _state.AppendToExpression("(");
            }

            _state.IsNewEntry = false;
            UpdateDisplay();
        }

        private void btn_parentesis_cierra_Click(object sender, EventArgs e)
        {
            string currentText = _state.CurrentExpression;
            ExpressionHelper.CountParentheses(currentText, out int open, out int close);

            if (open > close)
            {
                _state.AppendToExpression(")");
            }

            _state.IsNewEntry = false;
            UpdateDisplay();
        }

        private void btn_raiz_Click_1(object sender, EventArgs e)
        {
            string currentText = _state.CurrentExpression;

            if (currentText == "0" || _state.IsError())
            {
                _state.UpdateExpression(CalculatorConstants.Symbols.SquareRoot + "(");
            }
            else
            {
                if (ExpressionHelper.ShouldAddMultiplicationBeforeFunction(currentText))
                    _state.AppendToExpression(CalculatorConstants.Symbols.Multiply +
                                            CalculatorConstants.Symbols.SquareRoot + "(");
                else
                    _state.AppendToExpression(CalculatorConstants.Symbols.SquareRoot + "(");
            }

            _state.IsNewEntry = false;
            UpdateDisplay();
        }

        private void btn_exp_Click(object sender, EventArgs e)
        {
            string currentText = _state.CurrentExpression;

            if (currentText == "0" || _state.IsError())
                _state.UpdateExpression("1^(");
            else
                _state.AppendToExpression("^(");

            _state.IsNewEntry = false;
            UpdateDisplay();
        }

        private void btn_pi_Click(object sender, EventArgs e)
        {
            string currentText = _state.CurrentExpression;

            if (currentText == "0" || _state.IsError())
            {
                _state.UpdateExpression(CalculatorConstants.Symbols.Pi);
            }
            else
            {
                char lastChar = currentText[currentText.Length - 1];
                if (ExpressionHelper.EndsWithNumber(currentText) || lastChar == ')' || lastChar == 'π')
                    _state.AppendToExpression(CalculatorConstants.Symbols.Multiply +
                                            CalculatorConstants.Symbols.Pi);
                else
                    _state.AppendToExpression(CalculatorConstants.Symbols.Pi);
            }

            _state.IsNewEntry = false;
            UpdateDisplay();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (_state.CurrentExpression == "0" || string.IsNullOrEmpty(_state.CurrentExpression))
                return;

            _state.DeleteLastCharacter();
            UpdateDisplay();
        }

        private void btn_rad_Click(object sender, EventArgs e)
        {
            _state.ToggleRadianMode();
            UpdateRadianModeButton();
        }

        #endregion

        #region Funciones Trigonométricas

        private void btn_sen_Click(object sender, EventArgs e)
        {
            ExecuteTrigonometricFunction("sen");
        }

        private void btn_cos_Click(object sender, EventArgs e)
        {
            ExecuteTrigonometricFunction("cos");
        }

        private void btn_tan_Click(object sender, EventArgs e)
        {
            ExecuteTrigonometricFunction("tan");
        }

        private void ExecuteTrigonometricFunction(string function)
        {
            try
            {
                string lastNumber = ExpressionHelper.GetLastNumber(_state.CurrentExpression);

                if (string.IsNullOrEmpty(lastNumber) || !double.TryParse(lastNumber, out double value))
                {
                    _state.UpdateExpression(CalculatorConstants.ErrorMessages.GenericError);
                    UpdateDisplay();
                    return;
                }

                double result = _engine.CalculateTrigonometric(function, value,
                                                              _state.IsRadianMode,
                                                              _state.IsInverseMode);

                string currentText = _state.CurrentExpression;
                int position = currentText.LastIndexOf(lastNumber);

                if (position >= 0)
                {
                    _state.UpdateExpression(currentText.Substring(0, position) +
                                          _engine.FormatResult(result));
                }
                else
                {
                    _state.UpdateExpression(_engine.FormatResult(result));
                }

                _state.IsNewEntry = false;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                _state.UpdateExpression(CalculatorConstants.ErrorMessages.GenericError);
                UpdateDisplay();
                MessageBox.Show($"Error en función trigonométrica: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Eventos de Gráficas

        /// <summary>
        /// Muestra la gráfica de la función trigonométrica especificada
        /// </summary>
        /// <summary>
        /// Muestra la gráfica de la función trigonométrica CON el punto calculado del usuario
        /// </summary>
        private void MostrarGrafica(string functionName)
        {
            try
            {
                // Obtener el valor que el usuario ingresó
                string lastNumber = ExpressionHelper.GetLastNumber(_state.CurrentExpression);

                double? userX = null;
                double? userY = null;

                // Si hay un número válido en la expresión
                if (!string.IsNullOrEmpty(lastNumber) && double.TryParse(lastNumber, out double inputValue))
                {
                    userX = inputValue;

                    // Calcular el resultado de la función para ese valor
                    try
                    {
                        userY = _engine.CalculateTrigonometric(
                            functionName,
                            inputValue,
                            _state.IsRadianMode,
                            _state.IsInverseMode
                        );
                    }
                    catch
                    {
                        // Si falla el cálculo, solo mostrar la gráfica sin el punto
                        userX = null;
                        userY = null;
                    }
                }

                // Crear la gráfica con el punto del usuario
                UI.FormGrafica formGrafica = new UI.FormGrafica(
                    functionName,
                    _state.IsInverseMode,
                    userX,      // Valor X del usuario
                    userY,      // Valor Y calculado
                    _state.IsRadianMode
                );

                formGrafica.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar gráfica: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento para mostrar gráfica de seno (clic derecho)
        /// </summary>
        private void btn_sen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MostrarGrafica("sen");
            }
        }

        /// <summary>
        /// Evento para mostrar gráfica de coseno (clic derecho)
        /// </summary>
        private void btn_cos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MostrarGrafica("cos");
            }
        }

        /// <summary>
        /// Evento para mostrar gráfica de tangente (clic derecho)
        /// </summary>
        private void btn_tan_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MostrarGrafica("tan");
            }
        }

        /// <summary>
        /// Evento para mostrar menú de gráficas con mantener presionado el botón igual
        /// </summary>
        private void btn__equal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowGraphMenu();
            }
        }

        #endregion

        #region Otros Eventos

        private void btn_off_Click(object sender, EventArgs e)
        {
            txt_screen.Text = "¡Hasta luego!";

            foreach (Control control in panel1.Controls)
            {
                if (control is Button)
                    control.Enabled = false;
            }

            Timer timer = new Timer { Interval = 1000 };
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion
    }
}