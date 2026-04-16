namespace restaurante
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
            this.panel_fondo = new System.Windows.Forms.Panel();
            this.panel_container = new System.Windows.Forms.Panel();
            this.btn_cena = new System.Windows.Forms.Button();
            this.btn_comida = new System.Windows.Forms.Button();
            this.btn_almuerzo = new System.Windows.Forms.Button();
            this.btn_desayuno = new System.Windows.Forms.Button();
            this.title_lbl = new System.Windows.Forms.Label();
            this.btn_administrar = new System.Windows.Forms.Button();
            this.panel_fondo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_fondo
            // 
            this.panel_fondo.BackgroundImage = global::restaurante.Properties.Resources.fondo_menu;
            this.panel_fondo.Controls.Add(this.btn_administrar);
            this.panel_fondo.Controls.Add(this.panel_container);
            this.panel_fondo.Controls.Add(this.btn_cena);
            this.panel_fondo.Controls.Add(this.btn_comida);
            this.panel_fondo.Controls.Add(this.btn_almuerzo);
            this.panel_fondo.Controls.Add(this.btn_desayuno);
            this.panel_fondo.Controls.Add(this.title_lbl);
            this.panel_fondo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_fondo.Location = new System.Drawing.Point(0, 0);
            this.panel_fondo.Name = "panel_fondo";
            this.panel_fondo.Size = new System.Drawing.Size(1192, 857);
            this.panel_fondo.TabIndex = 0;
            this.panel_fondo.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_fondo_Paint);
            // 
            // panel_container
            // 
            this.panel_container.AutoScroll = true;
            this.panel_container.BackColor = System.Drawing.Color.White;
            this.panel_container.BackgroundImage = global::restaurante.Properties.Resources.fondo_foods;
            this.panel_container.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_container.Location = new System.Drawing.Point(30, 195);
            this.panel_container.Name = "panel_container";
            this.panel_container.Size = new System.Drawing.Size(1100, 564);
            this.panel_container.TabIndex = 9;
            this.panel_container.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btn_cena
            // 
            this.btn_cena.BackColor = System.Drawing.Color.Red;
            this.btn_cena.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cena.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_cena.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cena.ForeColor = System.Drawing.Color.White;
            this.btn_cena.Location = new System.Drawing.Point(872, 128);
            this.btn_cena.Name = "btn_cena";
            this.btn_cena.Size = new System.Drawing.Size(177, 42);
            this.btn_cena.TabIndex = 8;
            this.btn_cena.Text = "CENA";
            this.btn_cena.UseVisualStyleBackColor = false;
            this.btn_cena.Click += new System.EventHandler(this.btn_cena_Click);
            // 
            // btn_comida
            // 
            this.btn_comida.BackColor = System.Drawing.Color.White;
            this.btn_comida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_comida.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_comida.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_comida.Location = new System.Drawing.Point(637, 128);
            this.btn_comida.Name = "btn_comida";
            this.btn_comida.Size = new System.Drawing.Size(177, 42);
            this.btn_comida.TabIndex = 7;
            this.btn_comida.Text = "COMIDA";
            this.btn_comida.UseVisualStyleBackColor = false;
            this.btn_comida.Click += new System.EventHandler(this.btn_comida_Click);
            // 
            // btn_almuerzo
            // 
            this.btn_almuerzo.BackColor = System.Drawing.Color.White;
            this.btn_almuerzo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_almuerzo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_almuerzo.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_almuerzo.Location = new System.Drawing.Point(376, 128);
            this.btn_almuerzo.Name = "btn_almuerzo";
            this.btn_almuerzo.Size = new System.Drawing.Size(177, 42);
            this.btn_almuerzo.TabIndex = 6;
            this.btn_almuerzo.Text = "ALMUERZO";
            this.btn_almuerzo.UseVisualStyleBackColor = false;
            this.btn_almuerzo.Click += new System.EventHandler(this.btn_almuerzo_Click);
            // 
            // btn_desayuno
            // 
            this.btn_desayuno.BackColor = System.Drawing.Color.Green;
            this.btn_desayuno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_desayuno.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_desayuno.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_desayuno.ForeColor = System.Drawing.Color.White;
            this.btn_desayuno.Location = new System.Drawing.Point(116, 128);
            this.btn_desayuno.Name = "btn_desayuno";
            this.btn_desayuno.Size = new System.Drawing.Size(177, 42);
            this.btn_desayuno.TabIndex = 1;
            this.btn_desayuno.Text = "DESAYUNO";
            this.btn_desayuno.UseVisualStyleBackColor = false;
            this.btn_desayuno.Click += new System.EventHandler(this.btn_desayuno_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.AutoSize = true;
            this.title_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.title_lbl.Font = new System.Drawing.Font("Mistral", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.ForeColor = System.Drawing.Color.Red;
            this.title_lbl.Image = global::restaurante.Properties.Resources.fondo_menu;
            this.title_lbl.Location = new System.Drawing.Point(322, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(525, 95);
            this.title_lbl.TabIndex = 0;
            this.title_lbl.Text = "MENU MEXICANO";
            // 
            // btn_administrar
            // 
            this.btn_administrar.BackColor = System.Drawing.Color.White;
            this.btn_administrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_administrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_administrar.Font = new System.Drawing.Font("Mistral", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_administrar.Location = new System.Drawing.Point(953, 803);
            this.btn_administrar.Name = "btn_administrar";
            this.btn_administrar.Size = new System.Drawing.Size(177, 42);
            this.btn_administrar.TabIndex = 10;
            this.btn_administrar.Text = "ADMINISTRAR";
            this.btn_administrar.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 857);
            this.Controls.Add(this.panel_fondo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_fondo.ResumeLayout(false);
            this.panel_fondo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_fondo;
        private System.Windows.Forms.Label title_lbl;
        private System.Windows.Forms.Button btn_desayuno;
        private System.Windows.Forms.Button btn_cena;
        private System.Windows.Forms.Button btn_comida;
        private System.Windows.Forms.Button btn_almuerzo;
        private System.Windows.Forms.Panel panel_container;
        private System.Windows.Forms.Button btn_administrar;
    }
}

