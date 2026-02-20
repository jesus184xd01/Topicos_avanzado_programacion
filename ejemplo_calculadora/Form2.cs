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
            btn_calculadora.Text = "CALCULADORA CIENTÍFICA";
            btn_calculadora.Size = tamanoBoton;
            btn_calculadora.BackColor = azulItesi;
            btn_calculadora.ForeColor = blancoPuro;
            btn_calculadora.FlatStyle = FlatStyle.Flat;
            btn_calculadora.FlatAppearance.BorderSize = 0;
            btn_calculadora.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn_calculadora.Cursor = Cursors.Hand;

            // Botón 2 (Estilo Secundario)
            btn_library.Text = "GESTIÓN DE LIBRERÍAS";
            btn_library.Size = tamanoBoton;
            btn_library.BackColor = Color.FromArgb(45, 85, 140); // Un azul más claro
            btn_library.ForeColor = blancoPuro;
            btn_library.FlatStyle = FlatStyle.Flat;
            btn_library.FlatAppearance.BorderSize = 0;
            btn_library.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn_library.Cursor = Cursors.Hand;

            // 5. Posicionamiento Central
            int centroX = (this.ClientSize.Width - btn_calculadora.Width) / 2;
            btn_calculadora.Location = new Point(centroX, 220);
            btn_library.Location = new Point(centroX, 300);

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

        private void button1_Click(object sender, EventArgs e) //boton que redirije a la calculadora
        {
            Form1 formcalculadora = new Form1();
            formcalculadora.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e) //boton que redirije a la biblioteca
        {
            Form_vectores formbiblioteca = new Form_vectores();
            formbiblioteca.Show();
            this.Hide();
        }
        private void Form2_Load(object sender, EventArgs e) { }
    }
}