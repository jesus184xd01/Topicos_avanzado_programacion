namespace ejemplo_calculadora
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_parentesis_cierra = new System.Windows.Forms.Button();
            this.btn_parentesis_abre = new System.Windows.Forms.Button();
            this.btn_off = new System.Windows.Forms.Button();
            this.txt_screen = new System.Windows.Forms.TextBox();
            this.btn_tan = new System.Windows.Forms.Button();
            this.btn_cos = new System.Windows.Forms.Button();
            this.btn_sen = new System.Windows.Forms.Button();
            this.btn_raiz = new System.Windows.Forms.Button();
            this.btn_pi = new System.Windows.Forms.Button();
            this.btn_ac = new System.Windows.Forms.Button();
            this.btn_shift = new System.Windows.Forms.Button();
            this.btn_exp = new System.Windows.Forms.Button();
            this.btn_point = new System.Windows.Forms.Button();
            this.btn__equal = new System.Windows.Forms.Button();
            this.btn_less = new System.Windows.Forms.Button();
            this.btn_divid = new System.Windows.Forms.Button();
            this.btn_plus = new System.Windows.Forms.Button();
            this.btn_multiply = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_0 = new System.Windows.Forms.Button();
            this.btn_9 = new System.Windows.Forms.Button();
            this.btn_8 = new System.Windows.Forms.Button();
            this.btn_7 = new System.Windows.Forms.Button();
            this.btn_6 = new System.Windows.Forms.Button();
            this.btn_5 = new System.Windows.Forms.Button();
            this.btn_4 = new System.Windows.Forms.Button();
            this.btn_3 = new System.Windows.Forms.Button();
            this.btn_2 = new System.Windows.Forms.Button();
            this.btn_1 = new System.Windows.Forms.Button();
            this.btn_rad = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btn_rad);
            this.panel1.Controls.Add(this.btn_parentesis_cierra);
            this.panel1.Controls.Add(this.btn_parentesis_abre);
            this.panel1.Controls.Add(this.btn_off);
            this.panel1.Controls.Add(this.txt_screen);
            this.panel1.Controls.Add(this.btn_tan);
            this.panel1.Controls.Add(this.btn_cos);
            this.panel1.Controls.Add(this.btn_sen);
            this.panel1.Controls.Add(this.btn_raiz);
            this.panel1.Controls.Add(this.btn_pi);
            this.panel1.Controls.Add(this.btn_ac);
            this.panel1.Controls.Add(this.btn_shift);
            this.panel1.Controls.Add(this.btn_exp);
            this.panel1.Controls.Add(this.btn_point);
            this.panel1.Controls.Add(this.btn__equal);
            this.panel1.Controls.Add(this.btn_less);
            this.panel1.Controls.Add(this.btn_divid);
            this.panel1.Controls.Add(this.btn_plus);
            this.panel1.Controls.Add(this.btn_multiply);
            this.panel1.Controls.Add(this.btn_del);
            this.panel1.Controls.Add(this.btn_0);
            this.panel1.Controls.Add(this.btn_9);
            this.panel1.Controls.Add(this.btn_8);
            this.panel1.Controls.Add(this.btn_7);
            this.panel1.Controls.Add(this.btn_6);
            this.panel1.Controls.Add(this.btn_5);
            this.panel1.Controls.Add(this.btn_4);
            this.panel1.Controls.Add(this.btn_3);
            this.panel1.Controls.Add(this.btn_2);
            this.panel1.Controls.Add(this.btn_1);
            this.panel1.Location = new System.Drawing.Point(12, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 542);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btn_parentesis_cierra
            // 
            this.btn_parentesis_cierra.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_parentesis_cierra.Location = new System.Drawing.Point(88, 452);
            this.btn_parentesis_cierra.Name = "btn_parentesis_cierra";
            this.btn_parentesis_cierra.Size = new System.Drawing.Size(58, 61);
            this.btn_parentesis_cierra.TabIndex = 28;
            this.btn_parentesis_cierra.Text = ")";
            this.btn_parentesis_cierra.UseVisualStyleBackColor = false;
            this.btn_parentesis_cierra.Click += new System.EventHandler(this.btn_parentesis_cierra_Click);
            // 
            // btn_parentesis_abre
            // 
            this.btn_parentesis_abre.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_parentesis_abre.Location = new System.Drawing.Point(15, 452);
            this.btn_parentesis_abre.Name = "btn_parentesis_abre";
            this.btn_parentesis_abre.Size = new System.Drawing.Size(58, 61);
            this.btn_parentesis_abre.TabIndex = 27;
            this.btn_parentesis_abre.Text = "(";
            this.btn_parentesis_abre.UseVisualStyleBackColor = false;
            this.btn_parentesis_abre.Click += new System.EventHandler(this.btn_parentesis_abre_Click);
            // 
            // btn_off
            // 
            this.btn_off.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_off.Location = new System.Drawing.Point(88, 102);
            this.btn_off.Name = "btn_off";
            this.btn_off.Size = new System.Drawing.Size(58, 61);
            this.btn_off.TabIndex = 26;
            this.btn_off.Text = "off";
            this.btn_off.UseVisualStyleBackColor = false;
            this.btn_off.Click += new System.EventHandler(this.btn_off_Click);
            // 
            // txt_screen
            // 
            this.txt_screen.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_screen.Location = new System.Drawing.Point(15, 25);
            this.txt_screen.Name = "txt_screen";
            this.txt_screen.Size = new System.Drawing.Size(336, 49);
            this.txt_screen.TabIndex = 25;
            // 
            // btn_tan
            // 
            this.btn_tan.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_tan.Location = new System.Drawing.Point(293, 102);
            this.btn_tan.Name = "btn_tan";
            this.btn_tan.Size = new System.Drawing.Size(58, 61);
            this.btn_tan.TabIndex = 24;
            this.btn_tan.Text = "TAN";
            this.btn_tan.UseVisualStyleBackColor = false;
            this.btn_tan.Click += new System.EventHandler(this.btn_tan_Click);
            // 
            // btn_cos
            // 
            this.btn_cos.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_cos.Location = new System.Drawing.Point(226, 102);
            this.btn_cos.Name = "btn_cos";
            this.btn_cos.Size = new System.Drawing.Size(58, 61);
            this.btn_cos.TabIndex = 23;
            this.btn_cos.Text = "COS";
            this.btn_cos.UseVisualStyleBackColor = false;
            this.btn_cos.Click += new System.EventHandler(this.btn_cos_Click);
            // 
            // btn_sen
            // 
            this.btn_sen.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_sen.Location = new System.Drawing.Point(159, 102);
            this.btn_sen.Name = "btn_sen";
            this.btn_sen.Size = new System.Drawing.Size(58, 61);
            this.btn_sen.TabIndex = 22;
            this.btn_sen.Text = "SEN";
            this.btn_sen.UseVisualStyleBackColor = false;
            this.btn_sen.Click += new System.EventHandler(this.btn_sen_Click);
            // 
            // btn_raiz
            // 
            this.btn_raiz.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_raiz.Location = new System.Drawing.Point(15, 381);
            this.btn_raiz.Name = "btn_raiz";
            this.btn_raiz.Size = new System.Drawing.Size(58, 61);
            this.btn_raiz.TabIndex = 21;
            this.btn_raiz.Text = "Raiz2";
            this.btn_raiz.UseVisualStyleBackColor = false;
            this.btn_raiz.Click += new System.EventHandler(this.btn_raiz_Click_1);
            // 
            // btn_pi
            // 
            this.btn_pi.BackColor = System.Drawing.Color.SandyBrown;
            this.btn_pi.Location = new System.Drawing.Point(293, 382);
            this.btn_pi.Name = "btn_pi";
            this.btn_pi.Size = new System.Drawing.Size(58, 61);
            this.btn_pi.TabIndex = 20;
            this.btn_pi.Text = "π";
            this.btn_pi.UseVisualStyleBackColor = false;
            this.btn_pi.Click += new System.EventHandler(this.btn_pi_Click);
            // 
            // btn_ac
            // 
            this.btn_ac.BackColor = System.Drawing.Color.Red;
            this.btn_ac.ForeColor = System.Drawing.Color.White;
            this.btn_ac.Location = new System.Drawing.Point(293, 172);
            this.btn_ac.Name = "btn_ac";
            this.btn_ac.Size = new System.Drawing.Size(58, 61);
            this.btn_ac.TabIndex = 19;
            this.btn_ac.Text = "AC";
            this.btn_ac.UseVisualStyleBackColor = false;
            this.btn_ac.Click += new System.EventHandler(this.btn_ac_Click);
            // 
            // btn_shift
            // 
            this.btn_shift.BackColor = System.Drawing.SystemColors.Info;
            this.btn_shift.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_shift.Location = new System.Drawing.Point(15, 102);
            this.btn_shift.Name = "btn_shift";
            this.btn_shift.Size = new System.Drawing.Size(67, 61);
            this.btn_shift.TabIndex = 18;
            this.btn_shift.Text = "SHIFT";
            this.btn_shift.UseVisualStyleBackColor = false;
            this.btn_shift.Click += new System.EventHandler(this.btn_shift_Click);
            // 
            // btn_exp
            // 
            this.btn_exp.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_exp.Location = new System.Drawing.Point(226, 382);
            this.btn_exp.Name = "btn_exp";
            this.btn_exp.Size = new System.Drawing.Size(58, 61);
            this.btn_exp.TabIndex = 17;
            this.btn_exp.Text = "^";
            this.btn_exp.UseVisualStyleBackColor = false;
            this.btn_exp.Click += new System.EventHandler(this.btn_exp_Click);
            // 
            // btn_point
            // 
            this.btn_point.BackColor = System.Drawing.Color.Silver;
            this.btn_point.Location = new System.Drawing.Point(159, 381);
            this.btn_point.Name = "btn_point";
            this.btn_point.Size = new System.Drawing.Size(58, 61);
            this.btn_point.TabIndex = 16;
            this.btn_point.Text = ".";
            this.btn_point.UseVisualStyleBackColor = false;
            this.btn_point.Click += new System.EventHandler(this.btn_point_Click);
            // 
            // btn__equal
            // 
            this.btn__equal.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn__equal.Location = new System.Drawing.Point(226, 452);
            this.btn__equal.Name = "btn__equal";
            this.btn__equal.Size = new System.Drawing.Size(125, 61);
            this.btn__equal.TabIndex = 15;
            this.btn__equal.Text = "=";
            this.btn__equal.UseVisualStyleBackColor = false;
            this.btn__equal.Click += new System.EventHandler(this.btn__equal_Click);
            // 
            // btn_less
            // 
            this.btn_less.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_less.Location = new System.Drawing.Point(293, 312);
            this.btn_less.Name = "btn_less";
            this.btn_less.Size = new System.Drawing.Size(58, 61);
            this.btn_less.TabIndex = 14;
            this.btn_less.Text = "-";
            this.btn_less.UseVisualStyleBackColor = false;
            this.btn_less.Click += new System.EventHandler(this.operacion_Click);
            // 
            // btn_divid
            // 
            this.btn_divid.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_divid.Location = new System.Drawing.Point(293, 242);
            this.btn_divid.Name = "btn_divid";
            this.btn_divid.Size = new System.Drawing.Size(58, 61);
            this.btn_divid.TabIndex = 13;
            this.btn_divid.Text = "/";
            this.btn_divid.UseVisualStyleBackColor = false;
            this.btn_divid.Click += new System.EventHandler(this.operacion_Click);
            // 
            // btn_plus
            // 
            this.btn_plus.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_plus.Location = new System.Drawing.Point(226, 312);
            this.btn_plus.Name = "btn_plus";
            this.btn_plus.Size = new System.Drawing.Size(58, 61);
            this.btn_plus.TabIndex = 12;
            this.btn_plus.Text = "+";
            this.btn_plus.UseVisualStyleBackColor = false;
            this.btn_plus.Click += new System.EventHandler(this.operacion_Click);
            // 
            // btn_multiply
            // 
            this.btn_multiply.BackColor = System.Drawing.Color.PeachPuff;
            this.btn_multiply.Location = new System.Drawing.Point(226, 242);
            this.btn_multiply.Name = "btn_multiply";
            this.btn_multiply.Size = new System.Drawing.Size(58, 61);
            this.btn_multiply.TabIndex = 11;
            this.btn_multiply.Text = "X";
            this.btn_multiply.UseVisualStyleBackColor = false;
            this.btn_multiply.Click += new System.EventHandler(this.operacion_Click);
            // 
            // btn_del
            // 
            this.btn_del.BackColor = System.Drawing.Color.DarkRed;
            this.btn_del.ForeColor = System.Drawing.Color.White;
            this.btn_del.Location = new System.Drawing.Point(226, 172);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(58, 61);
            this.btn_del.TabIndex = 10;
            this.btn_del.Text = "DEL";
            this.btn_del.UseVisualStyleBackColor = false;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_0
            // 
            this.btn_0.BackColor = System.Drawing.Color.Silver;
            this.btn_0.Location = new System.Drawing.Point(88, 381);
            this.btn_0.Name = "btn_0";
            this.btn_0.Size = new System.Drawing.Size(58, 61);
            this.btn_0.TabIndex = 9;
            this.btn_0.Text = "0";
            this.btn_0.UseVisualStyleBackColor = false;
            this.btn_0.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_9
            // 
            this.btn_9.BackColor = System.Drawing.Color.Silver;
            this.btn_9.Location = new System.Drawing.Point(159, 311);
            this.btn_9.Name = "btn_9";
            this.btn_9.Size = new System.Drawing.Size(58, 61);
            this.btn_9.TabIndex = 8;
            this.btn_9.Text = "9";
            this.btn_9.UseVisualStyleBackColor = false;
            this.btn_9.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_8
            // 
            this.btn_8.BackColor = System.Drawing.Color.Silver;
            this.btn_8.Location = new System.Drawing.Point(88, 311);
            this.btn_8.Name = "btn_8";
            this.btn_8.Size = new System.Drawing.Size(58, 61);
            this.btn_8.TabIndex = 7;
            this.btn_8.Text = "8";
            this.btn_8.UseVisualStyleBackColor = false;
            this.btn_8.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_7
            // 
            this.btn_7.BackColor = System.Drawing.Color.Silver;
            this.btn_7.Location = new System.Drawing.Point(15, 311);
            this.btn_7.Name = "btn_7";
            this.btn_7.Size = new System.Drawing.Size(58, 61);
            this.btn_7.TabIndex = 6;
            this.btn_7.Text = "7";
            this.btn_7.UseVisualStyleBackColor = false;
            this.btn_7.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_6
            // 
            this.btn_6.BackColor = System.Drawing.Color.Silver;
            this.btn_6.Location = new System.Drawing.Point(159, 241);
            this.btn_6.Name = "btn_6";
            this.btn_6.Size = new System.Drawing.Size(58, 61);
            this.btn_6.TabIndex = 5;
            this.btn_6.Text = "6";
            this.btn_6.UseVisualStyleBackColor = false;
            this.btn_6.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_5
            // 
            this.btn_5.BackColor = System.Drawing.Color.Silver;
            this.btn_5.Location = new System.Drawing.Point(88, 241);
            this.btn_5.Name = "btn_5";
            this.btn_5.Size = new System.Drawing.Size(58, 61);
            this.btn_5.TabIndex = 4;
            this.btn_5.Text = "5";
            this.btn_5.UseVisualStyleBackColor = false;
            this.btn_5.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_4
            // 
            this.btn_4.BackColor = System.Drawing.Color.Silver;
            this.btn_4.Location = new System.Drawing.Point(15, 241);
            this.btn_4.Name = "btn_4";
            this.btn_4.Size = new System.Drawing.Size(58, 61);
            this.btn_4.TabIndex = 3;
            this.btn_4.Text = "4";
            this.btn_4.UseVisualStyleBackColor = false;
            this.btn_4.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_3
            // 
            this.btn_3.BackColor = System.Drawing.Color.Silver;
            this.btn_3.Location = new System.Drawing.Point(159, 172);
            this.btn_3.Name = "btn_3";
            this.btn_3.Size = new System.Drawing.Size(58, 61);
            this.btn_3.TabIndex = 2;
            this.btn_3.Text = "3";
            this.btn_3.UseVisualStyleBackColor = false;
            this.btn_3.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_2
            // 
            this.btn_2.BackColor = System.Drawing.Color.Silver;
            this.btn_2.Location = new System.Drawing.Point(88, 172);
            this.btn_2.Name = "btn_2";
            this.btn_2.Size = new System.Drawing.Size(58, 61);
            this.btn_2.TabIndex = 1;
            this.btn_2.Text = "2";
            this.btn_2.UseVisualStyleBackColor = false;
            this.btn_2.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_1
            // 
            this.btn_1.BackColor = System.Drawing.Color.Silver;
            this.btn_1.Location = new System.Drawing.Point(15, 172);
            this.btn_1.Name = "btn_1";
            this.btn_1.Size = new System.Drawing.Size(58, 61);
            this.btn_1.TabIndex = 0;
            this.btn_1.Text = "1";
            this.btn_1.UseVisualStyleBackColor = false;
            this.btn_1.Click += new System.EventHandler(this.numero_Click);
            // 
            // btn_rad
            // 
            this.btn_rad.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btn_rad.Location = new System.Drawing.Point(159, 452);
            this.btn_rad.Name = "btn_rad";
            this.btn_rad.Size = new System.Drawing.Size(58, 61);
            this.btn_rad.TabIndex = 29;
            this.btn_rad.Text = "Rad";
            this.btn_rad.UseVisualStyleBackColor = false;
            this.btn_rad.Click += new System.EventHandler(this.btn_rad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 709);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_0;
        private System.Windows.Forms.Button btn_9;
        private System.Windows.Forms.Button btn_8;
        private System.Windows.Forms.Button btn_7;
        private System.Windows.Forms.Button btn_6;
        private System.Windows.Forms.Button btn_5;
        private System.Windows.Forms.Button btn_4;
        private System.Windows.Forms.Button btn_3;
        private System.Windows.Forms.Button btn_2;
        private System.Windows.Forms.Button btn_1;
        private System.Windows.Forms.Button btn_plus;
        private System.Windows.Forms.Button btn_multiply;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_ac;
        private System.Windows.Forms.Button btn_shift;
        private System.Windows.Forms.Button btn_exp;
        private System.Windows.Forms.Button btn_point;
        private System.Windows.Forms.Button btn__equal;
        private System.Windows.Forms.Button btn_less;
        private System.Windows.Forms.Button btn_divid;
        private System.Windows.Forms.Button btn_tan;
        private System.Windows.Forms.Button btn_cos;
        private System.Windows.Forms.Button btn_sen;
        private System.Windows.Forms.Button btn_raiz;
        private System.Windows.Forms.Button btn_pi;
        private System.Windows.Forms.TextBox txt_screen;
        private System.Windows.Forms.Button btn_off;
        private System.Windows.Forms.Button btn_parentesis_cierra;
        private System.Windows.Forms.Button btn_parentesis_abre;
        private System.Windows.Forms.Button btn_rad;
    }
}

