using BE_MB29;
using BLL;
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
    public partial class FormPrincipal_MB29 : Form
    {
        public FormPrincipal_MB29()
        {
            InitializeComponent();
            if (SessionManager_MB29.Instancia.UsuarioActual.IdRol == 1)
            {
                administradorToolStripMenuItem.Enabled = true;
                usuarioToolStripMenuItem.Enabled = true;
                rF1ToolStripMenuItem.Enabled = true;
                rF2ToolStripMenuItem.Enabled = true;
                ayudaToolStripMenuItem.Enabled = true;
            }
            else
            {
                administradorToolStripMenuItem.Enabled = false;
                administradorToolStripMenuItem.Visible = false;
                usuarioToolStripMenuItem.Enabled = true;
                rF1ToolStripMenuItem.Enabled = false;
                rF1ToolStripMenuItem.Visible = false;
                rF2ToolStripMenuItem.Enabled = false;
                rF2ToolStripMenuItem.Visible = false;
                ayudaToolStripMenuItem.Enabled = true;
            }
        }

        /*public FormPrincipal_MB29(FormLogin_MB29 login)
        {
            InitializeComponent();
            _login = login;
        }*/

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGESTIONUSER_MB29 FGESTUS= new FormGESTIONUSER_MB29();
            FGESTUS.Show();
        }

        private void bitacoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBitacora_MB29 FBITA = new FormBitacora_MB29();
            FBITA.Show();
        }

        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SessionManager_MB29.Instancia.CerrarSesion();
            FormLogin_MB29 _login = new FormLogin_MB29();
            _login.Show();
            this.Close();    // cierra el menú
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin_MB29 _login = new FormLogin_MB29();
            _login.Show();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCambiarContaseña_MB29 CamCon = new FormCambiarContaseña_MB29();
            CamCon.Show();
        }
    }
}
