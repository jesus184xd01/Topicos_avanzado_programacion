using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Libreria_Vectores;


namespace ejemplo_calculadora
{
    public partial class Form_vectores : Form
    {
        public Form_vectores()
        {
            InitializeComponent();
        }

        private void btn_menuform_Click(object sender, EventArgs e)
        {
            Form2 formmenu = new Form2();
            formmenu.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Libreria_Vectores.Class1 vector = new Libreria_Vectores.Class1();
            int algo = vector.suma(10, 10);
            lbl_main.Text = algo.ToString();
        }
    }
}
