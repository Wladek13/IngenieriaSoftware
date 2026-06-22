using BLL;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IngenieriaSoftware
{
    public partial class FormRoles : Form, IObserverSesion_MB29
    {

        private readonly RolBLL_MB29 rolBLL = new RolBLL_MB29();
        private readonly FamiliaBLL_MB29 familiaBLL = new FamiliaBLL_MB29();
        private readonly PermisoBLL_MB29 permisoBLL = new PermisoBLL_MB29();
        public FormRoles()
        {
            InitializeComponent();
            CargarRoles();
            CargarFamilias();
            CargarPermisos();
            SessionManager_MB29.Instancia_MB29.AgregarObserverSesion(this);
        }

        //Cuando el SessionManager notifica, el form se cierra solo
        public void SesionCerrada_MB29()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Close()));
            else
                this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            //Se desregistra si el usuario cierra el form manualmente
            SessionManager_MB29.Instancia_MB29.EliminarObserverSesion(this);
            base.OnFormClosed(e);
        }

        /*private void EstaLogueado_MB29()
        {
            if (!SessionManager_MB29.Instancia_MB29.HaySesion())
            {
                this.Close();
                return;
            }
            else if (SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.IdRol_MB29 == 2)
            {
                this.Close();
                return;
            }
        }*/

        private void CargarRoles()
        {
            var roles = rolBLL.ObtenerRoles();
            LBRoles.DataSource = null;
            LBRoles.DataSource = roles;
            LBRoles.DisplayMember = "Nombre";
        }

        private void CargarFamilias()
        {
            var familias = familiaBLL.ObtenerFamilias();
            LBFamilias.DataSource = null;
            LBFamilias.DataSource = familias;
            LBFamilias.DisplayMember = "Nombre";
        }

        private void CargarPermisos()
        {
            var permisos = permisoBLL.ObtenerPermisos();
            LBPermisos.DataSource = null;
            LBPermisos.DataSource = permisos;
            LBPermisos.DisplayMember = "Nombre";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRol.Text))
            {
                MessageBox.Show("Ingrese un nombre para el rol.");
                return;
            }

            try
            {
                var rol = new Rol_MB29 { Nombre = txtRol.Text.Trim() };
                rolBLL.GuardarRol(rol);
                txtRol.Clear();
                CargarRoles();
                MessageBox.Show("Rol creado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear rol: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;
                rolBLL.Eliminar(rol);
                CargarRoles();
                MessageBox.Show("Rol eliminado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarPerm_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            if (LBFamilias.SelectedItem == null && LBPermisos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un permiso para agregar al rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;

                var permiso = (Permiso_MB29)LBPermisos.SelectedItem;
                rolBLL.AgregarComponente(rol, permiso);
                MessageBox.Show("Permiso agregado al rol correctamente.");

                CargarRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAgregarFam_Click(object sender, EventArgs e)
        {
            if (LBRoles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            if (LBFamilias.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una familia para agregar al rol.");
                return;
            }

            try
            {
                var rol = (Rol_MB29)LBRoles.SelectedItem;

                var familia = (Familia_MB29)LBFamilias.SelectedItem;
                rolBLL.AgregarComponente(rol, familia);
                MessageBox.Show("Familia agregada al rol correctamente.");

                CargarRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
