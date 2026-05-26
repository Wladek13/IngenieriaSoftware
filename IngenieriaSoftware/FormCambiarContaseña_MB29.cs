using BE_MB29;
using BLL;
using BLL_MB29;
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
    public partial class FormCambiarContaseña_MB29 : Form
    {
        public FormCambiarContaseña_MB29()
        {
            InitializeComponent();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            //Verificar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtContraActual.Text) ||
                string.IsNullOrWhiteSpace(txtContraNueva.Text) ||
                string.IsNullOrWhiteSpace(txtRepetirContra.Text))
            {
                MessageBox.Show("Completá todos los campos.");
                return;
            }

            //Verificar que las dos contraseñas nuevas coincidan
            if (txtContraNueva.Text != txtRepetirContra.Text)
            {
                MessageBox.Show("Las contraseñas nuevas no coinciden.");
                return;
            }

            //Verificar que la contraseña nueva sea distinta a la actual
            if (txtContraActual.Text == txtContraNueva.Text)
            {
                MessageBox.Show("La contraseña nueva debe ser distinta a la actual.");
                return;
            }

            //Buscar el usuario por nombre
            var usuario = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre(txtNombre.Text);
            if (usuario == null)
            {
                MessageBox.Show("Usuario no encontrado.");
                return;
            }

            //Verificar que la contraseña actual sea correcta
            string hashActual = Encriptador_MB29.EncriptarPassword_MB29(txtContraActual.Text);
            if (hashActual != usuario.PassHash)
            {
                MessageBox.Show("La contraseña actual es incorrecta.");
                return;
            }

            //Cambiar la contraseña
            usuario.PassHash = Encriptador_MB29.EncriptarPassword_MB29(txtContraNueva.Text);
            UsuarioBLL_MB29.Instancia.CambiarContraseña_MB29(usuario);

            MessageBox.Show("Contraseña cambiada correctamente.");

            var usuarioActual = SessionManager_MB29.Instancia.UsuarioActual;
            if (usuarioActual != null && usuarioActual.PrimerLogin)
            {
                UsuarioBLL_MB29.Instancia.MarcarPrimerLoginUsado_MB29(usuarioActual);
                FormPrincipal_MB29 FP = new FormPrincipal_MB29();
                FP.Show();
                this.Close();
            }
        }

        private void FormCambiarContaseña_MB29_Load(object sender, EventArgs e)
        {

        }
    }
}
