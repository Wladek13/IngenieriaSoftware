using BE;
using BE_MB29;
using BLL;
using BLL_MB29;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace IngenieriaSoftware
{
    public partial class FormGESTIONUSER_MB29: Form
    {
        public FormGESTIONUSER_MB29()
        {
            InitializeComponent();

            CargarDGV();

            //Limpiar campos
            NombreTxt.Clear();
            ApellidoTxt.Clear();
            DNITxt.Clear();
            EmailTxt.Clear();

            //Desactiva todos los campos de default
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = false;
            RolCB.Enabled = false;
        }

        //modo: 0=ninguno, 1=agregar, 2=deshabilitar, 3=modificar, 4=desbloquear
        private int _modo = 0;

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            //Agregar usuario
            _modo = 1;

            //Limpiar campos
            NombreTxt.Clear();
            ApellidoTxt.Clear();
            DNITxt.Clear();
            EmailTxt.Clear();

            //Habilitar todos los campos para ingresar datos
            NombreTxt.Enabled = true;
            ApellidoTxt.Enabled = true;
            DNITxt.Enabled = true;
            EmailTxt.Enabled = true;
            RolCB.Enabled = true;
        }

        private void BtnDeshabilitar_Click(object sender, EventArgs e)
        {
            //Deshabilitar usuario
            _modo = 2;

            //No habilitar ningun campo
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = false;
            RolCB.Enabled = false;

            var seleccionado = (UsuarioBE_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
            if(seleccionado.Estado != "Deshabilitado")
            {
                MessageBox.Show($"Está seguro que desea deshabilitar al usuario {seleccionado.Nombre}? Para confirmar presione el boton Aplicar");
            }
            else
            {
                MessageBox.Show($"Este usuario ya se encuentra deshabilitado");
                _modo = 0;
            }           
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //Modificar usuario
            _modo = 3;

            //Habilitar solo los campos permitidos para modificar
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = true;
            RolCB.Enabled = true;
        }

        private void BtnDesbloquear_Click(object sender, EventArgs e)
        {
            //Desbloquear usuario
            _modo = 4;

            //No habilitar ningun campo
            NombreTxt.Enabled = false;
            ApellidoTxt.Enabled = false;
            DNITxt.Enabled = false;
            EmailTxt.Enabled = false;
            RolCB.Enabled = false;

            var seleccionado = (UsuarioBE_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
            if (!seleccionado.Bloqueado)
            {
                MessageBox.Show($"El usuario {seleccionado.Usuario} no se encuentra bloqueado");
                _modo = 0;
            }
        }

        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            switch (_modo)
            {
                case 1: //Agregar usuario
                    {
                        //Leer campos
                        string nombre = NombreTxt.Text.Trim();
                        string apellido = ApellidoTxt.Text.Trim();
                        string email = EmailTxt.Text.Trim();
                        string dniTexto = DNITxt.Text.Trim();

                        //Validaciones
                        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) ||
                            string.IsNullOrEmpty(dniTexto) || string.IsNullOrEmpty(email))
                        {
                            MessageBox.Show("Completá todos los campos.");
                            return;
                        }

                        if (!double.TryParse(dniTexto, out double dni))
                        {
                            MessageBox.Show("El DNI ingresado no es válido.");
                            return;
                        }

                        //Generar credenciales automáticas
                        var (usuarioGen, contraGen) = UsuarioBLL_MB29.AutogenerarCredenciales_MB29(nombre, apellido, dni);

                        //Asignar prioridad segun combo box
                        int idrol = Convert.ToInt32(RolCB.SelectedValue);

                        //Crear usuario y guardarlo
                        var nuevoUsuario = new UsuarioBE_MB29(
                            id: 0,               //No llega a la BD
                            usuario: usuarioGen,
                            contra: contraGen,
                            esHash: false,       //false porque es texto plano, la BLL lo hashea
                            nombre: nombre,
                            apellido: apellido,
                            dni: dni,
                            idrol: idrol,
                            email: email,
                            telefono: "",
                            false
                        );
                        nuevoUsuario.Estado = "Habilitado";

                        UsuarioBLL_MB29.Instancia.Guardar_MB29(nuevoUsuario);
                        CargarDGV();

                        break;
                    }

                case 2: //Deshabilitar usuario
                    {
                        if (DGVUsuarios.CurrentRow != null)
                        {
                            var seleccionado = (UsuarioBE_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
                            UsuarioBLL_MB29.Instancia.Deshabilitar_MB29(seleccionado);
                            MessageBox.Show($"El usuario {seleccionado.Nombre} fue deshabilitado correctamente.");                           
                        }
                        CargarDGV();
                        break;
                    }

                case 3: //Modificar usuario
                    {
                        var seleccionado = (UsuarioBE_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
                        if (!string.IsNullOrEmpty(EmailTxt.Text))
                        {
                            seleccionado.Email = EmailTxt.Text;
                        }
                        if(RolCB.SelectedItem != null)
                        {
                            int idrol = Convert.ToInt32(RolCB.SelectedValue);
                            seleccionado.IdRol = idrol;
                        }
                        
                        UsuarioBLL_MB29.Instancia.Modificar_MB29(seleccionado);
                        MessageBox.Show($"El usuario {seleccionado.Nombre} fue modificado correctamente.");
                        CargarDGV();
                        break;
                    }

                case 4: //Desbloquear usuario
                    {
                        if(DGVUsuarios.CurrentRow != null)
                        {
                            var seleccionado = (UsuarioBE_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
                            UsuarioBLL_MB29.Instancia.Desbloquear_MB29(seleccionado);
                            MessageBox.Show($"El usuario {seleccionado.Nombre} fue desbloqueado correctamente.");
                        }
                        CargarDGV();
                        break;
                    }
            }
        }

        private void FiltrarUsuarios()
        {
            var usuarios = UsuarioBLL_MB29.Instancia.ObtenerUsuarios();

            if (BloqueadosRB.Checked)
                usuarios = usuarios.Where(u => u.Bloqueado).ToList();
            else if (ActivosRB.Checked)
                usuarios = usuarios.Where(u => !u.Bloqueado).ToList();
            else if (RBTodos.Checked)
                usuarios = usuarios.ToList();

                DGVUsuarios.DataSource = usuarios;
        }

        private void DGVUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVUsuarios.SelectedRows.Count == 0) return;

            DataGridViewRow fila = DGVUsuarios.SelectedRows[0];

            NombreTxt.Text = fila.Cells["Nombre"].Value.ToString();
            ApellidoTxt.Text = fila.Cells["Apellido"].Value.ToString();
            DNITxt.Text = fila.Cells["DNI"].Value.ToString();
            EmailTxt.Text = fila.Cells["Email"].Value.ToString();
            RolCB.SelectedValue = Convert.ToInt32(fila.Cells["IdRol"].Value);
        }

        private void CargarDGV()
        {
            DGVUsuarios.DataSource = null;
            List<UsuarioBE_MB29> usuarios = UsuarioBLL_MB29.Instancia.ObtenerUsuarios();
            DGVUsuarios.DataSource = usuarios; // genera columnas automáticamente

            if (DGVUsuarios.Columns["PassHash"] != null)
                DGVUsuarios.Columns["PassHash"].Visible = false;
            if (DGVUsuarios.Columns["Contra"] != null)
                DGVUsuarios.Columns["Contra"].Visible = false;
            if (DGVUsuarios.Columns["IntentosErrados"] != null)
                DGVUsuarios.Columns["IntentosErrados"].Visible = false;
        }

        private void FormGESTIONUSER_MB29_Load(object sender, EventArgs e)
        {
            var roles = new List<Rol_MB29>
            {
                new Rol_MB29 { IdRol = 1, NombreRol = "Administrador" },
                new Rol_MB29 { IdRol = 2, NombreRol = "Usuario" }
            };

            RolCB.DataSource = roles;
            RolCB.DisplayMember = "NombreRol";
            RolCB.ValueMember = "IdRol";

            if (DGVUsuarios.Columns.Contains("Bloqueado"))
                DGVUsuarios.Columns.Remove("Bloqueado");

            var colBloqueado = new DataGridViewCheckBoxColumn();
            colBloqueado.Name = "Bloqueado";
            colBloqueado.HeaderText = "Bloqueado";
            colBloqueado.DataPropertyName = "Bloqueado";
            colBloqueado.ReadOnly = true;

            DGVUsuarios.Columns.Add(colBloqueado);
        }

        private void BloqueadosRB_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarUsuarios();
        }

        private void ActivosRB_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarUsuarios();
        }

        private void RBTodos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarUsuarios();
        }
    }
}
