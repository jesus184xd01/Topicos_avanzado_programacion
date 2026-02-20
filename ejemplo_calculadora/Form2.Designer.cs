namespace ejemplo_calculadora
{
    partial class Form2
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
            this.btn_library = new System.Windows.Forms.Button();
            this.btn_calculadora = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_library
            // 
            this.btn_library.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_library.Location = new System.Drawing.Point(212, 258);
            this.btn_library.Name = "btn_library";
            this.btn_library.Size = new System.Drawing.Size(337, 81);
            this.btn_library.TabIndex = 1;
            this.btn_library.Text = "button2";
            this.btn_library.UseVisualStyleBackColor = true;
            this.btn_library.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_calculadora
            // 
            this.btn_calculadora.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_calculadora.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_calculadora.Location = new System.Drawing.Point(212, 171);
            this.btn_calculadora.Name = "btn_calculadora";
            this.btn_calculadora.Size = new System.Drawing.Size(337, 81);
            this.btn_calculadora.TabIndex = 0;
            this.btn_calculadora.Text = "button1";
            this.btn_calculadora.UseVisualStyleBackColor = false;
            this.btn_calculadora.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ejemplo_calculadora.Properties.Resources.Logo_TecNMactual;
            this.pictureBox1.Location = new System.Drawing.Point(-7, 422);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(367, 140);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_library);
            this.Controls.Add(this.btn_calculadora);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_library;
        private System.Windows.Forms.Button btn_calculadora;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}