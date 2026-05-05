using BE_MB29;
using BLL;
using BLL_MB29;
using IngenieriaSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_MB29
{
     public partial class FormLogin_MB29 : Form
     {
        
        public FormLogin_MB29()
        {
            InitializeComponent();
        }

        public UsuarioBE_MB29 UsuarioAutenticado { get; private set; }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = UserTxt.Text.Trim();
            string contra = ContraTxt.Text.Trim();

            UsuarioAutenticado = UsuarioBLL_MB29.Instancia.Login_MB29(usuario, contra);

            if (UsuarioAutenticado == null)
            {
                var user = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre(usuario);

                if(user == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
                else
                {
                    if (UsuarioBLL_MB29.Instancia.EstaBloqueado_MB29(user))
                    {
                        MessageBox.Show($"Usuario bloqueado. Contacte al administrador.");
                    }
                    else if(UsuarioBLL_MB29.Instancia.EstaDeshabilitado_MB29(user))
                    {
                        MessageBox.Show($"Usuario desactivado por el administrador. Contacte al administrador.");
                    }
                    else
                    {
                        int intentosRestantes = 3 - user.IntentosErrados;
                        MessageBox.Show($"Usuario o contraseña incorrectos. Intentos restantes: {intentosRestantes}");
                    }
                }
                UserTxt.Clear();
                ContraTxt.Clear();
                UserTxt.Focus();
                return;
            }

            bool loguearOK = SessionManager_MB29.Instancia.IniciarSesion(UsuarioAutenticado);

            if (!loguearOK)
            {
                MessageBox.Show("Ya hay un usuario logueado");
                return;
            }

            MessageBox.Show($"Bienvenido de nuevo {UsuarioAutenticado.Usuario}!");

            FormPrincipal_MB29 FP = new FormPrincipal_MB29();
            FP.Show();
            this.Hide();
        }

        private void RegistrarBtn_Click(object sender, EventArgs e)
        {
            FormRegistrarse_MB29 Freg = new FormRegistrarse_MB29();
            Freg.Show();
        }

        private void RecuperarBtn_Click(object sender, EventArgs e)
        {
            FormRecuperar_MB29 Frec = new FormRecuperar_MB29();
            Frec.Show();
        }
     }
}
