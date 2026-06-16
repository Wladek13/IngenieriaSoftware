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
    public partial class FormGESTIONPERFIL : Form
    {
        private readonly FamiliaBLL_MB29 familiaBLL = new FamiliaBLL_MB29();
        private readonly PermisoBLL_MB29 permisoBLL = new PermisoBLL_MB29();
        public FormGESTIONPERFIL()
        {
            InitializeComponent();
            CargarFamilias();
            CargarPermisos();
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
                familiaBLL.GuardarFamilia(familia);
                txtFamilia.Clear();
                CargarFamilias();
                MessageBox.Show("Familia creada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear familia: " + ex.Message);
            }
        }

        private void CargarFamilias()
        {
            var familias = new FamiliaBLL_MB29().ObtenerFamilias();
            LBFamilias.DataSource = null;
            LBFamilias.DataSource = familias;
            LBFamilias.DisplayMember = "Nombre";
        }

        private void CargarPermisos()
        {
            var permisos = new PermisoBLL_MB29().ObtenerPermisos();
            LBPermisos.DataSource = null;
            LBPermisos.DataSource = permisos;
            LBPermisos.DisplayMember = "Nombre";
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
                familiaBLL.Eliminar(familia);
                CargarFamilias();
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
                familiaBLL.AgregarComponente(familia, componente);
                MessageBox.Show("Permiso agregado correctamente.");
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
            if (LBFamilias.SelectedItem == null || LBPermisos.SelectedItem == null)
        {
            MessageBox.Show("Seleccione una familia y un permiso.");
            return;
        }

        try
        {
            var familia    = (Familia_MB29)LBFamilias.SelectedItem;
            var componente = (ComponentePermiso_MB29)LBPermisos.SelectedItem;
            familiaBLL.EliminarPermiso(familia, componente);
            CargarFamilias();  // refresca para reflejar la eliminación
            MessageBox.Show("Permiso eliminado.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al eliminar permiso: " + ex.Message);
        }
        }
    }
}
