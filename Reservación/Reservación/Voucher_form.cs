using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing; 
using System.Windows.Forms;

namespace Reservación
{
    public partial class Voucher_form : Form
    {
        private Reservation reservacion;
        private Panel panelTicket;

        private readonly Color colorPapel = Color.White;
        private readonly Font fontHeader = new Font("Courier New", 14, FontStyle.Bold);
        private readonly Font fontSubHeader = new Font("Consolas", 10, FontStyle.Bold);
        private readonly Font fontBody = new Font("Consolas", 9, FontStyle.Regular);
        private readonly Font fontTotal = new Font("Consolas", 16, FontStyle.Bold);
        private readonly Font fontBarcode = new Font("Arial", 20, FontStyle.Regular);
        private readonly Font fontSeparator = new Font("Courier New", 10, FontStyle.Regular);

        public Voucher_form(Reservation reserva)
        {
            InitializeComponent();
            this.reservacion = reserva;
        }

        private void Voucher_form_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.Text = "Recibo de Reservación";
            this.Size = new Size(500, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            panelTicket = new Panel();
            panelTicket.Width = 360;
            panelTicket.Height = 650;
            panelTicket.BackColor = colorPapel;
            panelTicket.Location = new Point((this.ClientSize.Width - panelTicket.Width) / 2, 20);
            panelTicket.Paint += PanelTicket_Paint;
            this.Controls.Add(panelTicket);

            Button btnImprimir = new Button();
            btnImprimir.Text = "IMPRIMIR";
            btnImprimir.Size = new Size(200, 50);
            btnImprimir.Location = new Point((this.ClientSize.Width - 200) / 2, panelTicket.Bottom + 20);
            btnImprimir.BackColor = Color.FromArgb(0, 122, 204);
            btnImprimir.ForeColor = Color.White;
            btnImprimir.FlatStyle = FlatStyle.Flat;
            btnImprimir.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnImprimir.Cursor = Cursors.Hand;
            btnImprimir.Click += BtnImprimir_Click;
            this.Controls.Add(btnImprimir);

            string nombreCliente = reservacion.Client != null ? reservacion.Client.ToString() : "Invitado";
            string alojamiento = reservacion.housing_name;
            string anfitrion = reservacion.host;
            string fechaCheckIn = reservacion.checkIn.ToString("dd/MM/yyyy");
            string fechaCheckOut = reservacion.checkOut.ToString("dd/MM/yyyy");
            string noches = reservacion.nights.ToString();
            string huespedes = reservacion.guests.ToString();
            string totalPagar = "$" + reservacion.total.ToString("N2") + " MXN";
            string fechaEmision = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string folio = "FOL-" + new Random().Next(1000, 9999).ToString();

            int yPos = 20;

            Label lblApp = CreateLabel("Airbnb", fontHeader, panelTicket.Width, yPos, true);
            panelTicket.Controls.Add(lblApp);
            yPos += 25;

            Label lblSlogan = CreateLabel("¡Tu hogar, en cualquier rincón del mundo!", fontBody, panelTicket.Width, yPos, true);
            panelTicket.Controls.Add(lblSlogan);
            yPos += 30;

            AddItem(panelTicket, "FECHA:", fechaEmision, ref yPos);
            AddItem(panelTicket, "FOLIO:", folio, ref yPos);

            AddSeparator(panelTicket, ref yPos);

            Label lblTituloAlojamiento = CreateLabel(alojamiento, fontSubHeader, panelTicket.Width, yPos, true);
            panelTicket.Controls.Add(lblTituloAlojamiento);
            yPos += 20;

            Label lblAnfitrion = CreateLabel("Anfitrión: " + anfitrion, fontBody, panelTicket.Width, yPos, true);
            panelTicket.Controls.Add(lblAnfitrion);
            yPos += 25;

            AddSeparator(panelTicket, ref yPos);

            AddItem(panelTicket, "PERSONAS:", huespedes, ref yPos);
            AddItem(panelTicket, "CHECK-IN:", fechaCheckIn, ref yPos);
            AddItem(panelTicket, "CHECK-OUT:", fechaCheckOut, ref yPos);
            AddItem(panelTicket, "ESTANCIA:", noches + " Noches", ref yPos);

            AddSeparator(panelTicket, ref yPos);

            Label lblConcepto = new Label { Text = "CONCEPTO", Font = fontSubHeader, Location = new Point(15, yPos), AutoSize = true };
            Label lblImporte = new Label { Text = "IMPORTE", Font = fontSubHeader, Location = new Point(panelTicket.Width - 80, yPos), AutoSize = true };
            panelTicket.Controls.Add(lblConcepto);
            panelTicket.Controls.Add(lblImporte);
            yPos += 20;

            string descripcionCobro = $"Renta x {noches} noches";
            AddItem(panelTicket, descripcionCobro, totalPagar, ref yPos);

            yPos += 20;

            AddSeparator(panelTicket, ref yPos);
            yPos += 10;

            Label lblTotalLabel = new Label { Text = "TOTAL:", Font = fontTotal, Location = new Point(20, yPos), AutoSize = true };
            Label lblTotalValue = new Label
            {
                Text = totalPagar,
                Font = fontTotal,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Width = 200,
                Location = new Point(panelTicket.Width - 220, yPos)
            };
            panelTicket.Controls.Add(lblTotalLabel);
            panelTicket.Controls.Add(lblTotalValue);

            yPos += 50;

            Label lblBarra = CreateLabel("||| ||| || ||||| || |||", fontBarcode, panelTicket.Width, yPos, true);
            panelTicket.Controls.Add(lblBarra);
            yPos += 30;

            Label lblGracias = CreateLabel("*** GRACIAS POR SU PREFERENCIA ***", fontBody, panelTicket.Width, yPos, true);
            panelTicket.Controls.Add(lblGracias);
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            pd.PrintPage += new PrintPageEventHandler(ImprimirTicket);

            PrintPreviewDialog pview = new PrintPreviewDialog();
            pview.Document = pd;

            ((Form)pview).WindowState = FormWindowState.Maximized;

            pview.ShowDialog();
        }

