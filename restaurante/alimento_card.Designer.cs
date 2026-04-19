namespace restaurante
{
    partial class alimento_card
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.precio_lbl = new System.Windows.Forms.Label();
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.lbl_precio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_descripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.img_icono = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_icono)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::restaurante.Properties.Resources.fondo_food_card;
            this.panel1.Controls.Add(this.precio_lbl);
            this.panel1.Controls.Add(this.lbl_nombre);
            this.panel1.Controls.Add(this.lbl_precio);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_descripcion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.img_icono);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 346);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // precio_lbl
            // 
            this.precio_lbl.AutoSize = true;
            this.precio_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precio_lbl.ForeColor = System.Drawing.Color.White;
            this.precio_lbl.Image = global::restaurante.Properties.Resources.fondo_food_card;
            this.precio_lbl.Location = new System.Drawing.Point(406, 179);
            this.precio_lbl.Name = "precio_lbl";
            this.precio_lbl.Size = new System.Drawing.Size(0, 38);
            this.precio_lbl.TabIndex = 6;
            // 
            // lbl_nombre
            // 
            this.lbl_nombre.AutoSize = true;
            this.lbl_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nombre.ForeColor = System.Drawing.Color.White;
            this.lbl_nombre.Image = global::restaurante.Properties.Resources.fondo_food_card;
            this.lbl_nombre.Location = new System.Drawing.Point(308, 19);
            this.lbl_nombre.Name = "lbl_nombre";
            this.lbl_nombre.Size = new System.Drawing.Size(98, 32);
            this.lbl_nombre.TabIndex = 5;
            this.lbl_nombre.Text = "label3";
            // 
            // lbl_precio
            // 
            this.lbl_precio.AutoSize = true;
            this.lbl_precio.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_precio.ForeColor = System.Drawing.Color.White;
            this.lbl_precio.Image = global::restaurante.Properties.Resources.fondo_food_card;
            this.lbl_precio.Location = new System.Drawing.Point(459, 179);
            this.lbl_precio.Name = "lbl_precio";
            this.lbl_precio.Size = new System.Drawing.Size(0, 38);
            this.lbl_precio.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Image = global::restaurante.Properties.Resources.fondo_food_card;
            this.label2.Location = new System.Drawing.Point(356, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "$";
            // 
            // txt_descripcion
            // 
            this.txt_descripcion.BackColor = System.Drawing.Color.Black;
            this.txt_descripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_descripcion.Enabled = false;
            this.txt_descripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_descripcion.ForeColor = System.Drawing.Color.White;
            this.txt_descripcion.Location = new System.Drawing.Point(302, 68);
            this.txt_descripcion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_descripcion.Multiline = true;
            this.txt_descripcion.Name = "txt_descripcion";
            this.txt_descripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_descripcion.Size = new System.Drawing.Size(230, 100);
            this.txt_descripcion.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::restaurante.Properties.Resources.fondo_food_card;
            this.label1.Location = new System.Drawing.Point(308, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 39);
            this.label1.TabIndex = 1;
            // 
            // img_icono
            // 
            this.img_icono.Location = new System.Drawing.Point(14, 15);
            this.img_icono.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.img_icono.Name = "img_icono";
            this.img_icono.Size = new System.Drawing.Size(263, 200);
            this.img_icono.TabIndex = 0;
            this.img_icono.TabStop = false;
            // 
            // alimento_card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 230);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "alimento_card";
            this.Text = "alimento_card";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_icono)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_descripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox img_icono;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_precio;
        private System.Windows.Forms.Label lbl_nombre;
        private System.Windows.Forms.Label precio_lbl;
    }
}