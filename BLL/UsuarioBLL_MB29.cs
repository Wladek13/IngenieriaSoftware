using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_MB29;
using BLL;
using DAL_MB29;

namespace BLL_MB29
{
    public class UsuarioBLL_MB29
    {
        //Singleton
        private static UsuarioBLL_MB29 _instancia;
        public static UsuarioBLL_MB29 Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new UsuarioBLL_MB29();
                return _instancia;
            }
        }

        private List<UsuarioBE_MB29> _usuarios;

        private UsuarioDAL_MB29 _repo;

        private UsuarioBLL_MB29()
        {
            _repo = new UsuarioDAL_MB29();
            _usuarios = _repo.CargarUsuarios_MB29();
        }

        public UsuarioBE_MB29 Login_MB29(string usuario, string contra)
        {
           var uss= _usuarios.FirstOrDefault(u => u.IniciarSesion_MB29(usuario, contra));
            if (uss != null)
            {
                SessionManager_MB29.Instancia.IniciarSesion(uss);

                BitacoraBLL_MB29.instancia.registrar(
                    "Login OK",
                    "Seguridad",
                    $"Usuario {usuario} inició sesión",
                    criticidad: 1
                );
            }

            return uss;
        }

        public void Guardar_MB29(UsuarioBE_MB29 usuario)
        {
            if (usuario.PassHash.Length != 44)
            {
                usuario.PassHash = Encriptador_MB29.EncriptarPassword_MB29(usuario.PassHash);
            }
            _repo.GuardarUsuario_MB29(usuario);
            _usuarios.Add(usuario);

            //accion critica por esto nivel 5
            BitacoraBLL_MB29.instancia.registrar(
               "Alta Usuario",
             "Usuarios",
            $"Se creó el usuario {usuario.Usuario}",
            criticidad: 5
            );
        }

        public void Modificar_MB29(UsuarioBE_MB29 usuario)
        {
            if(usuario.PassHash.Length != 44)
            {
                usuario.PassHash = Encriptador_MB29.EncriptarPassword_MB29(usuario.PassHash);
            }            
            _repo.ModificarUsuario_MB29(usuario);
            var index = _usuarios.FindIndex(u => u.IdPersona == usuario.IdPersona);
            if (index >= 0) _usuarios[index] = usuario;

            BitacoraBLL_MB29.instancia.registrar(
                 "Modificar Usuario",
                  "Usuarios",
              $"Se modificó el usuario {usuario.Usuario}",
               criticidad: 4
             );
        }

        public void Eliminar_MB29(int idPersona)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.IdPersona == idPersona);
            if (usuario == null) return;

            _repo.EliminarUsuario_MB29(usuario);
            _usuarios.Remove(usuario);

            //accion critica por esto nivel 5
            BitacoraBLL_MB29.instancia.registrar(
          "Eliminar Usuario",
         "Usuarios",
         $"Se eliminó el usuario {usuario.Usuario}",
          criticidad: 5

             );

        }

        public List<UsuarioBE_MB29> ObtenerUsuarios()
        {
            return _usuarios;
        }

        public void Recargar_MB29()
        {
            _usuarios = _repo.CargarUsuarios_MB29();
        }
    }
}
