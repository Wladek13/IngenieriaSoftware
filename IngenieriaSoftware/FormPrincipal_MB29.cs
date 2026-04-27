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

        public FormPrincipal_MB29(FormLogin_MB29 login)
        {
            InitializeComponent();
            _login = login;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            SessionManager_MB29.Instancia.CerrarSesion();

            _login.Show();   // vuelve al login oculto
            this.Close();    // cierra el menú
        }
    }
}
