using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Libreria_Vectores;

namespace ejemplo_calculadora
{
    public partial class Form_vectores : Form
    {
        // ── Controles ────────────────────────────────────────────────────────
        private TextBox txtX1, txtY1, txtX2, txtY2, txtEscalar;
        private Label lblResultado;

        public Form_vectores()
        {
            InicializarComponentes();
        }

        // ════════════════════════════════════════════════════════════════════
        //  CONSTRUCCIÓN DE LA INTERFAZ
        // ════════════════════════════════════════════════════════════════════
        private void InicializarComponentes()
        {
            this.Text = "Calculadora Vectorial 2D";
            this.Size = new Size(720, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Font fuenteLabel = new Font("Segoe UI", 10);
            Font fuenteTitulo = new Font("Segoe UI", 16, FontStyle.Bold);

            // ── Título ───────────────────────────────────────────────────────
            Label titulo = new Label()
            {
                Text = "Calculadora de Vectores 2D",
                Font = fuenteTitulo,
                Location = new Point(165, 18),
                AutoSize = true
            };

            // ── Grupo Vector 1 ───────────────────────────────────────────────
            GroupBox grupoV1 = CrearGrupo("Vector 1", 40, 70, 280, 155);

            Label lblX1 = CrearEtiqueta("X:", 20, 30, fuenteLabel);
            Label lblY1 = CrearEtiqueta("Y:", 155, 30, fuenteLabel);
            txtX1 = CrearTextBox(38, 28, 100);
            txtY1 = CrearTextBox(173, 28, 100);

            Button btnPolarV1 = CrearBoton("Forma Polar V1", 15, 70, BtnPolarV1_Click);
            Button btnVerV1 = CrearBoton("Ver en plano", 15, 108, (s, e) =>
            {
                if (ValidarVector(txtX1, txtY1, out Vector2D v))
                    AbrirGrafica(new[] { v }, new[] { "Vector 1" }, "Vector 1");
            });

            grupoV1.Controls.AddRange(new Control[] { lblX1, lblY1, txtX1, txtY1, btnPolarV1, btnVerV1 });

            // ── Grupo Vector 2 ───────────────────────────────────────────────
            GroupBox grupoV2 = CrearGrupo("Vector 2", 390, 70, 280, 155);

            Label lblX2 = CrearEtiqueta("X:", 20, 30, fuenteLabel);
            Label lblY2 = CrearEtiqueta("Y:", 155, 30, fuenteLabel);
            txtX2 = CrearTextBox(38, 28, 100);
            txtY2 = CrearTextBox(173, 28, 100);

            Button btnPolarV2 = CrearBoton("Forma Polar V2", 15, 70, BtnPolarV2_Click);
            Button btnVerV2 = CrearBoton("Ver en plano", 15, 108, (s, e) =>
            {
                if (ValidarVector(txtX2, txtY2, out Vector2D v))
                    AbrirGrafica(new[] { v }, new[] { "Vector 2" }, "Vector 2");
            });

            grupoV2.Controls.AddRange(new Control[] { lblX2, lblY2, txtX2, txtY2, btnPolarV2, btnVerV2 });

            // ── Grupo Operaciones ────────────────────────────────────────────
            GroupBox grupoOp = CrearGrupo("Operaciones", 40, 250, 630, 330);

            Label lblEsc = CrearEtiqueta("Escalar:", 15, 32, fuenteLabel);
            txtEscalar = CrearTextBox(90, 30, 80);

            // Cabeceras de columna
            Label lblColV1 = CrearEtiqueta("── Vector 1 ──", 185, 8, new Font("Segoe UI", 8, FontStyle.Bold));
            Label lblColV2 = CrearEtiqueta("── Vector 2 ──", 340, 8, new Font("Segoe UI", 8, FontStyle.Bold));
            Label lblCol12 = CrearEtiqueta("── V1 y V2 ──", 495, 8, new Font("Segoe UI", 8, FontStyle.Bold));

            // Fila 1 — Escalar / Escalar / Sumar
            Button btnEscV1 = CrearBoton("× Escalar (V1)", 185, 25, BtnEscalarV1_Click);
            Button btnEscV2 = CrearBoton("× Escalar (V2)", 340, 25, BtnEscalarV2_Click);
            Button btnSumar = CrearBoton("Sumar V1+V2", 495, 25, BtnSumar_Click, Color.FromArgb(39, 174, 96));

            // Fila 2 — Normalizar / Normalizar / Restar
            Button btnNormV1 = CrearBoton("Normalizar V1", 185, 65, BtnNormalV1_Click);
            Button btnNormV2 = CrearBoton("Normalizar V2", 340, 65, BtnNormalV2_Click);
            Button btnRestar = CrearBoton("Restar V1−V2", 495, 65, BtnRestar_Click, Color.FromArgb(39, 174, 96));

            // Fila 3 — Magnitud / Magnitud / Producto Punto
            Button btnMagV1 = CrearBoton("Magnitud V1", 185, 105, (s, e) =>
            {
                if (ValidarVector(txtX1, txtY1, out Vector2D v))
                    MostrarResultado(string.Format("Magnitud V1: {0:F4}", v.Magnitud()));
            });
            Button btnMagV2 = CrearBoton("Magnitud V2", 340, 105, (s, e) =>
            {
                if (ValidarVector(txtX2, txtY2, out Vector2D v))
                    MostrarResultado(string.Format("Magnitud V2: {0:F4}", v.Magnitud()));
            });
            Button btnPunto = CrearBoton("Producto Punto", 495, 105, BtnPunto_Click);

            // Fila 4 — Prod. Cruzado / Ángulo / Paralelos
            Button btnCruz = CrearBoton("Prod. Cruzado", 185, 145, BtnCruzado_Click);
            Button btnAngulo = CrearBoton("Ángulo entre", 340, 145, BtnAngulo_Click);
            Button btnParal = CrearBoton("¿Paralelos?", 495, 145, BtnParalelos_Click);

            // Fila 5 — Ver ambos
            Button btnVerAmbos = CrearBoton("Ver ambos vectores", 340, 190, BtnVerAmbos_Click,
                                            Color.FromArgb(142, 68, 173));
            btnVerAmbos.Width = 145;

            grupoOp.Controls.AddRange(new Control[]
            {
                lblEsc, txtEscalar,
                lblColV1, lblColV2, lblCol12,
                btnEscV1,  btnEscV2,  btnSumar,
                btnNormV1, btnNormV2, btnRestar,
                btnMagV1,  btnMagV2,  btnPunto,
                btnCruz,   btnAngulo, btnParal,
                btnVerAmbos
            });

            // ── Resultado ────────────────────────────────────────────────────
            lblResultado = new Label()
            {
                Text = "Resultado: —",
                Location = new Point(40, 595),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            // ── Botones inferiores ───────────────────────────────────────────
            Button btnLimpiar = CrearBoton("Limpiar campos", 40, 625, BtnLimpiar_Click,
                                           Color.FromArgb(231, 76, 60));
            Button btnMenu = CrearBoton("← Menú Principal", 530, 625, (s, e) =>
            {
                Form2 menuForm = new Form2();
                menuForm.Show();
                this.Hide();
            }, Color.FromArgb(100, 100, 100));

            // ── Validación en tiempo real ────────────────────────────────────
            foreach (TextBox tb in new[] { txtX1, txtY1, txtX2, txtY2, txtEscalar })
                tb.KeyPress += SoloNumerosKeyPress;

            // ── Agregar al formulario ────────────────────────────────────────
            this.Controls.AddRange(new Control[]
            {
                titulo, grupoV1, grupoV2, grupoOp,
                lblResultado, btnLimpiar, btnMenu
            });
        }

        // ════════════════════════════════════════════════════════════════════
        //  HELPERS DE CREACIÓN DE CONTROLES
        // ════════════════════════════════════════════════════════════════════

        private GroupBox CrearGrupo(string texto, int x, int y, int w, int h)
            => new GroupBox()
            {
                Text = texto,
                Font = new Font("Segoe UI", 10),
                Location = new Point(x, y),
                Size = new Size(w, h)
            };

        private Label CrearEtiqueta(string texto, int x, int y, Font fuente)
            => new Label() { Text = texto, Location = new Point(x, y), AutoSize = true, Font = fuente };

        private TextBox CrearTextBox(int x, int y, int ancho)
            => new TextBox() { Location = new Point(x, y), Width = ancho };

        private Button CrearBoton(string texto, int x, int y, EventHandler evento, Color? color = null)
        {
            Button btn = new Button()
            {
                Text = texto,
                Location = new Point(x, y),
                Width = 145,
                Height = 32,
                BackColor = color ?? Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += evento;
            return btn;
        }

        // ════════════════════════════════════════════════════════════════════
        //  VALIDACIÓN
        // ════════════════════════════════════════════════════════════════════

        /// <summary>Permite solo caracteres numéricos, signo negativo y separadores decimales.</summary>
        private void SoloNumerosKeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir: dígitos, retroceso, signo negativo, punto y coma decimal
            bool esDigito = char.IsDigit(e.KeyChar);
            bool esRetroceso = e.KeyChar == (char)Keys.Back;
            bool esPunto = (e.KeyChar == '.' || e.KeyChar == ',');
            bool esNegativo = e.KeyChar == '-';

            if (!esDigito && !esRetroceso && !esPunto && !esNegativo)
            {
                e.Handled = true; // bloquear el carácter
            }
        }

        private bool ValidarVector(TextBox txtX, TextBox txtY, out Vector2D vector)
        {
            vector = null;

            if (string.IsNullOrWhiteSpace(txtX.Text) || string.IsNullOrWhiteSpace(txtY.Text))
            {
                MessageBox.Show("Debe completar ambos valores del vector.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!double.TryParse(txtX.Text.Replace(',', '.'),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out double x) ||
                !double.TryParse(txtY.Text.Replace(',', '.'),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out double y))
            {
                MessageBox.Show("Los valores deben ser numéricos (ej. 3.5 o −2).",
                    "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            vector = new Vector2D(x, y);
            return true;
        }

        private bool ValidarEscalar(out double escalar)
        {
            escalar = 0;
            if (!double.TryParse(txtEscalar.Text.Replace(',', '.'),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out escalar))
            {
                MessageBox.Show("El escalar debe ser un número válido.",
                    "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // ════════════════════════════════════════════════════════════════════
        //  NAVEGACIÓN Y UTILIDADES
        // ════════════════════════════════════════════════════════════════════

        private void AbrirGrafica(Vector2D[] vectores, string[] nombres, string titulo)
        {
            var g = new Forms_graphics_vectores(vectores, nombres, titulo, this);
            g.Show();
            this.Hide();
        }

        /// <summary>Muestra el resultado en el label inferior.</summary>
        private void MostrarResultado(string texto)
        {
            lblResultado.Text = "Resultado: " + texto;
        }

        // ════════════════════════════════════════════════════════════════════
        //  MANEJADORES DE EVENTOS
        // ════════════════════════════════════════════════════════════════════

        // ── Limpiar ──────────────────────────────────────────────────────────
        private void BtnLimpiar_Click(object s, EventArgs e)
        {
            txtX1.Clear();
            txtY1.Clear();
            txtX2.Clear();
            txtY2.Clear();
            txtEscalar.Clear();
            lblResultado.Text = "Resultado: —";
        }

        // ── Forma polar ──────────────────────────────────────────────────────
        private void BtnPolarV1_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v))
                MostrarResultado(string.Format(
                    "V1 polar → Magnitud: {0:F4} | Ángulo: {1:F2}°",
                    v.Magnitud(), v.AnguloGrados()));
        }

        private void BtnPolarV2_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX2, txtY2, out Vector2D v))
                MostrarResultado(string.Format(
                    "V2 polar → Magnitud: {0:F4} | Ángulo: {1:F2}°",
                    v.Magnitud(), v.AnguloGrados()));
        }

        // ── Operaciones individuales ─────────────────────────────────────────
        private void BtnEscalarV1_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) && ValidarEscalar(out double esc))
            {
                var r = Vector2D.Multiplicar(v1, esc);
                MostrarResultado(string.Format("V1 × {0} = {1}", esc, r));
                AbrirGrafica(new[] { v1, r },
                             new[] { "V1 original", string.Format("V1 × {0}", esc) },
                             "Multiplicación Escalar V1");
            }
        }

