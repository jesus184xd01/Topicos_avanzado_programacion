using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurante
{
    public partial class FormPedido : Form
    {
        // ── Lista temporal de items en el pedido ──────────────────
        private List<ItemPedido> _items = new List<ItemPedido>();

        // ── Controles referenciados ───────────────────────────────
        private DataGridView dgv_items;
        private ComboBox cbo_destino;
        private ComboBox cbo_pago;
        private TextBox txt_mesa;
        private TextBox txt_monto;
        private TextBox txt_notas;
        private Label lbl_mesa;
        private Label lbl_monto;
        private Label lbl_total_items;
        private Label lbl_subtotal;
        private Label lbl_total;
        private Label lbl_cambio;
        private Label lbl_cambio_tit;
        private Label lbl_cambio_entregado_tit;
        private Label lbl_cambio_entregado;
        private Button btn_confirmar;
        private Button btn_cerrar;
        private Button btn_limpiar;

        public FormPedido()
        {
            InitializeComponent();
            ConstruirUI();
        }

        // ══════════════════════════════════════════════════════════
        //  MODELO TEMPORAL
        // ══════════════════════════════════════════════════════════
        public class ItemPedido
        {
            public int Id { get; set; }
            public string Tipo { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal => Precio * Cantidad;
        }

        // ══════════════════════════════════════════════════════════
        //  CONSTRUCCIÓN DEL FRONT
        // ══════════════════════════════════════════════════════════
        private void ConstruirUI()
        {
            this.Text = "Pedido";
            this.Size = new Size(1100, 740);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(245, 240, 230);

            // ── Panel izquierdo ───────────────────────────────────
            var pnlIzq = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(680, 690),
                BackColor = Color.FromArgb(235, 225, 210)
            };
            pnlIzq.Paint += (s, e) => e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(180, 140, 80), 2),
                new Rectangle(0, 0, pnlIzq.Width - 1, pnlIzq.Height - 1));
            this.Controls.Add(pnlIzq);

            pnlIzq.Controls.Add(new Label
            {
                Text = "ITEMS DEL PEDIDO",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                AutoSize = true,
                Location = new Point(14, 14)
            });

            pnlIzq.Controls.Add(new Panel
            {
                Location = new Point(0, 38),
                Size = new Size(680, 2),
                BackColor = Color.FromArgb(180, 140, 80)
            });

            // ── DataGridView ──────────────────────────────────────
            dgv_items = new DataGridView
            {
                Location = new Point(10, 48),
                Size = new Size(658, 510),
                ReadOnly = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.FromArgb(30, 20, 10),
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(60, 60, 60),
                Font = new Font("Segoe UI", 9f),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgv_items.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 50, 10);
            dgv_items.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_items.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv_items.EnableHeadersVisualStyles = false;
            dgv_items.ColumnHeadersHeight = 34;
            dgv_items.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(45, 30, 15);
            dgv_items.DefaultCellStyle.BackColor = Color.FromArgb(30, 20, 10);
            dgv_items.DefaultCellStyle.ForeColor = Color.White;
            dgv_items.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 60, 20);
            dgv_items.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv_items.RowTemplate.Height = 34;

            dgv_items.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col_tipo",
                HeaderText = "Tipo",
                FillWeight = 15,
                ReadOnly = true
            });
            dgv_items.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col_nombre",
                HeaderText = "Nombre",
                FillWeight = 38,
                ReadOnly = true
            });
            dgv_items.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col_precio",
                HeaderText = "Precio",
                FillWeight = 14,
                ReadOnly = true
            });
            dgv_items.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col_cantidad",
                HeaderText = "Cantidad",
                FillWeight = 13,
                ReadOnly = false
            });
            dgv_items.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col_subtotal",
                HeaderText = "Subtotal",
                FillWeight = 14,
                ReadOnly = true
            });
            dgv_items.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "col_eliminar",
                HeaderText = "",
                Text = "✖",
                UseColumnTextForButtonValue = true,
                FillWeight = 6
            });

            pnlIzq.Controls.Add(dgv_items);

            // ── Botón limpiar ─────────────────────────────────────
            btn_limpiar = CrearBoton("Limpiar pedido", 10, 572, 180,
                Color.FromArgb(100, 100, 110), Color.White);
            pnlIzq.Controls.Add(btn_limpiar);

            // ── Panel derecho ─────────────────────────────────────
            var pnlDer = new Panel
            {
                Location = new Point(700, 10),
                Size = new Size(380, 690),
                BackColor = Color.FromArgb(235, 225, 210)
            };
            pnlDer.Paint += (s, e) => e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(180, 140, 80), 2),
                new Rectangle(0, 0, pnlDer.Width - 1, pnlDer.Height - 1));
            this.Controls.Add(pnlDer);

            pnlDer.Controls.Add(new Label
            {
                Text = "DATOS DEL PEDIDO",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                AutoSize = true,
                Location = new Point(14, 14)
            });

            pnlDer.Controls.Add(new Panel
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

            int y = 50, ix = 14, iw = 348;

            // ── Destino ───────────────────────────────────────────
            pnlDer.Controls.Add(MkLbl("DESTINO", y)); y += 18;
            cbo_destino = new ComboBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cbo_destino.Items.AddRange(new object[] { "Mesa", "Para llevar" });
            cbo_destino.SelectedIndex = 0;
            pnlDer.Controls.Add(cbo_destino); y += 34;

            // ── Número de mesa ────────────────────────────────────
            lbl_mesa = MkLbl("NÚMERO DE MESA", y);
            pnlDer.Controls.Add(lbl_mesa); y += 18;

            txt_mesa = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            pnlDer.Controls.Add(txt_mesa); y += 34;

            // ── Tipo de pago ──────────────────────────────────────
            pnlDer.Controls.Add(MkLbl("TIPO DE PAGO", y)); y += 18;
            cbo_pago = new ComboBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cbo_pago.Items.AddRange(new object[] { "Efectivo", "Tarjeta", "Transferencia" });
            cbo_pago.SelectedIndex = 0;
            pnlDer.Controls.Add(cbo_pago); y += 34;

            // ── Monto recibido ────────────────────────────────────
            lbl_monto = MkLbl("MONTO RECIBIDO", y);
            pnlDer.Controls.Add(lbl_monto); y += 18;

            txt_monto = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 26),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            pnlDer.Controls.Add(txt_monto); y += 34;

            // ── Separador resumen ─────────────────────────────────
            pnlDer.Controls.Add(new Panel
            {
                Location = new Point(0, y),
                Size = new Size(380, 2),
                BackColor = Color.FromArgb(180, 140, 80)
            }); y += 14;

            // ── Total items ───────────────────────────────────────
            pnlDer.Controls.Add(MkLbl("TOTAL ITEMS", y));
            lbl_total_items = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(60, 40, 20),
                AutoSize = true,
                Location = new Point(iw - 30, y)
            };
            pnlDer.Controls.Add(lbl_total_items); y += 26;

            // ── Subtotal ──────────────────────────────────────────
            pnlDer.Controls.Add(MkLbl("SUBTOTAL", y));
            lbl_subtotal = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(60, 40, 20),
                AutoSize = true,
                Location = new Point(iw - 60, y)
            };
            pnlDer.Controls.Add(lbl_subtotal); y += 26;

            // ── Cambio ────────────────────────────────────────────
            lbl_cambio_tit = MkLbl("CAMBIO", y);
            lbl_cambio_tit.Visible = false;
            pnlDer.Controls.Add(lbl_cambio_tit);

            lbl_cambio = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 125, 50),
                AutoSize = true,
                Location = new Point(iw - 60, y),
                Visible = false
            };
            pnlDer.Controls.Add(lbl_cambio); y += 26;

            // ── Cambio entregado ──────────────────────────────────
            lbl_cambio_entregado_tit = MkLbl("CAMBIO ENTREGADO", y);
            lbl_cambio_entregado_tit.Visible = false;
            pnlDer.Controls.Add(lbl_cambio_entregado_tit);

            lbl_cambio_entregado = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 125, 50),
                AutoSize = true,
                Location = new Point(iw - 60, y),
                Visible = false
            };
            pnlDer.Controls.Add(lbl_cambio_entregado); y += 26;

            // ── Separador total ───────────────────────────────────
            pnlDer.Controls.Add(new Panel
            {
                Location = new Point(0, y),
                Size = new Size(380, 2),
                BackColor = Color.FromArgb(180, 140, 80)
            }); y += 14;

            // ── Total grande ──────────────────────────────────────
            pnlDer.Controls.Add(new Label
            {
                Text = "TOTAL",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 50, 10),
                AutoSize = true,
                Location = new Point(ix, y)
            });
            lbl_total = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 60, 20),
                AutoSize = true,
                Location = new Point(iw - 80, y)
            };
            pnlDer.Controls.Add(lbl_total); y += 46;

            // ── Notas ─────────────────────────────────────────────
            pnlDer.Controls.Add(MkLbl("NOTAS / OBSERVACIONES", y)); y += 18;
            txt_notas = new TextBox
            {
                Location = new Point(ix, y),
                Size = new Size(iw, 60),
                Font = new Font("Segoe UI", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            pnlDer.Controls.Add(txt_notas); y += 72;

            // ── Botón confirmar ───────────────────────────────────
            btn_confirmar = CrearBoton("Confirmar pedido", ix, y, iw,
                Color.FromArgb(180, 60, 20), Color.White);
            btn_confirmar.Height = 42;
            btn_confirmar.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            pnlDer.Controls.Add(btn_confirmar); y += 52;

            // ── Botón cerrar ──────────────────────────────────────
            btn_cerrar = CrearBoton("Cerrar", ix, y, iw,
                Color.FromArgb(100, 100, 110), Color.White);
            btn_cerrar.Click += (s, e) => this.Close();
            pnlDer.Controls.Add(btn_cerrar);

            // ══════════════════════════════════════════════════════
            //  EVENTOS CONDICIONALES
            // ══════════════════════════════════════════════════════

            // ── Mostrar/ocultar mesa ──────────────────────────────
            cbo_destino.SelectedIndexChanged += (s, e) =>
            {
                bool esMesa = cbo_destino.Text == "Mesa";
                lbl_mesa.Visible = esMesa;
                txt_mesa.Visible = esMesa;
            };

            // ── Mostrar/ocultar campos efectivo ───────────────────
            cbo_pago.SelectedIndexChanged += (s, e) =>
            {
                bool esEfectivo = cbo_pago.Text == "Efectivo";
                lbl_monto.Visible = esEfectivo;
                txt_monto.Visible = esEfectivo;
                lbl_cambio_tit.Visible = esEfectivo;
                lbl_cambio.Visible = esEfectivo;

                if (!esEfectivo)
                {
                    lbl_cambio_entregado_tit.Visible = false;
                    lbl_cambio_entregado.Visible = false;
                    txt_monto.Text = "";
                }
            };

            // ── Calcular cambio en tiempo real ────────────────────
            txt_monto.TextChanged += (s, e) =>
            {
                if (decimal.TryParse(txt_monto.Text, out decimal monto))
                {
                    decimal total = _items.Sum(i => i.Subtotal);
                    decimal cambio = monto - total;

                    lbl_cambio.Text = cambio >= 0
                        ? $"${cambio:F2}"
                        : "Monto insuficiente";
                    lbl_cambio.ForeColor = cambio >= 0
                        ? Color.FromArgb(46, 125, 50)
                        : Color.FromArgb(198, 40, 40);

                    bool hayCambio = cambio > 0;
                    lbl_cambio_entregado_tit.Visible = hayCambio;
                    lbl_cambio_entregado.Visible = hayCambio;
                    lbl_cambio_entregado.Text = hayCambio
                        ? $"${cambio:F2}"
                        : "$0.00";
                }
                else
                {
                    lbl_cambio.Text = "$0.00";
                    lbl_cambio.ForeColor = Color.FromArgb(46, 125, 50);
                    lbl_cambio_entregado_tit.Visible = false;
                    lbl_cambio_entregado.Visible = false;
                }
            };

            // ── Solo números en monto ─────────────────────────────
            txt_monto.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8)
                    e.Handled = true;
                if (e.KeyChar == '.' && txt_monto.Text.Contains('.'))
                    e.Handled = true;
            };

            // ── Eliminar fila con botón ✖ ─────────────────────────
            dgv_items.CellClick += (s, e) =>
            {
                if (e.ColumnIndex == dgv_items.Columns["col_eliminar"].Index && e.RowIndex >= 0)
                {
                    _items.RemoveAt(e.RowIndex);
                    dgv_items.Rows.RemoveAt(e.RowIndex);
                    ActualizarResumen();
                }
            };

            // ── Actualizar subtotal al editar cantidad ────────────
            dgv_items.CellEndEdit += (s, e) =>
            {
                if (e.ColumnIndex == dgv_items.Columns["col_cantidad"].Index && e.RowIndex >= 0)
                {
                    var cell = dgv_items.Rows[e.RowIndex].Cells["col_cantidad"];

                    if (int.TryParse(cell.Value?.ToString(), out int nuevaCantidad) && nuevaCantidad > 0)
                    {
                        _items[e.RowIndex].Cantidad = nuevaCantidad;
                        dgv_items.Rows[e.RowIndex].Cells["col_subtotal"].Value =
                            $"${_items[e.RowIndex].Subtotal:F2}";
                        ActualizarResumen();
                    }
                    else
                    {
                        cell.Value = _items[e.RowIndex].Cantidad;
                    }
                }
            };

            // ── Conectar botones ──────────────────────────────────
            btn_limpiar.Click += btn_limpiar_Click;
            btn_confirmar.Click += btn_confirmar_Click;
        }

        // ── Helper botones ────────────────────────────────────────
        private Button CrearBoton(string texto, int x, int y,
            int ancho, Color fondo, Color letra)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, 34),
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                BackColor = fondo,
                ForeColor = letra,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // ══════════════════════════════════════════════════════════
        //  EVENTOS
        // ══════════════════════════════════════════════════════════
        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            _items.Clear();
            dgv_items.Rows.Clear();
            txt_monto.Text = "";
            txt_mesa.Text = "";
            txt_notas.Text = "";
            lbl_cambio_entregado_tit.Visible = false;
            lbl_cambio_entregado.Visible = false;
            ActualizarResumen();
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (_items.Count == 0)
            {
                MessageBox.Show("No hay items en el pedido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbo_destino.Text == "Mesa" && string.IsNullOrWhiteSpace(txt_mesa.Text))
            {
                MessageBox.Show("Ingresa el número de mesa.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_mesa.Focus();
                return;
            }

            if (cbo_pago.Text == "Efectivo")
            {
                if (!decimal.TryParse(txt_monto.Text, out decimal monto))
                {
                    MessageBox.Show("Ingresa el monto recibido.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_monto.Focus();
                    return;
                }

                decimal total = _items.Sum(i => i.Subtotal);
                if (monto < total)
                {
                    MessageBox.Show("El monto recibido es insuficiente.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_monto.Focus();
                    return;
                }
            }

            // Todo válido — aquí irá la llamada al DAO
            MessageBox.Show("Pedido listo para registrar.", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ══════════════════════════════════════════════════════════
        //  ACTUALIZAR RESUMEN
        // ══════════════════════════════════════════════════════════
        private void ActualizarResumen()
        {
            int totalItems = _items.Sum(i => i.Cantidad);
            decimal total = _items.Sum(i => i.Subtotal);

            lbl_total_items.Text = totalItems.ToString();
            lbl_subtotal.Text = $"${total:F2}";
            lbl_total.Text = $"${total:F2}";

            if (cbo_pago.Text == "Efectivo" &&
                decimal.TryParse(txt_monto.Text, out decimal monto))
            {
                decimal cambio = monto - total;

                lbl_cambio.Text = cambio >= 0 ? $"${cambio:F2}" : "Monto insuficiente";
                lbl_cambio.ForeColor = cambio >= 0
                    ? Color.FromArgb(46, 125, 50)
                    : Color.FromArgb(198, 40, 40);

                bool hayCambio = cambio > 0;
                lbl_cambio_entregado_tit.Visible = hayCambio;
                lbl_cambio_entregado.Visible = hayCambio;
                lbl_cambio_entregado.Text = hayCambio ? $"${cambio:F2}" : "$0.00";
            }
        }

        // ══════════════════════════════════════════════════════════
        //  AGREGAR ITEM — llamado desde alimento_card
        // ══════════════════════════════════════════════════════════
        public void AgregarItem(int id, string tipo, string nombre, decimal precio)
        {
            int indiceExistente = _items.FindIndex(i => i.Id == id && i.Tipo == tipo);

            if (indiceExistente >= 0)
            {
                _items[indiceExistente].Cantidad++;
                dgv_items.Rows[indiceExistente].Cells["col_cantidad"].Value =
                    _items[indiceExistente].Cantidad;
                dgv_items.Rows[indiceExistente].Cells["col_subtotal"].Value =
                    $"${_items[indiceExistente].Subtotal:F2}";
            }
            else
            {
                var item = new ItemPedido
                {
                    Id = id,
                    Tipo = tipo,
                    Nombre = nombre,
                    Precio = precio,
                    Cantidad = 1
                };
                _items.Add(item);
                dgv_items.Rows.Add(tipo, nombre, $"${precio:F2}", 1, $"${precio:F2}", "✖");
            }

            ActualizarResumen();
        }

        // ══════════════════════════════════════════════════════════
        //  CARGA INICIAL
        // ══════════════════════════════════════════════════════════
        private void FormPedido_Load(object sender, EventArgs e) { }
    }
}