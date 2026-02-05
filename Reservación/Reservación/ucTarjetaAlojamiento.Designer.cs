namespace Reservación
{
    partial class ucTarjetaAlojamiento
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.picAlojamiento = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picAlojamiento)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picAlojamiento
            // 
            this.picAlojamiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.picAlojamiento.Location = new System.Drawing.Point(0, 0);
            this.picAlojamiento.Name = "picAlojamiento";
            this.picAlojamiento.Size = new System.Drawing.Size(350, 200);
            this.picAlojamiento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAlojamiento.TabIndex = 0;
            this.picAlojamiento.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRating);
            this.panel1.Controls.Add(this.lblPrecio);
            this.panel1.Controls.Add(this.lblUbicacion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 150);
            this.panel1.TabIndex = 1;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUbicacion.Location = new System.Drawing.Point(3, 13);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(59, 23);
            this.lblUbicacion.TabIndex = 0;
            this.lblUbicacion.Text = "label1";
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.Location = new System.Drawing.Point(3, 82);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(59, 23);
            this.lblPrecio.TabIndex = 1;
            this.lblPrecio.Text = "label1";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRating.Location = new System.Drawing.Point(273, 82);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(25, 23);
            this.lblRating.TabIndex = 2;
            this.lblRating.Text = "⭐";
            // 
            // ucTarjetaAlojamiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picAlojamiento);
            this.Name = "ucTarjetaAlojamiento";
            this.Size = new System.Drawing.Size(350, 350);
            ((System.ComponentModel.ISupportInitialize)(this.picAlojamiento)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picAlojamiento;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblPrecio;
    }
}
