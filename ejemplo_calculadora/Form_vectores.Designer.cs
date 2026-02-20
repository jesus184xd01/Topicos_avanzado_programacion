namespace ejemplo_calculadora
{
    partial class Form_vectores
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
            this.lbl_main = new System.Windows.Forms.Label();
            this.btn_menuform = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_main
            // 
            this.lbl_main.AutoSize = true;
            this.lbl_main.Location = new System.Drawing.Point(44, 53);
            this.lbl_main.Name = "lbl_main";
            this.lbl_main.Size = new System.Drawing.Size(33, 16);
            this.lbl_main.TabIndex = 0;
            this.lbl_main.Text = "text: ";
            // 
            // btn_menuform
            // 
            this.btn_menuform.Location = new System.Drawing.Point(47, 326);
            this.btn_menuform.Name = "btn_menuform";
            this.btn_menuform.Size = new System.Drawing.Size(131, 45);
            this.btn_menuform.TabIndex = 0;
            this.btn_menuform.Text = "Volver ";
            this.btn_menuform.UseVisualStyleBackColor = true;
            this.btn_menuform.Click += new System.EventHandler(this.btn_menuform_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(399, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_vectores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_menuform);
            this.Controls.Add(this.lbl_main);
            this.Name = "Form_vectores";
            this.Text = "Form_vectores";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_main;
        private System.Windows.Forms.Button btn_menuform;
        private System.Windows.Forms.Button button1;
    }
}