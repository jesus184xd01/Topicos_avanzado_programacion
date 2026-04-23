using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace restaurante
{
    public partial class management : Form
    {
        // ── Controles UI ──────────────────────────────────────────
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

        // ── DAO y estado ──────────────────────────────────────────
        private readonly ManagementDAO _dao = new ManagementDAO();
        private int _idSeleccionado = -1;

        public management()
        {
            InitializeComponent();
            ConstruirUI();

            // Conectar eventos aquí, no en Load
            btn_guardar.Click += btn_guardar_Click;
            btn_eliminar.Click += btn_eliminar_Click;
            btn_limpiar.Click += (s, e) => LimpiarCampos();
            btn_examinar.Click += btn_examinar_Click;
            dgv_alimentos.SelectionChanged += dgv_alimentos_SelectionChanged;

            CargarGrid();
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

            // ── Panel izquierdo ───────────────────────────────────
            var pnlIzq = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(380, 700),
                BackColor = Color.FromArgb(235, 225, 210)
            };
            pnlIzq.Paint += (s, e) => e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(180, 140, 80), 2),
                new Rectangle(0, 0, pnlIzq.Width - 1, pnlIzq.Height - 1));
            this.Controls.Add(pnlIzq);

            pnlIzq.Controls.Add(new Label
            {
                Text = "DATOS DEL ALIMENTO",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                AutoSize = true,
                Location = new Point(20, 14)
            });

            pnlIzq.Controls.Add(new Panel
            {
                Location = new Point(0, 38),
                Size = new Size(380, 2),
                BackColor = Color.FromArgb(180, 140, 80)
            });

            Label MkLbl(string texto, int posY) => new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                Location = new Point(14, posY),
                AutoSize = true
            };

            int ix = 14, iw = 350, y = 50;

            // ── Tipo ──────────────────────────────────────────────
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
            cbo_categoria.SelectedIndexChanged += (s, e) => CargarGrid();
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
            lbl_capacidad = MkLbl("CAPACIDAD (ej. 500ml, 1Vaso)", y);
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
                Checked = true,
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
                Text = "Preview",
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

            // ── Botones ───────────────────────────────────────────
            btn_guardar = CrearBoton("Guardar", ix, y, 110,
                Color.FromArgb(46, 125, 50), Color.White);
            pnlIzq.Controls.Add(btn_guardar);

            btn_eliminar = CrearBoton("Eliminar", ix + 118, y, 110,
                Color.FromArgb(198, 40, 40), Color.White);
            btn_eliminar.Enabled = false;
            pnlIzq.Controls.Add(btn_eliminar);

            btn_limpiar = CrearBoton("Limpiar", ix + 236, y, 100,
                Color.FromArgb(100, 100, 110), Color.White);
            pnlIzq.Controls.Add(btn_limpiar);

            // ── Panel derecho ─────────────────────────────────────
            var pnlDer = new Panel
            {
                Location = new Point(400, 10),
                Size = new Size(780, 700),
                BackColor = Color.FromArgb(235, 225, 210)
            };
            pnlDer.Paint += (s, e) => e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(180, 140, 80), 2),
                new Rectangle(0, 0, pnlDer.Width - 1, pnlDer.Height - 1));
            this.Controls.Add(pnlDer);

            pnlDer.Controls.Add(new Label
            {
                Text = "REGISTROS",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                AutoSize = true,
                Location = new Point(14, 14)
            });

            btn_cerrar = CrearBoton("Cerrar", pnlDer.Width - 110, 8, 96,
                Color.FromArgb(198, 40, 40), Color.White);
            btn_cerrar.Click += (s, e) => this.Close();
            pnlDer.Controls.Add(btn_cerrar);

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
                Size = new Size(758, 640),
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

            dgv_alimentos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 50, 10);
            dgv_alimentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_alimentos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv_alimentos.EnableHeadersVisualStyles = false;
            dgv_alimentos.ColumnHeadersHeight = 34;
            dgv_alimentos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(45, 30, 15);
            dgv_alimentos.DefaultCellStyle.BackColor = Color.FromArgb(30, 20, 10);
            dgv_alimentos.DefaultCellStyle.ForeColor = Color.White;
            dgv_alimentos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(198, 40, 40);
            dgv_alimentos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv_alimentos.RowTemplate.Height = 30;

            pnlDer.Controls.Add(dgv_alimentos);
        }

        // ── Helper botones ────────────────────────────────────────
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
        //  CARGA INICIAL
        // ══════════════════════════════════════════════════════════
        private void management_Load(object sender, EventArgs e) { }

        // ══════════════════════════════════════════════════════════
        //  GRID
        // ══════════════════════════════════════════════════════════
        private void CargarGrid()
        {
            string tipo = cbo_tipo_alimento.Text;
            string categoria = cbo_categoria.Text;

            dgv_alimentos.DataSource = _dao.CargarGrid(tipo, categoria);

            if (dgv_alimentos.Columns["ID"] != null)
                dgv_alimentos.Columns["ID"].Visible = false;
        }

        private void dgv_alimentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_alimentos.SelectedRows.Count == 0) return;

            var row = dgv_alimentos.SelectedRows[0];

            // Verificar que la fila tenga datos válidos
            if (row.Cells["ID"].Value == null || row.Cells["ID"].Value == DBNull.Value)
                return;

            _idSeleccionado = Convert.ToInt32(row.Cells["ID"].Value);
            txt_nombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
            txt_descripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? "";
            txt_precio.Text = row.Cells["Precio"].Value?.ToString() ?? "";
            txt_imagen_url.Text = row.Cells["Imagen"].Value?.ToString() ?? "";

            string tipo = cbo_tipo_alimento.Text;

            if (tipo == "Bebida" && dgv_alimentos.Columns["Capacidad"] != null)
                txt_capacidad.Text = row.Cells["Capacidad"].Value?.ToString() ?? "";

            if (tipo == "Platillo" && dgv_alimentos.Columns["Vegetariano"] != null)
                chk_vegetariano.Checked = Convert.ToBoolean(row.Cells["Vegetariano"].Value);

            if (tipo != "Bebida" && dgv_alimentos.Columns["Disponible"] != null)
                chk_disponible.Checked = Convert.ToBoolean(row.Cells["Disponible"].Value);

            btn_eliminar.Enabled = true;
            MostrarPreview(txt_imagen_url.Text);
        }

        // ══════════════════════════════════════════════════════════
        //  GUARDAR
        // ══════════════════════════════════════════════════════════
        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            string tipo = cbo_tipo_alimento.Text;
            string categoria = cbo_categoria.Text;
            string nombre = txt_nombre.Text.Trim();
            string desc = txt_descripcion.Text.Trim();
            string imagenUrl = txt_imagen_url.Text.Trim();
            bool esNuevo = _idSeleccionado == -1;

            if (!decimal.TryParse(txt_precio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = false;

            if (esNuevo)
            {
                // ── INSERT ────────────────────────────────────────────
                switch (tipo)
                {
                    case "Platillo":
                        ok = _dao.InsertarPlatillo(nombre, desc, precio,
                                 chk_vegetariano.Checked, chk_disponible.Checked,
                                 imagenUrl, categoria);
                        break;
                    case "Bebida":
                        if (string.IsNullOrWhiteSpace(txt_capacidad.Text))
                        {
                            MessageBox.Show("La capacidad es requerida para bebidas.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        ok = _dao.InsertarBebida(nombre, desc,
                                 txt_capacidad.Text.Trim(), precio, imagenUrl, categoria);
                        break;
                    case "Postre":
                        ok = _dao.InsertarPostre(nombre, desc, precio,
                                 chk_disponible.Checked, imagenUrl, categoria);
                        break;
                }
            }
            else
            {
                // ── UPDATE ────────────────────────────────────────────
                switch (tipo)
                {
                    case "Platillo":
                        ok = _dao.ActualizarPlatillo(_idSeleccionado, nombre, desc,
                                 precio, chk_vegetariano.Checked, chk_disponible.Checked,
                                 imagenUrl);
                        break;
                    case "Bebida":
                        if (string.IsNullOrWhiteSpace(txt_capacidad.Text))
                        {
                            MessageBox.Show("La capacidad es requerida para bebidas.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        ok = _dao.ActualizarBebida(_idSeleccionado, nombre, desc,
                                 txt_capacidad.Text.Trim(), precio, imagenUrl);
                        break;
                    case "Postre":
                        ok = _dao.ActualizarPostre(_idSeleccionado, nombre, desc,
                                 precio, chk_disponible.Checked, imagenUrl);
                        break;
                }
            }

            if (ok)
            {
                string accion = esNuevo ? "guardado" : "actualizado";
                MessageBox.Show($"Registro {accion} correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarGrid();
            }
        }

        // ══════════════════════════════════════════════════════════
        //  ELIMINAR
        // ══════════════════════════════════════════════════════════
        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == -1) return;

            var respuesta = MessageBox.Show(
                $"¿Eliminar '{txt_nombre.Text}'?\nEsta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes) return;

            bool ok = _dao.Eliminar(cbo_tipo_alimento.Text, _idSeleccionado);

            if (ok)
            {
                MessageBox.Show("Registro eliminado.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarGrid();
            }
        }

        // ══════════════════════════════════════════════════════════
        //  IMAGEN PREVIEW
        // ══════════════════════════════════════════════════════════
        private void btn_examinar_Click(object sender, EventArgs e)
        {
            MostrarPreview(txt_imagen_url.Text.Trim());
        }

        private void MostrarPreview(string imagenUrl)
        {
            if (string.IsNullOrWhiteSpace(imagenUrl))
            {
                pic_preview.Image = null;
                return;
            }

            try
            {
                string ruta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, imagenUrl);

                if (File.Exists(ruta))
                {
                    if (pic_preview.Image != null)
                    {
                        pic_preview.Image.Dispose();
                        pic_preview.Image = null;
                    }
                    byte[] bytes = File.ReadAllBytes(ruta);
                    using (var ms = new MemoryStream(bytes))
                        pic_preview.Image = new Bitmap(ms);
                }
                else
                {
                    pic_preview.Image = null;
                    MessageBox.Show("No se encontró la imagen en la ruta indicada.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                pic_preview.Image = null;
            }
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

            LimpiarCampos();
            CargarGrid();
        }

        // ══════════════════════════════════════════════════════════
        //  VALIDACIÓN
        // ══════════════════════════════════════════════════════════
        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txt_nombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_precio.Text))
            {
                MessageBox.Show("El precio es obligatorio.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_precio.Focus();
                return false;
            }
            return true;
        }

        // ══════════════════════════════════════════════════════════
        //  LIMPIAR
        // ══════════════════════════════════════════════════════════
        private void LimpiarCampos()
        {
            _idSeleccionado = -1;
            txt_nombre.Text = "";
            txt_descripcion.Text = "";
            txt_precio.Text = "";
            txt_capacidad.Text = "";
            txt_imagen_url.Text = "";
            chk_vegetariano.Checked = false;
            chk_disponible.Checked = true;

            if (pic_preview.Image != null)
            {
                pic_preview.Image.Dispose();
                pic_preview.Image = null;
            }

            btn_eliminar.Enabled = false;
            dgv_alimentos.ClearSelection();
        }
    }
}