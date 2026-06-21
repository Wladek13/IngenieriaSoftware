using Servicio_MB29;
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
using UI_MB29;

namespace IngenieriaSoftware
{
    public partial class FormCambiarContaseña_MB29 : Form, IObserverIdioma
    {
        public FormCambiarContaseña_MB29()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia.IdiomaActual);
        }

        private void EstaLogueado_MB29()
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
        }
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            EstaLogueado_MB29();
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
            var usuario = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre_MB29(txtNombre.Text);
            if (usuario == null)
            {
                MessageBox.Show("Usuario no encontrado.");
                return;
            }

            //Verificar que la contraseña actual sea correcta
            string hashActual = Encriptador_MB29.EncriptarPassword_MB29(txtContraActual.Text);
            if (hashActual != usuario.PassHash_MB29)
            {
                MessageBox.Show("La contraseña actual es incorrecta.");
                return;
            }

            //Cambiar la contraseña
            usuario.PassHash_MB29 = Encriptador_MB29.EncriptarPassword_MB29(txtContraNueva.Text);
            UsuarioBLL_MB29.Instancia.CambiarContraseña_MB29(usuario);

            MessageBox.Show("Contraseña cambiada correctamente.");

            var usuarioActual = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29;
            if (usuarioActual != null && usuarioActual.PrimerLogin_MB29)
            {
                UsuarioBLL_MB29.Instancia.MarcarPrimerLoginUsado_MB29(usuarioActual);
                SessionManager_MB29.Instancia_MB29.CerrarSesion();

                FormLogin_MB29 login2 = new FormLogin_MB29();
                login2.Show();
                this.Close();
                return;
            }

            SessionManager_MB29.Instancia_MB29.CerrarSesion();

            FormLogin_MB29 login = new FormLogin_MB29();
            login.Show();
            this.Close();
        }
        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia;
            this.Text = g.Traducir("cambiar_titulo");
            label4.Text = g.Traducir("cambiar_lbl_nombre");
            label1.Text = g.Traducir("cambiar_lbl_contra_actual");
            label2.Text = g.Traducir("cambiar_lbl_contra_nueva");
            label3.Text = g.Traducir("cambiar_lbl_repetir");
            btnAplicar.Text = g.Traducir("cambiar_btn_aplicar");
        }

        // Override:
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Gestoridioma_MB29.Instancia.Eliminar_MB29(this);
            base.OnFormClosed(e);
        }
    }
}
