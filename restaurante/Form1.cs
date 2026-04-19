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
    public partial class Form1 : Form
    {
        // ── Tamaño de la card ─────────────────────────────────────
        private readonly int cardAncho = 485;
        private readonly int cardAlto = 184;
        private readonly int gapCards = 8;

        // ── Instancia del DAO ─────────────────────────────────────
        private readonly RestauranteDAO dao = new RestauranteDAO();

        public Form1()
        {
            InitializeComponent();
        }

        // ══════════════════════════════════════════════════════════
        //  CONFIGURACIÓN INICIAL
        // ══════════════════════════════════════════════════════════
        private void settings(object sender, EventArgs e)
        {
            // ── Form principal ───────────────────────────────────
            this.Size = new Size(1400, 900);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // ── Panel fondo ──────────────────────────────────────
            panel_fondo.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            panel_fondo.Location = new Point(0, 0);

            // ── Título ───────────────────────────────────────────
            title_lbl.Location = new Point(
                (panel_fondo.Width - title_lbl.Width) / 2,
                10
            );

            // ── Botones categorías ───────────────────────────────
            int espaciado = 50;
            int anchoBotones = (btn_desayuno.Width * 4) + (espaciado * 3);
            int startX = (panel_fondo.Width / 2) - (anchoBotones / 2);
            int posY = title_lbl.Location.Y + title_lbl.Height + 10;

            btn_desayuno.Location = new Point(startX, posY);
            btn_almuerzo.Location = new Point(startX + btn_desayuno.Width + espaciado, posY);
            btn_comida.Location = new Point(startX + (btn_desayuno.Width + espaciado) * 2, posY);
            btn_cena.Location = new Point(startX + (btn_desayuno.Width + espaciado) * 3, posY);

            // ── Botón administrar al final de la fila ────────────
            btn_administrar.Location = new Point(
                btn_cena.Location.X + btn_cena.Width + espaciado,
                posY + (btn_cena.Height / 2) - (btn_administrar.Height / 2)
            );

            // ── Panel contenedor ─────────────────────────────────
            int margen = 200;
            int topContenedor = posY + btn_desayuno.Height + 20;

            panel_container.Location = new Point(margen, topContenedor);
            panel_container.Size = new Size(
                panel_fondo.Width - (margen * 2),
                this.ClientSize.Height - topContenedor - 10
            );

            // deshabilitar scroll del panel contenedor
            panel_container.AutoScroll = false;
            panel_container.HorizontalScroll.Visible = false;
            panel_container.VerticalScroll.Visible = false;
            panel_container.HorizontalScroll.Enabled = false;
            panel_container.VerticalScroll.Enabled = false;

            // ── Medidas base ─────────────────────────────────────
            int padding = 10;
            int gapEntrePaneles = 20;
            int tituloAlto = 28;
            int reduccion = 100;

            // ancho paneles superiores
            int anchoPanelUnaCol = (cardAncho - reduccion) + (padding * 2);

            // ancho y alto panel postres fijo
            int anchoPanelTresCols = ((cardAncho - reduccion) * 2) + (gapCards * 2) + (padding * 2) - 10;
            int altoPanelDesserts = (int)(cardAlto * 0.8) + 20;

            // alto paneles superiores
            int espacioUtil = panel_container.Height
                            - (padding * 3)
                            - (tituloAlto * 2)
                            - altoPanelDesserts;
            int altoArriba = espacioUtil - 80;

            // posiciones Y
            int startYArriba = tituloAlto + padding;
            int startYAbajo = startYArriba + altoArriba + padding + tituloAlto;

            // centrar paneles superiores
            int anchoDosPanel = (anchoPanelUnaCol * 2) + gapEntrePaneles;
            int startXArriba = (panel_container.Width / 2) - (anchoDosPanel / 2);

            // ── Panel meals ──────────────────────────────────────
            panel_meals.Size = new Size(anchoPanelUnaCol, altoArriba);
            panel_meals.Location = new Point(startXArriba, startYArriba);

            lbl_title_meals.Location = new Point(
                panel_meals.Location.X + (panel_meals.Width / 2) - (lbl_title_meals.Width / 2),
                panel_meals.Location.Y - tituloAlto + 4
            );

            // ── Panel drinks ─────────────────────────────────────
            panel_drinks.Size = new Size(anchoPanelUnaCol, altoArriba);
            panel_drinks.Location = new Point(
                startXArriba + anchoPanelUnaCol + gapEntrePaneles,
                startYArriba
            );

            lbl_title_drinks.Location = new Point(
                panel_drinks.Location.X + (panel_drinks.Width / 2) - (lbl_title_drinks.Width / 2),
                panel_drinks.Location.Y - tituloAlto + 4
            );

            // ── Panel desserts ───────────────────────────────────
            int startXAbajo = (panel_container.Width / 2) - (anchoPanelTresCols / 2);

            panel_desserts.Size = new Size(anchoPanelTresCols, altoPanelDesserts);
            panel_desserts.Location = new Point(startXAbajo, startYAbajo);

            lbl_title_desserts.Location = new Point(
                panel_desserts.Location.X + (panel_desserts.Width / 2) - (lbl_title_desserts.Width / 2),
                panel_desserts.Location.Y - tituloAlto + 4
            );
        }

        // ══════════════════════════════════════════════════════════
        //  CARGA DE CARDS
        // ══════════════════════════════════════════════════════════

        // ── Platillos (una columna) ───────────────────────────────
        private void CargarCardsPlatillos(Panel panel, List<Platillo> lista)
        {
            foreach (Control c in panel.Controls.OfType<FlowLayoutPanel>().ToList())
                panel.Controls.Remove(c);

            panel.AutoScroll = true;

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = panel.BackColor
            };

            foreach (var p in lista)
            {
                var card = new alimento_card(p)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    BackColor = panel.BackColor,
                    Margin = new Padding(4, 4, 4, 8),
                    Visible = true
                };
                flow.Controls.Add(card);
            }

            panel.Controls.Add(flow);
            flow.BringToFront();
        }

        // ── Bebidas (una columna) ────────────────────────────────
        private void CargarCardsBebidas(Panel panel, List<Bebida> lista)
        {
            foreach (Control c in panel.Controls.OfType<FlowLayoutPanel>().ToList())
                panel.Controls.Remove(c);

            panel.AutoScroll = true;

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = panel.BackColor
            };

            foreach (var b in lista)
            {
                var card = new alimento_card(b)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    BackColor = panel.BackColor,
                    Margin = new Padding(4, 4, 4, 8),
                    Visible = true
                };
                flow.Controls.Add(card);
            }

            panel.Controls.Add(flow);
            flow.BringToFront();
        }

        // ── Postres (dos columnas) ────────────────────────────────
        private void CargarCardsPostres(Panel panel, List<Postre> lista)
        {
            foreach (Control c in panel.Controls.OfType<FlowLayoutPanel>().ToList())
                panel.Controls.Remove(c);

            panel.AutoScroll = true;

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = panel.BackColor
            };

            foreach (var p in lista)
            {
                var card = new alimento_card(p)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    BackColor = panel.BackColor,
                    Margin = new Padding(4, 4, 4, 8),
                    Visible = true
                };
                flow.Controls.Add(card);
            }

            panel.Controls.Add(flow);
            flow.BringToFront();
        }

        // ══════════════════════════════════════════════════════════
        //  BOTONES CATEGORÍAS
        // ══════════════════════════════════════════════════════════
        private void CargarCategoria(string tipoDia)
        {
            string titulo = char.ToUpper(tipoDia[0]) + tipoDia.Substring(1);

            lbl_title_meals.Text = $"Platillos — {titulo}";
            lbl_title_drinks.Text = $"Bebidas — {titulo}";
            lbl_title_desserts.Text = $"Postres — {titulo}";

            List<Platillo> platillos = dao.ObtenerPlatillos(tipoDia);
            List<Bebida> bebidas = dao.ObtenerBebidas(tipoDia);
            List<Postre> postres = dao.ObtenerPostres(tipoDia);

            CargarCardsPlatillos(panel_meals, platillos);
            CargarCardsBebidas(panel_drinks, bebidas);
            CargarCardsPostres(panel_desserts, postres);
        }

        private void btn_desayuno_Click(object sender, EventArgs e) => CargarCategoria("desayuno");
        private void btn_almuerzo_Click(object sender, EventArgs e) => CargarCategoria("almuerzo");
        private void btn_comida_Click(object sender, EventArgs e) => CargarCategoria("comida");
        private void btn_cena_Click(object sender, EventArgs e) => CargarCategoria("cena");

        private void btn_administrar_Click(object sender, EventArgs e)
        {
            // aquí irá el form de administración
        }

        // ══════════════════════════════════════════════════════════
        //  EVENTOS PAINT
        // ══════════════════════════════════════════════════════════
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel_fondo_Paint(object sender, PaintEventArgs e) { }
        private void panel_meals_Paint(object sender, PaintEventArgs e) { }
        private void panel_drinks_Paint(object sender, PaintEventArgs e) { }
        private void panel_desserts_Paint(object sender, PaintEventArgs e) { }

        // ══════════════════════════════════════════════════════════
        //  CARGA INICIAL
        // ══════════════════════════════════════════════════════════
        private void Form1_Load(object sender, EventArgs e)
        {
            settings(sender, e);
            CargarCategoria("desayuno");
        }
    }
}