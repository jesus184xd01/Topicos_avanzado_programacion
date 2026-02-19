using System;
using System.Drawing;
using System.Windows.Forms;

namespace ejemplo_calculadora
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            ConfigurarDisenoItesi();
        }

        private void ConfigurarDisenoItesi()
        {
            Color azulItesi = Color.FromArgb(28, 63, 108);
            Color blancoPuro = Color.White;
            Color grisSuave = Color.FromArgb(240, 240, 240);
            
            this.Text = "Menu";
            this.BackColor = blancoPuro;
            this.Size = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Bordes más finos
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel panelHeader = new Panel();
            panelHeader.BackColor = azulItesi;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 100;
            this.Controls.Add(panelHeader);
        
            if (pictureBox1 != null)
            {
                //pictureBox1.Parent = panelHeader;
                //pictureBox1.Location = new Point(20, 20);
                pictureBox1.BackColor = Color.Transparent;
                pictureBox1.Size = new Size(250, 80);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            // 3. Título del Menú
            Label lblTitulo = new Label();
            lblTitulo.Text = "Menú principal";
            lblTitulo.ForeColor = blancoPuro;
            lblTitulo.Font = new Font("Segoe UI Semibold", 18);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(500, 35); // Ajustado a la derecha
            panelHeader.Controls.Add(lblTitulo);

            // 4. Estilo de los Botones
            Size tamanoBoton = new Size(300, 60);

            // Botón 1 (Estilo Principal)
            button1.Text = "CALCULADORA CIENTÍFICA";
            button1.Size = tamanoBoton;
            button1.BackColor = azulItesi;
            button1.ForeColor = blancoPuro;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            button1.Cursor = Cursors.Hand;

            // Botón 2 (Estilo Secundario)
            button2.Text = "GESTIÓN DE LIBRERÍAS";
            button2.Size = tamanoBoton;
            button2.BackColor = Color.FromArgb(45, 85, 140); // Un azul más claro
            button2.ForeColor = blancoPuro;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            button2.Cursor = Cursors.Hand;

            // 5. Posicionamiento Central
            int centroX = (this.ClientSize.Width - button1.Width) / 2;
            button1.Location = new Point(centroX, 220);
            button2.Location = new Point(centroX, 300);

            // 6. Decoración: Barra inferior (Footer)
            Panel panelFooter = new Panel();
            panelFooter.BackColor = Color.FromArgb(220, 220, 220);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Height = 30;
            this.Controls.Add(panelFooter);

            Label lblCopy = new Label();
            lblCopy.Text = "© 2026 ITESI - Ingeniería en Sistemas Computacionales";
            lblCopy.Font = new Font("Segoe UI", 8);
            lblCopy.ForeColor = Color.DimGray;
            lblCopy.AutoSize = true;
            lblCopy.Location = new Point(10, 8);
            panelFooter.Controls.Add(lblCopy);
        }

        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void Form2_Load(object sender, EventArgs e) { }
    }
}