using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Servicio_MB29;

namespace IngenieriaSoftware
{
    public partial class FormGESTIONPERFIL_MB29 : Form, IObserverSesion_MB29, IObserverIdioma_MB29
    {
        private readonly FamiliaBLL_MB29 familiaBLL_MB29 = new FamiliaBLL_MB29();
        private readonly PermisoBLL_MB29 permisoBLL_MB29 = new PermisoBLL_MB29();
        public FormGESTIONPERFIL_MB29()
        {
            InitializeComponent();

            Gestoridioma_MB29.Instancia_MB29.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia_MB29.IdiomaActual_MB29);

            if (!SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Rol_MB29.TienePermiso_MB29(Permisos_MB29.GestionPerfiles))
            {
                MessageBox.Show("No tiene permiso para acceder a esta sección.");
                this.Load += (s, e) => this.Close();
                return;
            }

            CargarFamilias_MB29();
            CargarPermisos_MB29();
            SessionManager_MB29.Instancia_MB29.AgregarObserverSesion_MB29(this);
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
            SessionManager_MB29.Instancia_MB29.EliminarObserverSesion_MB29(this);
            base.OnFormClosed(e);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFamilia.Text))
            {
                MessageBox.Show("Ingrese un nombre.");
                return;
            }

            try
            {
                var familia = new Familia_MB29 { Nombre = txtFamilia.Text.Trim() };
                familiaBLL_MB29.GuardarFamilia_MB29(familia);
                txtFamilia.Clear();
                CargarFamilias_MB29();
                CargarPermisos_MB29();
                MessageBox.Show("Familia creada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear familia: " + ex.Message);
            }
        }

        private void CargarFamilias_MB29()
        {
            var familias = new FamiliaBLL_MB29().ObtenerFamilias_MB29();
            LBFamilias.DataSource = null;
            LBFamilias.DataSource = familias;
            LBFamilias.DisplayMember = "Nombre";
        }

        private void CargarPermisos_MB29()
        {
            //Permisos sueltos
            var permisos = permisoBLL_MB29.ObtenerPermisos_MB29().Cast<ComponentePermiso_MB29>();

            //Familias que tengan un permiso
            var familias = familiaBLL_MB29.ObtenerFamilias_MB29()
                                     .Where(f => f.ObtenerPermisos_MB29().Count > 0)
                                     .Cast<ComponentePermiso_MB29>();

            var todo = permisos.Concat(familias).ToList();

            LBPermisos.DataSource = null;
            LBPermisos.DataSource = todo;
            LBPermisos.DisplayMember = "Nombre";
        }

        private void CargarFamiliasPermisos_MB29()
        {
            if (LBFamilias.SelectedItem == null) return;
            var seleccionado = (Familia_MB29)LBFamilias.SelectedItem;
            var permisos = familiaBLL_MB29.PermisosFamilia_MB29(seleccionado);
            LBPermisosFamilia.DataSource = null;
            LBPermisosFamilia.DataSource = permisos;
            LBPermisosFamilia.DisplayMember = "Nombre";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (LBFamilias.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una familia.");
                return;
            }

            try
            {
                var familia = (Familia_MB29)LBFamilias.SelectedItem;
                familiaBLL_MB29.EliminarFamilia_MB29(familia);
                CargarFamilias_MB29();
                MessageBox.Show("Familia eliminada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (LBFamilias.SelectedItem == null || LBPermisos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una familia y un permiso.");
                return;
            }

            try
            {
                var familia = (Familia_MB29)LBFamilias.SelectedItem;
                var componente = (ComponentePermiso_MB29)LBPermisos.SelectedItem;
                familiaBLL_MB29.AgregarComponente_MB29(familia, componente);
                MessageBox.Show("Permiso agregado correctamente.");
                CargarFamiliasPermisos_MB29();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnElimPerm_Click(object sender, EventArgs e)
        {
            if (LBFamilias.SelectedItem == null || LBPermisosFamilia.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una familia y un permiso de esa familia.");
                return;
            }

            try
            {
                var familia = (Familia_MB29)LBFamilias.SelectedItem;
                var permiso = (Permiso_MB29)LBPermisosFamilia.SelectedItem;
                familiaBLL_MB29.EliminarPermiso_MB29(familia, permiso);
                CargarFamiliasPermisos_MB29();
                MessageBox.Show("Permiso eliminado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar permiso: " + ex.Message);
            }
        }

        private void LBFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFamiliasPermisos_MB29();
        }

        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia_MB29;
            label1.Text = g.Traducir_MB29("familias_lbl_permisos_disponibles");
            label2.Text = g.Traducir_MB29("familias_lbl_familias");
            label3.Text = g.Traducir_MB29("familias_lbl_nueva_familia");
            label4.Text = g.Traducir_MB29("familias_lbl_permisos_familia");
            btnCrear.Text = g.Traducir_MB29("familias_btn_crear");
            btnEliminar.Text = g.Traducir_MB29("familias_btn_eliminar");
            btnAgregar.Text = g.Traducir_MB29("familias_btn_agregar");
            btnElimPerm.Text = g.Traducir_MB29("familias_btn_eliminar_permiso");
            btnSalir.Text = g.Traducir_MB29("familias_btn_salir");
        }
    }
}