        private void ImprimirTicket(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(panelTicket.Width, panelTicket.Height);
            panelTicket.DrawToBitmap(bmp, new Rectangle(0, 0, panelTicket.Width, panelTicket.Height));

            int x = (e.PageBounds.Width - bmp.Width) / 2;
            int y = 50;

            e.Graphics.DrawImage(bmp, x, y);
        }

        private Label CreateLabel(string text, Font font, int width, int y, bool center)
        {
            Label l = new Label();
            l.Text = text;
            l.Font = font;
            l.AutoSize = false;
            l.Width = width - 20;
            l.Height = (int)font.GetHeight() + 5;
            l.Location = new Point(10, y);
            l.TextAlign = center ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
            return l;
        }

        private void AddItem(Panel p, string label, string value, ref int y)
        {
            Label l = new Label();
            l.Text = label;
            l.Font = fontSubHeader;
            l.AutoSize = true;
            l.Location = new Point(15, y);
            p.Controls.Add(l);

            Label v = new Label();
            v.Text = value;
            if (v.Text.Length > 25) v.Text = v.Text.Substring(0, 22) + "...";

            v.Font = fontBody;
            v.AutoSize = false;
            v.TextAlign = ContentAlignment.MiddleRight;
            v.Width = 180;
            v.Location = new Point(p.Width - 200, y);
            p.Controls.Add(v);

            y += 20;
        }

        private void AddSeparator(Panel p, ref int y)
        {
            Label l = new Label();
            l.Text = "--------------------------------------------------";
            l.Font = fontSeparator;
            l.AutoSize = false;
            l.Height = 15;
            l.Width = p.Width;
            l.TextAlign = ContentAlignment.TopCenter;
            l.Location = new Point(0, y);
            p.Controls.Add(l);

            y += 15;
        }

        private void PanelTicket_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int tamanoDiente = 10;
            GraphicsPath path = new GraphicsPath();

            path.AddLine(0, 0, p.Width, 0);
            path.AddLine(p.Width, 0, p.Width, p.Height - tamanoDiente);

            for (int x = p.Width; x > 0; x -= tamanoDiente)
            {
                path.AddLine(x, p.Height - tamanoDiente, x - (tamanoDiente / 2), p.Height);
                path.AddLine(x - (tamanoDiente / 2), p.Height, x - tamanoDiente, p.Height - tamanoDiente);
            }

            path.AddLine(0, p.Height - tamanoDiente, 0, 0);
            p.Region = new Region(path);
        }
    }
}