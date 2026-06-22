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
            this.btnAgregarPerm = new System.Windows.Forms.Button();
            this.LBPermisos = new System.Windows.Forms.ListBox();
            this.LBFamilias = new System.Windows.Forms.ListBox();
            this.LBRoles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.btnAgregarFam = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Familias";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(344, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Permisos";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(496, 417);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(112, 417);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.Text = "Eliminar Rol";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(31, 417);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(75, 23);
            this.btnCrear.TabIndex = 11;
            this.btnCrear.Text = "Crear Rol";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnAgregarPerm
            // 
            this.btnAgregarPerm.Location = new System.Drawing.Point(429, 381);
            this.btnAgregarPerm.Name = "btnAgregarPerm";
            this.btnAgregarPerm.Size = new System.Drawing.Size(126, 23);
            this.btnAgregarPerm.TabIndex = 10;
            this.btnAgregarPerm.Text = "Agregar permiso";
            this.btnAgregarPerm.UseVisualStyleBackColor = true;
            this.btnAgregarPerm.Click += new System.EventHandler(this.btnAgregarPerm_Click);
            // 
            // LBPermisos
            // 
            this.LBPermisos.FormattingEnabled = true;
            this.LBPermisos.Location = new System.Drawing.Point(340, 163);
            this.LBPermisos.Name = "LBPermisos";
            this.LBPermisos.Size = new System.Drawing.Size(215, 212);
            this.LBPermisos.TabIndex = 9;
            // 
            // LBFamilias
            // 
            this.LBFamilias.FormattingEnabled = true;
            this.LBFamilias.Location = new System.Drawing.Point(340, 41);
            this.LBFamilias.Name = "LBFamilias";
            this.LBFamilias.Size = new System.Drawing.Size(212, 69);
            this.LBFamilias.TabIndex = 8;
            // 
            // LBRoles
            // 
            this.LBRoles.FormattingEnabled = true;
            this.LBRoles.Location = new System.Drawing.Point(37, 41);
            this.LBRoles.Name = "LBRoles";
            this.LBRoles.Size = new System.Drawing.Size(215, 303);
            this.LBRoles.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Roles";
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(91, 381);
            this.txtRol.Margin = new System.Windows.Forms.Padding(2);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(170, 20);
            this.txtRol.TabIndex = 18;
            // 
            // btnAgregarFam
            // 
            this.btnAgregarFam.Location = new System.Drawing.Point(429, 116);
            this.btnAgregarFam.Name = "btnAgregarFam";
            this.btnAgregarFam.Size = new System.Drawing.Size(126, 23);
            this.btnAgregarFam.TabIndex = 19;
            this.btnAgregarFam.Text = "Agregar Familia";
            this.btnAgregarFam.UseVisualStyleBackColor = true;
            this.btnAgregarFam.Click += new System.EventHandler(this.btnAgregarFam_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 384);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Nuevo Rol";
            // 
            // FormRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAgregarFam);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LBRoles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnAgregarPerm);
            this.Controls.Add(this.LBPermisos);
            this.Controls.Add(this.LBFamilias);
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
        private System.Windows.Forms.Button btnAgregarPerm;
        private System.Windows.Forms.ListBox LBPermisos;
        private System.Windows.Forms.ListBox LBFamilias;
        private System.Windows.Forms.ListBox LBRoles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.Button btnAgregarFam;
        private System.Windows.Forms.Label label4;
    }
}