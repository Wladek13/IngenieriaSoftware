using BE_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SessionManager_MB29
    {
        private static SessionManager_MB29 _instancia;

        public static SessionManager_MB29 Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new SessionManager_MB29();
                return _instancia;
            }
        }

        private SessionManager_MB29() { }

        public UsuarioBE_MB29 UsuarioActual { get; private set; }

        public bool HaySesion()
        {
            return UsuarioActual != null;
        }

        public bool IniciarSesion(UsuarioBE_MB29 usuario)
        {
            if (UsuarioActual != null)
                return false; // ya hay alguien logueado

            UsuarioActual = usuario;
            return true;
        }

        public void CerrarSesion()
        {
            if (UsuarioActual != null)
            {
                BitacoraBLL_MB29.instancia.registrar(
                    "Logout",
                    "Seguridad",
                    $"Usuario {UsuarioActual.Usuario} cerró sesión",
                    criticidad: 1
                );
            }
            UsuarioActual = null;
        }
    }
}
