using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_MB29;
using BE_MB29;

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
                        int prioridad = RolCB.SelectedItem.ToString() == "Administrador" ? 1 : 2;

                        //Crear usuario y guardarlo
                        var nuevoUsuario = new UsuarioBE_MB29(
                            id: 0,               //No llega a la BD
                            usuario: usuarioGen,
                            contra: contraGen,
                            esHash: false,       //false porque es texto plano, la BLL lo hashea
                            nombre: nombre,
                            apellido: apellido,
                            dni: dni,
                            prioridad: prioridad,
                            email: email,
                            telefono: ""
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

                case 3:
                    {
                        var seleccionado = (UsuarioBE_MB29)DGVUsuarios.CurrentRow.DataBoundItem;
                        if (!string.IsNullOrEmpty(EmailTxt.Text))
                        {
                            seleccionado.Email = EmailTxt.Text;
                        }
                        if(RolCB.SelectedItem != null)
                        {
                            seleccionado.Prioridad = RolCB.SelectedItem.ToString() == "Administrador" ? 1 : 2;
                        }
                        UsuarioBLL_MB29.Instancia.Modificar_MB29(seleccionado);
                        MessageBox.Show($"El usuario {seleccionado.Nombre} fue modificado correctamente.");
                        CargarDGV();
                        break;
                    }

                case 4:
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

        private void DGVUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (DGVUsuarios.SelectedRows.Count == 0) return;

            DataGridViewRow fila = DGVUsuarios.SelectedRows[0];

            NombreTxt.Text = fila.Cells["Nombre"].Value.ToString();
            ApellidoTxt.Text = fila.Cells["Apellido"].Value.ToString();
            DNITxt.Text = fila.Cells["DNI"].Value.ToString();
            EmailTxt.Text = fila.Cells["Email"].Value.ToString();
            RolCB.SelectedItem = fila.Cells["Prioridad"].Value.ToString() == "1" ? "Administrador" : "Usuario";
        }

        private void CargarDGV()
        {
            DGVUsuarios.DataSource = null;
            List<UsuarioBE_MB29> usuarios = UsuarioBLL_MB29.Instancia.ObtenerUsuarios();
            DGVUsuarios.DataSource = usuarios;
        }
    }
}
