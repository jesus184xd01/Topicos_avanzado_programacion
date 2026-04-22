using System;
using System.Drawing;
using System.Windows.Forms;

namespace restaurante
{
    public partial class management : Form
    {
        // ── Controles ─────────────────────────────────────────────
        private ComboBox cbo_tipo_alimento;
        private ComboBox cbo_categoria;
        private TextBox txt_nombre;
        private TextBox txt_descripcion;
        private TextBox txt_precio;
        private TextBox txt_imagen_url;
        private TextBox txt_capacidad;
        private PictureBox pic_preview;
        private Button btn_examinar;
        private CheckBox chk_vegetariano;
        private CheckBox chk_disponible;
        private DataGridView dgv_alimentos;
        private Button btn_guardar;
        private Button btn_eliminar;
        private Button btn_limpiar;
        private Button btn_cerrar;
        private Label lbl_capacidad;
        private Label lbl_chk_vegetariano;
        private Label lbl_chk_disponible;

        public management()
        {
            InitializeComponent();
            ConstruirUI();
        }

        // ══════════════════════════════════════════════════════════
        //  CONSTRUCCIÓN DEL FRONT
        // ══════════════════════════════════════════════════════════
        private void ConstruirUI()
        {
            this.Text = "Administrar Menú";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(245, 240, 230);

            // ══════════════════════════════════════════════════════
            //  PANEL IZQUIERDO — Formulario
            // ══════════════════════════════════════════════════════
            var pnlIzq = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(380, 690),
                BackColor = Color.FromArgb(235, 225, 210)
            };
            pnlIzq.Paint += (s, e) => e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(180, 140, 80), 2),
                new Rectangle(0, 0, pnlIzq.Width - 1, pnlIzq.Height - 1));
            this.Controls.Add(pnlIzq);

            // ── Título ────────────────────────────────────────────
            pnlIzq.Controls.Add(new Label
            {
                Text = "DATOS DEL ALIMENTO",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                AutoSize = true,
                Location = new Point(20, 14)
            });

            // ── Separador ─────────────────────────────────────────
            pnlIzq.Controls.Add(new Panel
            {
                Location = new Point(0, 38),
                Size = new Size(380, 2),
                BackColor = Color.FromArgb(180, 140, 80)
            });

            // ── Helper label ──────────────────────────────────────
            Label MkLbl(string texto, int posY) => new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                Location = new Point(14, posY),
                AutoSize = true
            };

            int ix = 14, iw = 350, y = 50;

            // ── Tipo de alimento ──────────────────────────────────
            pnlIzq.Controls.Add(MkLbl("TIPO DE ALIMENTO", y)); y += 18;
            cbo_tipo_alimento = new ComboBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cbo_tipo_alimento.Items.AddRange(new object[] { "Platillo", "Bebida", "Postre" });
            cbo_tipo_alimento.SelectedIndex = 0;
            cbo_tipo_alimento.SelectedIndexChanged += cbo_tipo_alimento_SelectedIndexChanged;
            pnlIzq.Controls.Add(cbo_tipo_alimento); y += 34;

            // ── Categoría ─────────────────────────────────────────
            pnlIzq.Controls.Add(MkLbl("CATEGORÍA", y)); y += 18;
            cbo_categoria = new ComboBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cbo_categoria.Items.AddRange(new object[] { "Desayuno", "Almuerzo", "Comida", "Cena" });
            cbo_categoria.SelectedIndex = 0;
            pnlIzq.Controls.Add(cbo_categoria); y += 34;

            // ── Nombre ────────────────────────────────────────────
            pnlIzq.Controls.Add(MkLbl("NOMBRE *", y)); y += 18;
            txt_nombre = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            pnlIzq.Controls.Add(txt_nombre); y += 34;

            // ── Descripción ───────────────────────────────────────
            pnlIzq.Controls.Add(MkLbl("DESCRIPCIÓN", y)); y += 18;
            txt_descripcion = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 56),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            pnlIzq.Controls.Add(txt_descripcion); y += 64;

            // ── Precio ────────────────────────────────────────────
            pnlIzq.Controls.Add(MkLbl("PRECIO *", y)); y += 18;
            txt_precio = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            pnlIzq.Controls.Add(txt_precio); y += 34;

            // ── Capacidad (solo bebidas) ──────────────────────────
            lbl_capacidad = MkLbl("CAPACIDAD", y);
            lbl_capacidad.Visible = false;
            pnlIzq.Controls.Add(lbl_capacidad); y += 18;

            txt_capacidad = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Visible = false
            };
            pnlIzq.Controls.Add(txt_capacidad); y += 34;

            // ── Checkboxes ────────────────────────────────────────
            chk_vegetariano = new CheckBox
            {
                Text = "Vegetariano",
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(80, 40, 10),
                Location = new Point(ix, y),
                AutoSize = true,
                Visible = true
            };
            pnlIzq.Controls.Add(chk_vegetariano);

            chk_disponible = new CheckBox
            {
                Text = "Disponible",
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(80, 40, 10),
                Location = new Point(ix + 150, y),
                AutoSize = true,
                Visible = true
            };
            pnlIzq.Controls.Add(chk_disponible); y += 30;

            // ── Imagen URL ────────────────────────────────────────
            pnlIzq.Controls.Add(MkLbl("URL DE IMAGEN", y)); y += 18;
            txt_imagen_url = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw - 84, 26),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            pnlIzq.Controls.Add(txt_imagen_url);

            btn_examinar = new Button
            {
                Text = "👁 Preview",
                Location = new Point(ix + iw - 80, y),
                Size = new Size(80, 26),
                Font = new Font("Segoe UI", 8f),
                BackColor = Color.FromArgb(180, 140, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn_examinar.FlatAppearance.BorderSize = 0;
            pnlIzq.Controls.Add(btn_examinar); y += 32;

            // ── Preview imagen ────────────────────────────────────
            pic_preview = new PictureBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 100),
                BackColor = Color.FromArgb(210, 200, 185),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlIzq.Controls.Add(pic_preview); y += 108;

            // ── Botones acción ────────────────────────────────────
            btn_guardar = CrearBoton("💾  Guardar", ix, y, 110,
                Color.FromArgb(46, 125, 50), Color.White);
            pnlIzq.Controls.Add(btn_guardar);

            btn_eliminar = CrearBoton("🗑  Eliminar", ix + 118, y, 110,
                Color.FromArgb(198, 40, 40), Color.White);
            btn_eliminar.Enabled = false;
            pnlIzq.Controls.Add(btn_eliminar);

            btn_limpiar = CrearBoton("✖  Limpiar", ix + 236, y, 100,
                Color.FromArgb(100, 100, 110), Color.White);
            pnlIzq.Controls.Add(btn_limpiar);

            // ══════════════════════════════════════════════════════
            //  PANEL DERECHO — Grid
            // ══════════════════════════════════════════════════════
            var pnlDer = new Panel
            {
                Location = new Point(400, 10),
                Size = new Size(780, 690),
                BackColor = Color.FromArgb(235, 225, 210)
            };
            pnlDer.Paint += (s, e) => e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(180, 140, 80), 2),
                new Rectangle(0, 0, pnlDer.Width - 1, pnlDer.Height - 1));
            this.Controls.Add(pnlDer);

            // ── Título ────────────────────────────────────────────
            pnlDer.Controls.Add(new Label
            {
                Text = "REGISTROS",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                AutoSize = true,
                Location = new Point(14, 14)
            });

            // ── Botón cerrar ──────────────────────────────────────
            btn_cerrar = CrearBoton("✖  Cerrar", pnlDer.Width - 110, 8, 96,
                Color.FromArgb(198, 40, 40), Color.White);
            btn_cerrar.Click += (s, e) => this.Close();
            pnlDer.Controls.Add(btn_cerrar);

            // ── Separador ─────────────────────────────────────────
            pnlDer.Controls.Add(new Panel
            {
                Location = new Point(0, 38),
                Size = new Size(780, 2),
                BackColor = Color.FromArgb(180, 140, 80)
            });

            // ── DataGridView ──────────────────────────────────────
            dgv_alimentos = new DataGridView
            {
                Location = new Point(10, 48),
                Size = new Size(758, 630),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                BackgroundColor = Color.FromArgb(30, 20, 10),
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(60, 60, 60),
                Font = new Font("Segoe UI", 9f),
                Cursor = Cursors.Hand
            };

            // Encabezado
            dgv_alimentos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 50, 10);
            dgv_alimentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_alimentos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv_alimentos.EnableHeadersVisualStyles = false;
            dgv_alimentos.ColumnHeadersHeight = 34;

            // Filas
            dgv_alimentos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(45, 30, 15);
            dgv_alimentos.DefaultCellStyle.BackColor = Color.FromArgb(30, 20, 10);
            dgv_alimentos.DefaultCellStyle.ForeColor = Color.White;
            dgv_alimentos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(198, 40, 40);
            dgv_alimentos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv_alimentos.RowTemplate.Height = 30;

            pnlDer.Controls.Add(dgv_alimentos);
        }

        // ── Helper para crear botones uniformes ───────────────────
        private Button CrearBoton(string texto, int x, int y,
                                   int ancho, Color fondo, Color letra)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, 32),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                BackColor = fondo,
                ForeColor = letra,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // ══════════════════════════════════════════════════════════
        //  VISIBILIDAD CONDICIONAL
        // ══════════════════════════════════════════════════════════
        private void cbo_tipo_alimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = cbo_tipo_alimento.Text;

            lbl_capacidad.Visible = tipo == "Bebida";
            txt_capacidad.Visible = tipo == "Bebida";
            chk_vegetariano.Visible = tipo == "Platillo";
            chk_disponible.Visible = tipo == "Platillo" || tipo == "Postre";
        }

        // ══════════════════════════════════════════════════════════
        //  CARGA INICIAL
        // ══════════════════════════════════════════════════════════
        private void management_Load(object sender, EventArgs e)
        {
            // aquí irá la carga del grid cuando se conecte la BD
        }
    }
}