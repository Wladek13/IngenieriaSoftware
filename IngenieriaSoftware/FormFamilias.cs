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
        public FormGESTIONPERFIL()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFamilia.Text))
            {
                MessageBox.Show("Ingrese un nombre.");
                return;
            }

            Familia_MB29 familia = new Familia_MB29()
            {
                Nombre = txtFamilia.Text
            };

            FamiliaBLL_MB29.GuardarFamilia(familia);

            CargarFamilias();

            txtFamilia.Clear();
        }

        private void CargarFamilias()
        {
            var familias = new FamiliaBLL_MB29().ObtenerFamilias();
            LBFamilias.DataSource = null;
            LBFamilias.DataSource = familias;
        }

        private void CargarPermisos()
        {
            var permisos = new PermisoBLL_MB29().ObtenerPermisos();
            LBPermisos.DataSource = null;
            LBPermisos.DataSource = permisos;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (LBFamilias.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una familia.");
                return;
            }

            Familia_MB29 familia =
                (Familia_MB29)LBFamilias.SelectedItem;

            FamiliaBLL_MB29.Eliminar(familia);

            CargarFamilias();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Familia_MB29 familia = (Familia_MB29)LBFamilias.SelectedItem;

            ComponentePermiso_MB29 componente =
            (ComponentePermiso_MB29)LBPermisos.SelectedItem;

            familia.Agregar(componente);

            MessageBox.Show("Permiso agregado.");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnElimPerm_Click(object sender, EventArgs e)
        {
            Familia_MB29 familia =
                (Familia_MB29)LBFamilias.SelectedItem;

            ComponentePermiso_MB29 permiso =
                (ComponentePermiso_MB29)LBPermisos.SelectedItem;

            new FamiliaBLL_MB29().EliminarPermiso(familia, permiso);
        }
    }
}
