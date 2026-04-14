namespace HILO
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_no_iniciado = new System.Windows.Forms.Label();
            this.lbl_ejecutando = new System.Windows.Forms.Label();
            this.lbl_suspendido = new System.Windows.Forms.Label();
            this.lbl_detenido = new System.Windows.Forms.Label();
            this.lbl_estadoActual = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.btn_selectfolder = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_suspender = new System.Windows.Forms.Button();
            this.btn_reanudar = new System.Windows.Forms.Button();
            this.btn_detener = new System.Windows.Forms.Button();
            this.lbl_estado = new System.Windows.Forms.Label();
            this.lbl_estado_actual = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(241, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(526, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Galeria de imagenes - ESTADOS DE HILOS";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_detenido);
            this.panel1.Controls.Add(this.lbl_suspendido);
            this.panel1.Controls.Add(this.lbl_ejecutando);
            this.panel1.Controls.Add(this.lbl_no_iniciado);
            this.panel1.Location = new System.Drawing.Point(38, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 51);
            this.panel1.TabIndex = 1;
            // 
            // lbl_no_iniciado
            // 
            this.lbl_no_iniciado.AutoSize = true;
            this.lbl_no_iniciado.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_no_iniciado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_no_iniciado.Location = new System.Drawing.Point(12, 13);
            this.lbl_no_iniciado.Name = "lbl_no_iniciado";
            this.lbl_no_iniciado.Size = new System.Drawing.Size(167, 25);
            this.lbl_no_iniciado.TabIndex = 0;
            this.lbl_no_iniciado.Text = "    No iniciado    ";
            this.lbl_no_iniciado.Click += new System.EventHandler(this.lbl_no_iniciado_Click);
            // 
            // lbl_ejecutando
            // 
            this.lbl_ejecutando.AutoSize = true;
            this.lbl_ejecutando.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_ejecutando.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ejecutando.Location = new System.Drawing.Point(251, 13);
            this.lbl_ejecutando.Name = "lbl_ejecutando";
            this.lbl_ejecutando.Size = new System.Drawing.Size(168, 25);
            this.lbl_ejecutando.TabIndex = 1;
            this.lbl_ejecutando.Text = "    Ejecutando    ";
            this.lbl_ejecutando.Click += new System.EventHandler(this.lbl_ejecutando_Click);
            // 
            // lbl_suspendido
            // 
            this.lbl_suspendido.AutoSize = true;
            this.lbl_suspendido.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_suspendido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_suspendido.Location = new System.Drawing.Point(499, 13);
            this.lbl_suspendido.Name = "lbl_suspendido";
            this.lbl_suspendido.Size = new System.Drawing.Size(175, 25);
            this.lbl_suspendido.TabIndex = 2;
            this.lbl_suspendido.Text = "    Suspendido    ";
            this.lbl_suspendido.Click += new System.EventHandler(this.lbl_suspendido_Click);
            // 
            // lbl_detenido
            // 
            this.lbl_detenido.AutoSize = true;
            this.lbl_detenido.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_detenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_detenido.Location = new System.Drawing.Point(746, 13);
            this.lbl_detenido.Name = "lbl_detenido";
            this.lbl_detenido.Size = new System.Drawing.Size(158, 25);
            this.lbl_detenido.TabIndex = 3;
            this.lbl_detenido.Text = "    Detenido      ";
            this.lbl_detenido.Click += new System.EventHandler(this.lbl_detenido_Click);
            // 
            // lbl_estadoActual
            // 
            this.lbl_estadoActual.AutoSize = true;
            this.lbl_estadoActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_estadoActual.ForeColor = System.Drawing.Color.Red;
            this.lbl_estadoActual.Location = new System.Drawing.Point(39, 135);
            this.lbl_estadoActual.Name = "lbl_estadoActual";
            this.lbl_estadoActual.Size = new System.Drawing.Size(203, 20);
            this.lbl_estadoActual.TabIndex = 2;
            this.lbl_estadoActual.Text = "Estado actual del hilo: ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(38, 171);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(921, 22);
            this.progressBar1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.pictureBox6);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(38, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(921, 389);
            this.panel2.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(25, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(273, 157);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(321, 23);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(273, 157);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(620, 23);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(273, 157);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(25, 205);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(273, 157);
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(321, 205);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(273, 157);
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Location = new System.Drawing.Point(620, 205);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(273, 157);
            this.pictureBox6.TabIndex = 9;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // btn_selectfolder
            // 
            this.btn_selectfolder.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_selectfolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_selectfolder.Location = new System.Drawing.Point(72, 651);
            this.btn_selectfolder.Name = "btn_selectfolder";
            this.btn_selectfolder.Size = new System.Drawing.Size(154, 50);
            this.btn_selectfolder.TabIndex = 5;
            this.btn_selectfolder.Text = "Seleccionar carpeta\r\n";
            this.btn_selectfolder.UseVisualStyleBackColor = false;
            this.btn_selectfolder.Click += new System.EventHandler(this.btn_selectfolder_Click);
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.SpringGreen;
            this.btn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Location = new System.Drawing.Point(245, 651);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(154, 50);
            this.btn_start.TabIndex = 6;
            this.btn_start.Text = "Iniciar hilo";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_suspender
            // 
            this.btn_suspender.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_suspender.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_suspender.Location = new System.Drawing.Point(420, 651);
            this.btn_suspender.Name = "btn_suspender";
            this.btn_suspender.Size = new System.Drawing.Size(154, 50);
            this.btn_suspender.TabIndex = 7;
            this.btn_suspender.Text = "Suspender";
            this.btn_suspender.UseVisualStyleBackColor = false;
            this.btn_suspender.Click += new System.EventHandler(this.btn_suspender_Click);
            // 
            // btn_reanudar
            // 
            this.btn_reanudar.BackColor = System.Drawing.Color.MediumOrchid;
            this.btn_reanudar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reanudar.Location = new System.Drawing.Point(591, 651);
            this.btn_reanudar.Name = "btn_reanudar";
            this.btn_reanudar.Size = new System.Drawing.Size(154, 50);
            this.btn_reanudar.TabIndex = 8;
            this.btn_reanudar.Text = "Reanudar";
            this.btn_reanudar.UseVisualStyleBackColor = false;
            this.btn_reanudar.Click += new System.EventHandler(this.btn_reanudar_Click);
            // 
            // btn_detener
            // 
            this.btn_detener.BackColor = System.Drawing.Color.Crimson;
            this.btn_detener.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_detener.Location = new System.Drawing.Point(767, 651);
            this.btn_detener.Name = "btn_detener";
            this.btn_detener.Size = new System.Drawing.Size(154, 50);
            this.btn_detener.TabIndex = 9;
            this.btn_detener.Text = "Detener hilo";
            this.btn_detener.UseVisualStyleBackColor = false;
            this.btn_detener.Click += new System.EventHandler(this.btn_detener_Click);
            // 
            // lbl_estado
            // 
            this.lbl_estado.AutoSize = true;
            this.lbl_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_estado.ForeColor = System.Drawing.Color.Red;
            this.lbl_estado.Location = new System.Drawing.Point(241, 135);
            this.lbl_estado.Name = "lbl_estado";
            this.lbl_estado.Size = new System.Drawing.Size(0, 20);
            this.lbl_estado.TabIndex = 10;
            // 
            // lbl_estado_actual
            // 
            this.lbl_estado_actual.AutoSize = true;
            this.lbl_estado_actual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_estado_actual.ForeColor = System.Drawing.Color.Red;
            this.lbl_estado_actual.Location = new System.Drawing.Point(261, 135);
            this.lbl_estado_actual.Name = "lbl_estado_actual";
            this.lbl_estado_actual.Size = new System.Drawing.Size(16, 20);
            this.lbl_estado_actual.TabIndex = 11;
            this.lbl_estado_actual.Text = "-\r\n";
            this.lbl_estado_actual.Click += new System.EventHandler(this.lbl_estado_actual_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 713);
            this.Controls.Add(this.lbl_estado_actual);
            this.Controls.Add(this.lbl_estado);
            this.Controls.Add(this.btn_detener);
            this.Controls.Add(this.btn_reanudar);
            this.Controls.Add(this.btn_suspender);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_selectfolder);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbl_estadoActual);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_detenido;
        private System.Windows.Forms.Label lbl_suspendido;
        private System.Windows.Forms.Label lbl_ejecutando;
        private System.Windows.Forms.Label lbl_no_iniciado;
        private System.Windows.Forms.Label lbl_estadoActual;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btn_selectfolder;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_suspender;
        private System.Windows.Forms.Button btn_reanudar;
        private System.Windows.Forms.Button btn_detener;
        private System.Windows.Forms.Label lbl_estado;
        private System.Windows.Forms.Label lbl_estado_actual;
    }
}

