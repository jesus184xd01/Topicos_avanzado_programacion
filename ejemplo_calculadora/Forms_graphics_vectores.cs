using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Libreria_Vectores;

namespace ejemplo_calculadora
{
    public partial class Forms_graphics_vectores : Form
    {
        private readonly Vector2D[] vectores;
        private readonly string[] nombres;
        private readonly Form_vectores origen;
        private Panel panel;

        // Paleta de colores para hasta 5 vectores
        private static readonly Color[] Paleta =
        {
            Color.FromArgb(41,  128, 185),  // azul
            Color.FromArgb(192,  57,  43),  // rojo
            Color.FromArgb(39,  174,  96),  // verde
            Color.FromArgb(142,  68, 173),  // morado
            Color.FromArgb(230, 126,  34)   // naranja
        };

        public Forms_graphics_vectores(Vector2D[] vs, string[] ns, string titulo, Form_vectores formOrigen)
        {
            vectores = vs;
            nombres = ns;
            origen = formOrigen;
            Inicializar(titulo);
        }

        // ════════════════════════════════════════════════════════════════════
        //  CONSTRUCCIÓN DE LA INTERFAZ
        // ════════════════════════════════════════════════════════════════════
        private void Inicializar(string titulo)
        {
            this.Text = "Plano Cartesiano — " + titulo;
            this.Size = new Size(820, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblTitulo = new Label()
            {
                Text = titulo,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(50, 14),
                AutoSize = true
            };

            panel = new Panel()
            {
                Location = new Point(50, 55),
                Size = new Size(700, 560),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panel.Paint += DibujarPlano;

            // ── Info de vectores debajo del plano ────────────────────────────
            Label lblInfo = new Label()
            {
                Location = new Point(50, 622),
                Size = new Size(570, 30),
                Font = new Font("Segoe UI", 9),
                Text = ConstruirInfoVectores(),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            Button btnVolver = new Button()
            {
                Text = "← Volver",
                Location = new Point(660, 622),
                Width = 90,
                Height = 32,
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += (s, e) => { origen.Show(); this.Close(); };

            this.Controls.AddRange(new Control[] { lblTitulo, panel, lblInfo, btnVolver });
        }

        private string ConstruirInfoVectores()
        {
            if (vectores == null || vectores.Length == 0) return string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < vectores.Length; i++)
            {
                if (i > 0) sb.Append("   |   ");
                string nom = (nombres != null && i < nombres.Length) ? nombres[i] : ("V" + (i + 1));
                sb.AppendFormat("{0}: mag={1:F2}  θ={2:F1}°",
                    nom, vectores[i].Magnitud(), vectores[i].AnguloGrados());
            }
            return sb.ToString();
        }

        // ════════════════════════════════════════════════════════════════════
        //  DIBUJO
        // ════════════════════════════════════════════════════════════════════
        private void DibujarPlano(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int escala = CalcularEscala();
            Point centro = new Point(panel.Width / 2, panel.Height / 2);

            DibujarGrilla(g, escala, centro);
            DibujarEjes(g, escala, centro);

            if (vectores == null) return;

            for (int i = 0; i < vectores.Length; i++)
            {
                Color color = Paleta[i % Paleta.Length];
                string nom = (nombres != null && i < nombres.Length) ? nombres[i] : ("V" + (i + 1));
                DibujarVector(g, vectores[i], nom, color, escala, centro);
            }

            DibujarLeyenda(g);
        }

        // ── Escala automática ────────────────────────────────────────────────
        private int CalcularEscala()
        {
            if (vectores == null || vectores.Length == 0) return 30;

            double maxMag = 1;
            foreach (Vector2D v in vectores)
            {
                double mag = Math.Max(Math.Abs(v.X), Math.Abs(v.Y));
                if (mag > maxMag) maxMag = mag;
            }

            int maxPixeles = Math.Min(panel.Width, panel.Height) / 2 - 40;
            int escala = (int)(maxPixeles / maxMag);
            return Math.Max(10, Math.Min(escala, 55));
        }

        // ── Grilla ───────────────────────────────────────────────────────────
        private void DibujarGrilla(Graphics g, int escala, Point centro)
        {
            using (Pen gridPen = new Pen(Color.FromArgb(225, 225, 225), 1))
            {
                gridPen.DashStyle = DashStyle.Dot;

                for (int x = centro.X % escala; x < panel.Width; x += escala)
                    g.DrawLine(gridPen, x, 0, x, panel.Height);
                for (int y = centro.Y % escala; y < panel.Height; y += escala)
                    g.DrawLine(gridPen, 0, y, panel.Width, y);
            }
        }

        // ── Ejes ─────────────────────────────────────────────────────────────
        private void DibujarEjes(Graphics g, int escala, Point centro)
        {
            using (Pen ejePen = new Pen(Color.Black, 2))
            {
                ejePen.CustomEndCap = new AdjustableArrowCap(5, 5);

                g.DrawLine(ejePen, 10, centro.Y, panel.Width - 10, centro.Y);  // eje X
                g.DrawLine(ejePen, centro.X, panel.Height - 10, centro.X, 10);  // eje Y
            }

            using (Font fEje = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                g.DrawString("X", fEje, Brushes.Black, panel.Width - 20, centro.Y - 18);
                g.DrawString("Y", fEje, Brushes.Black, centro.X + 6, 8);
            }

            // Marcas numéricas
            using (Font fNum = new Font("Segoe UI", 7))
            using (Pen marca = new Pen(Color.Gray, 1))
            {
                for (int i = -30; i <= 30; i++)
                {
                    if (i == 0) continue;

                    int px = centro.X + i * escala;
                    int py = centro.Y - i * escala;

                    if (px > 5 && px < panel.Width - 5)
                    {
                        g.DrawLine(marca, px, centro.Y - 4, px, centro.Y + 4);
                        g.DrawString(i.ToString(), fNum, Brushes.Gray, px - 5, centro.Y + 6);
                    }
                    if (py > 5 && py < panel.Height - 5)
                    {
                        g.DrawLine(marca, centro.X - 4, py, centro.X + 4, py);
                        g.DrawString(i.ToString(), fNum, Brushes.Gray, centro.X + 6, py - 7);
                    }
                }
            }
        }

        // ── Vector individual ────────────────────────────────────────────────
        private void DibujarVector(Graphics g, Vector2D v, string nombre,
                                   Color color, int escala, Point centro)
        {
            if (v == null) return;

            Point fin = new Point(
                centro.X + (int)(v.X * escala),
                centro.Y - (int)(v.Y * escala)
            );

            // Línea con punta de flecha
            using (Pen vPen = new Pen(color, 3))
            {
                vPen.CustomEndCap = new AdjustableArrowCap(6, 6);
                g.DrawLine(vPen, centro, fin);
            }

            // Punto en el extremo
            int r = 5;
            using (SolidBrush bColor = new SolidBrush(color))
                g.FillEllipse(bColor, fin.X - r, fin.Y - r, r * 2, r * 2);

            // Etiqueta flotante con fondo semitransparente
            string etiqueta = string.Format("{0}\n({1:G4}, {2:G4})\n|{3:F2}| ∠{4:F1}°",
                                            nombre, v.X, v.Y, v.Magnitud(), v.AnguloGrados());

            using (Font fLabel = new Font("Segoe UI", 8.5f))
            {
                SizeF tam = g.MeasureString(etiqueta, fLabel);
                int ex = fin.X + 9;
                int ey = fin.Y - (int)(tam.Height / 2);

                // Ajuste para no salir del panel
                if (ex + tam.Width > panel.Width - 5) ex = fin.X - (int)tam.Width - 9;
                if (ey < 5) ey = 5;
                if (ey + tam.Height > panel.Height - 5) ey = panel.Height - (int)tam.Height - 5;

                RectangleF fondo = new RectangleF(ex - 2, ey - 2, tam.Width + 4, tam.Height + 4);

                using (SolidBrush bFondo = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
                    g.FillRectangle(bFondo, fondo);

                using (SolidBrush bTexto = new SolidBrush(color))
                    g.DrawString(etiqueta, fLabel, bTexto, ex, ey);
            }
        }

        // ── Leyenda ──────────────────────────────────────────────────────────
        private void DibujarLeyenda(Graphics g)
        {
            if (vectores == null || vectores.Length <= 1) return;

            int x = 10, y = 10;
            using (Font f = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                for (int i = 0; i < vectores.Length; i++)
                {
                    Color c = Paleta[i % Paleta.Length];
                    using (SolidBrush b = new SolidBrush(c))
                        g.FillRectangle(b, x, y + i * 22, 14, 14);

                    string etNom = (nombres != null && i < nombres.Length)
                        ? nombres[i] : string.Format("V{0}", i + 1);
                    g.DrawString(etNom, f, Brushes.Black, x + 18, y + i * 22);
                }
            }
        }
    }
}