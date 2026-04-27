namespace UI_MB29
{
    partial class FormLogin_MB29
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
            this.UserTxt = new System.Windows.Forms.TextBox();
            this.ContraTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InicioSesionBtn = new System.Windows.Forms.Button();
            this.RecuperarContraBtn = new System.Windows.Forms.Button();
            this.RegistrarBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserTxt
            // 
            this.UserTxt.Location = new System.Drawing.Point(80, 16);
            this.UserTxt.Name = "UserTxt";
            this.UserTxt.Size = new System.Drawing.Size(291, 20);
            this.UserTxt.TabIndex = 0;
            // 
            // ContraTxt
            // 
            this.ContraTxt.Location = new System.Drawing.Point(80, 53);
            this.ContraTxt.Name = "ContraTxt";
            this.ContraTxt.Size = new System.Drawing.Size(291, 20);
            this.ContraTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contraseña";
            // 
            // InicioSesionBtn
            // 
            this.InicioSesionBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InicioSesionBtn.Location = new System.Drawing.Point(22, 106);
            this.InicioSesionBtn.Name = "InicioSesionBtn";
            this.InicioSesionBtn.Size = new System.Drawing.Size(136, 60);
            this.InicioSesionBtn.TabIndex = 4;
            this.InicioSesionBtn.Text = "Iniciar Sesión";
            this.InicioSesionBtn.UseVisualStyleBackColor = true;
            // 
            // RecuperarContraBtn
            // 
            this.RecuperarContraBtn.Location = new System.Drawing.Point(245, 91);
            this.RecuperarContraBtn.Name = "RecuperarContraBtn";
            this.RecuperarContraBtn.Size = new System.Drawing.Size(126, 41);
            this.RecuperarContraBtn.TabIndex = 5;
            this.RecuperarContraBtn.Text = "¿Olvidaste tu contraseña?";
            this.RecuperarContraBtn.UseVisualStyleBackColor = true;
            // 
            // RegistrarBtn
            // 
            this.RegistrarBtn.Location = new System.Drawing.Point(245, 138);
            this.RegistrarBtn.Name = "RegistrarBtn";
            this.RegistrarBtn.Size = new System.Drawing.Size(126, 45);
            this.RegistrarBtn.TabIndex = 6;
            this.RegistrarBtn.Text = "¿No estás registrado?\r\n>>>Registrarse<<<\r\n";
            this.RegistrarBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 202);
            this.Controls.Add(this.RegistrarBtn);
            this.Controls.Add(this.RecuperarContraBtn);
            this.Controls.Add(this.InicioSesionBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ContraTxt);
            this.Controls.Add(this.UserTxt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UserTxt;
        private System.Windows.Forms.TextBox ContraTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button InicioSesionBtn;
        private System.Windows.Forms.Button RecuperarContraBtn;
        private System.Windows.Forms.Button RegistrarBtn;
    }
}

