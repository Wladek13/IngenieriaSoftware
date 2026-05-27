using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class SessionManager_MB29
    {
        private static SessionManager_MB29 _instancia_MB29;

        public static SessionManager_MB29 Instancia_MB29
        {
            get
            {
                if (_instancia_MB29 == null)
                    _instancia_MB29 = new SessionManager_MB29();
                return _instancia_MB29;
            }
        }

        private SessionManager_MB29() { }

        public UsuarioServicio_MB29 UsuarioActual_MB29 { get; private set; }

        public bool HaySesion()
        {
            return UsuarioActual_MB29 != null;
        }

        public bool IniciarSesion(UsuarioServicio_MB29 usuario)
        {
            if (UsuarioActual_MB29 != null)
                return false; // ya hay alguien logueado

            UsuarioActual_MB29 = usuario;
            return true;
        }

        public void CerrarSesion()
        {
            UsuarioActual_MB29 = null;
        }
    }
}
