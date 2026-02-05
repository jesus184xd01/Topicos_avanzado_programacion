namespace Reservación
{
    partial class Log_in_form
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
            this.logo_img = new System.Windows.Forms.PictureBox();
            this.panel_container = new System.Windows.Forms.Panel();
            this.user_label = new System.Windows.Forms.Label();
            this.user_txt = new System.Windows.Forms.TextBox();
            this.email_txt = new System.Windows.Forms.TextBox();
            this.email_label = new System.Windows.Forms.Label();
            this.password_txt = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.login_btn = new System.Windows.Forms.Button();
            this.login_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo_img)).BeginInit();
            this.panel_container.SuspendLayout();
            this.SuspendLayout();
            // 
            // logo_img
            // 
            this.logo_img.Image = global::Reservación.Properties.Resources.logo_white;
            this.logo_img.Location = new System.Drawing.Point(261, 47);
            this.logo_img.Name = "logo_img";
            this.logo_img.Size = new System.Drawing.Size(230, 75);
            this.logo_img.TabIndex = 0;
            this.logo_img.TabStop = false;
            // 
            // panel_container
            // 
            this.panel_container.Controls.Add(this.login_label);
            this.panel_container.Controls.Add(this.login_btn);
            this.panel_container.Controls.Add(this.password_txt);
            this.panel_container.Controls.Add(this.password_label);
            this.panel_container.Controls.Add(this.email_txt);
            this.panel_container.Controls.Add(this.email_label);
            this.panel_container.Controls.Add(this.user_txt);
            this.panel_container.Controls.Add(this.user_label);
            this.panel_container.Location = new System.Drawing.Point(261, 175);
            this.panel_container.Name = "panel_container";
            this.panel_container.Size = new System.Drawing.Size(263, 436);
            this.panel_container.TabIndex = 1;
            this.panel_container.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_container_Paint);
            // 
            // user_label
            // 
            this.user_label.AutoSize = true;
            this.user_label.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_label.ForeColor = System.Drawing.Color.White;
            this.user_label.Location = new System.Drawing.Point(19, 83);
            this.user_label.Name = "user_label";
            this.user_label.Size = new System.Drawing.Size(76, 18);
            this.user_label.TabIndex = 0;
            this.user_label.Text = "Nombre: ";
            this.user_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // user_txt
            // 
            this.user_txt.Location = new System.Drawing.Point(23, 114);
            this.user_txt.Name = "user_txt";
            this.user_txt.Size = new System.Drawing.Size(210, 24);
            this.user_txt.TabIndex = 1;
            // 
            // email_txt
            // 
            this.email_txt.Location = new System.Drawing.Point(23, 211);
            this.email_txt.Name = "email_txt";
            this.email_txt.Size = new System.Drawing.Size(210, 24);
            this.email_txt.TabIndex = 3;
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email_label.ForeColor = System.Drawing.Color.White;
            this.email_label.Location = new System.Drawing.Point(19, 180);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(158, 18);
            this.email_label.TabIndex = 2;
            this.email_label.Text = "Correo electronico: ";
            // 
            // password_txt
            // 
            this.password_txt.Location = new System.Drawing.Point(23, 303);
            this.password_txt.Name = "password_txt";
            this.password_txt.Size = new System.Drawing.Size(210, 24);
            this.password_txt.TabIndex = 5;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_label.ForeColor = System.Drawing.Color.White;
            this.password_label.Location = new System.Drawing.Point(19, 272);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(101, 18);
            this.password_label.TabIndex = 4;
            this.password_label.Text = "Contraseña: ";
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(67, 376);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(131, 37);
            this.login_btn.TabIndex = 6;
            this.login_btn.Text = "Iniciar";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_label.ForeColor = System.Drawing.Color.White;
            this.login_label.Location = new System.Drawing.Point(59, 9);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(158, 47);
            this.login_label.TabIndex = 7;
            this.login_label.Text = "LOG IN";
            // 
            // Log_in_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 963);
            this.Controls.Add(this.panel_container);
            this.Controls.Add(this.logo_img);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Log_in_form";
            this.Text = "Log_in";
            this.Load += new System.EventHandler(this.Log_in_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo_img)).EndInit();
            this.panel_container.ResumeLayout(false);
            this.panel_container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox logo_img;
        private System.Windows.Forms.Panel panel_container;
        private System.Windows.Forms.TextBox user_txt;
        private System.Windows.Forms.Label user_label;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.TextBox password_txt;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox email_txt;
        private System.Windows.Forms.Label email_label;
        private System.Windows.Forms.Label login_label;
    }
}