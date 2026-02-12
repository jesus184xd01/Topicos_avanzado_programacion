using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ejemplo_calculadora
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settings_screen();
            panel_settings();
            icons_btn();
        }

        private void settings_screen()
        {
            this.Size = new Size(330,520);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void panel_settings()
        {
            panel1.Left = (this.ClientRectangle.Width - panel1.Width) / 2;
            panel1.Top = 10;
        }

        private void icons_btn()
        {
            btn_pi.Text = "π";
            btn_raiz.Text = "√";
            btn_divid.Text = "÷";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public bool press = false;
        private void btn_shift_Click(object sender, EventArgs e)
        {
            if (!press)
            {
                press = true;
                btn_sen.Text = "sen-1";
                btn_cos.Text = "cos-1";
                btn_tan.Text = "tan-1";
            }
            else
            {
                press = false;
                btn_sen.Text = "SEN";
                btn_cos.Text = "COS";
                btn_tan.Text = "TAN";
            }

        }
    }
}
