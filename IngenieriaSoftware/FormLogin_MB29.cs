using Servicio_MB29;
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
     public partial class FormLogin_MB29 : Form, IObserverIdioma
    {
        
        public FormLogin_MB29()
        {
            InitializeComponent();
            Gestoridioma_MB29.Instancia.Agregar_MB29(this);
            actualizar_MB29(Gestoridioma_MB29.Instancia.IdiomaActual);
        }

        public UsuarioServicio_MB29 UsuarioAutenticado { get; private set; }

        private void RecuperarBtn_Click(object sender, EventArgs e)
        {
            FormCambiarContaseña_MB29 Frec = new FormCambiarContaseña_MB29();
            Frec.Show();
        }

        private void BtnLogin_Click_1(object sender, EventArgs e)
        {
            bool loguearOK = SessionManager_MB29.Instancia_MB29.IniciarSesion(UsuarioAutenticado);

            if (!loguearOK)
            {
                MessageBox.Show($"Ya hay un usuario logueado");
                return;
            }

            string usuario = UserTxt.Text.Trim();
            string contra = ContraTxt.Text.Trim();

            var user = UsuarioBLL_MB29.Instancia.ObtenerUsuarioPorNombre_MB29(usuario);

            if (UsuarioBLL_MB29.Instancia.EstaBloqueado_MB29(user))
            {
                MessageBox.Show($"Usuario bloqueado. Contacte al administrador.");
                return;
            }
            else if (UsuarioBLL_MB29.Instancia.EstaDeshabilitado_MB29(user))
            {
                MessageBox.Show($"Usuario deshabilitado por el administrador. Contacte al administrador.");
                return;
            }

            UsuarioAutenticado = UsuarioBLL_MB29.Instancia.Login_MB29(usuario, contra);

            if (UsuarioAutenticado == null)
            {
                if (user == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
                else
                {
                    if (UsuarioBLL_MB29.Instancia.EstaBloqueado_MB29(user))
                    {
                        MessageBox.Show($"Usuario bloqueado. Contacte al administrador.");
                    }
                    else if (UsuarioBLL_MB29.Instancia.EstaDeshabilitado_MB29(user))
                    {
                        MessageBox.Show($"Usuario deshabilitado por el administrador. Contacte al administrador.");
                    }
                    else
                    {
                        int intentosRestantes = 4 - BitacoraBLL_MB29.instancia.ObtenerIntentosFallidos_MB29(usuario);
                        MessageBox.Show($"Usuario o contraseña incorrectos. Intentos restantes: {intentosRestantes}");
                    }
                }
                UserTxt.Clear();
                ContraTxt.Clear();
                UserTxt.Focus();
                return;
            }

            Gestoridioma_MB29.Instancia.AplicarIdiomaUsuario();

            if (UsuarioAutenticado.PrimerLogin_MB29)
            {               
                MessageBox.Show("Bienvenido. Como es tu primer ingreso, debés cambiar tu contraseña.");              
                FormCambiarContaseña_MB29 fcc = new FormCambiarContaseña_MB29();
                fcc.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show($"Bienvenido de nuevo {UsuarioAutenticado.Usuario_MB29}!");
                FormPrincipal_MB29 FP = new FormPrincipal_MB29();
                FP.Show();
                this.Hide();
            }
        }

        public void actualizar_MB29(string idioma)
        {
            var g = Gestoridioma_MB29.Instancia;
            this.Text = g.Traducir("login_titulo");
            label1.Text = g.Traducir("login_usuario");
            label2.Text = g.Traducir("login_contrasena");
            BtnLogin.Text = g.Traducir("login_btn_ingresar");
            btnSalir.Text = g.Traducir("login_btn_salir");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Gestoridioma_MB29.Instancia.Eliminar_MB29(this);
            base.OnFormClosed(e);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
