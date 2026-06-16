namespace IngenieriaSoftware
{
    partial class FormRoles
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.LBPermisos = new System.Windows.Forms.ListBox();
            this.LBFamilias = new System.Windows.Forms.ListBox();
            this.LBRoles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Familias";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(455, 150);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Permisos";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(682, 464);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 28);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(525, 464);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 28);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(287, 513);
            this.btnCrear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(100, 28);
            this.btnCrear.TabIndex = 11;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(49, 464);
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(100, 28);
            this.btnAplicar.TabIndex = 10;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // LBPermisos
            // 
            this.LBPermisos.FormattingEnabled = true;
            this.LBPermisos.ItemHeight = 16;
            this.LBPermisos.Location = new System.Drawing.Point(449, 170);
            this.LBPermisos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LBPermisos.Name = "LBPermisos";
            this.LBPermisos.Size = new System.Drawing.Size(285, 260);
            this.LBPermisos.TabIndex = 9;
            // 
            // LBFamilias
            // 
            this.LBFamilias.FormattingEnabled = true;
            this.LBFamilias.ItemHeight = 16;
            this.LBFamilias.Location = new System.Drawing.Point(453, 50);
            this.LBFamilias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LBFamilias.Name = "LBFamilias";
            this.LBFamilias.Size = new System.Drawing.Size(281, 84);
            this.LBFamilias.TabIndex = 8;
            // 
            // LBRoles
            // 
            this.LBRoles.FormattingEnabled = true;
            this.LBRoles.ItemHeight = 16;
            this.LBRoles.Location = new System.Drawing.Point(49, 50);
            this.LBRoles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LBRoles.Name = "LBRoles";
            this.LBRoles.Size = new System.Drawing.Size(285, 372);
            this.LBRoles.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Roles";
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(234, 470);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(226, 22);
            this.txtRol.TabIndex = 18;
            // 
            // FormRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 554);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LBRoles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.LBPermisos);
            this.Controls.Add(this.LBFamilias);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormRoles";
            this.Text = "FormRoles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.ListBox LBPermisos;
        private System.Windows.Forms.ListBox LBFamilias;
        private System.Windows.Forms.ListBox LBRoles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRol;
    }
}