        private void BtnEscalarV2_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX2, txtY2, out Vector2D v2) && ValidarEscalar(out double esc))
            {
                var r = Vector2D.Multiplicar(v2, esc);
                MostrarResultado(string.Format("V2 × {0} = {1}", esc, r));
                AbrirGrafica(new[] { v2, r },
                             new[] { "V2 original", string.Format("V2 × {0}", esc) },
                             "Multiplicación Escalar V2");
            }
        }

        private void BtnNormalV1_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1))
            {
                try
                {
                    var r = v1.Normalizar();
                    MostrarResultado(string.Format("V1 normalizado: {0}", r));
                    AbrirGrafica(new[] { v1, r },
                                 new[] { "V1 original", "V1 normalizado" },
                                 "Normalización V1");
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnNormalV2_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                try
                {
                    var r = v2.Normalizar();
                    MostrarResultado(string.Format("V2 normalizado: {0}", r));
                    AbrirGrafica(new[] { v2, r },
                                 new[] { "V2 original", "V2 normalizado" },
                                 "Normalización V2");
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // ── Operaciones entre V1 y V2 ────────────────────────────────────────
        private void BtnSumar_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) &&
                ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                var r = Vector2D.Sumar(v1, v2);
                MostrarResultado(string.Format("V1 + V2 = {0}", r));
                AbrirGrafica(new[] { v1, v2, r },
                             new[] { "V1", "V2", "V1+V2" },
                             "Suma de Vectores");
            }
        }

        private void BtnRestar_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) &&
                ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                var r = Vector2D.Restar(v1, v2);
                MostrarResultado(string.Format("V1 − V2 = {0}", r));
                AbrirGrafica(new[] { v1, v2, r },
                             new[] { "V1", "V2", "V1−V2" },
                             "Resta de Vectores");
            }
        }

        private void BtnPunto_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) &&
                ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                double r = v1.ProductoPunto(v2);
                string extra = v1.EsOrtogonalA(v2) ? " ← vectores ortogonales" : "";
                MostrarResultado(string.Format("V1·V2 = {0:F4}{1}", r, extra));
            }
        }

        private void BtnCruzado_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) &&
                ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                double r = v1.ProductoCruzado(v2);
                string orientacion = r > 0 ? " (V2 está a la izquierda de V1)"
                                   : r < 0 ? " (V2 está a la derecha de V1)"
                                   : " (vectores paralelos)";
                MostrarResultado(string.Format("V1×V2 = {0:F4}{1}", r, orientacion));
            }
        }

        private void BtnAngulo_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) &&
                ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                try
                {
                    double angulo = v1.AnguloEntre(v2);
                    MostrarResultado(string.Format("Ángulo entre V1 y V2: {0:F2}°", angulo));
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnParalelos_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) &&
                ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                bool paralelos = v1.EsParaleloA(v2);
                MostrarResultado(paralelos
                    ? "V1 y V2 son PARALELOS (o antiparalelos)."
                    : "V1 y V2 NO son paralelos.");
            }
        }

        private void BtnVerAmbos_Click(object s, EventArgs e)
        {
            if (ValidarVector(txtX1, txtY1, out Vector2D v1) &&
                ValidarVector(txtX2, txtY2, out Vector2D v2))
            {
                AbrirGrafica(new[] { v1, v2 },
                             new[] { "Vector 1", "Vector 2" },
                             "Comparación de Vectores");
            }
        }
    }
}