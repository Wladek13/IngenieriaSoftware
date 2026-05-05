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
        }

        private FormLogin_MB29 _login;

        /*public FormPrincipal_MB29(FormLogin_MB29 login)
        {
            InitializeComponent();
            _login = login;
        }*/

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegistrarse_MB29 freg = new FormRegistrarse_MB29();
            freg.Show();
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
            _login.Show();   // vuelve al login oculto
            this.Close();    // cierra el menú
        }
    }
}
