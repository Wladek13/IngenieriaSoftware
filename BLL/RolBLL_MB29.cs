using DAL;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolBLL_MB29
    {
        private readonly RolDAL_MB29 rolDAL = new RolDAL_MB29();

        public List<Rol_MB29> ObtenerRoles_MB29()
        {
            return rolDAL.ObtenerTodosRoles_MB29();
        }

        public void GuardarRol_MB29(Rol_MB29 rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new Exception("El nombre del rol no puede estar vacío.");

            rolDAL.GuardarRol_MB29(rol);

            new BLL.DigitoVerificadorBLL_MB29().GuardarDVRol_MB29();

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
               "Creación de Rol",
               "Usuarios",
               $"{SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29} creó un nuevo rol",
               criticidad: 4
            );
        }

        public void EliminarRol_MB29(Rol_MB29 rol)
        {
            if (rolDAL.EstaEnUso_MB29(rol))
                throw new Exception("El rol está asignado a uno o más usuarios y no puede eliminarse.");

            rolDAL.EliminarRol_MB29(rol);

            new BLL.DigitoVerificadorBLL_MB29().GuardarDVRol_MB29();

            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
               "Eliminación de Rol",
               "Usuarios",
               $"{SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29} eliminó un rol",
               criticidad: 4
            );
        }

        public void AgregarComponente_MB29(Rol_MB29 rol, ComponentePermiso_MB29 componente)
        {
            //Valida permisos repetidos usando composite
            rol.AgregarComponenteARol_MB29(componente);

            if (componente is Familia_MB29 familia)
                rolDAL.AgregarFamiliaARol_MB29(rol, familia);
            else if (componente is Permiso_MB29 permiso)
                rolDAL.AgregarPermisoARol_MB29(rol, permiso);

            new BLL.DigitoVerificadorBLL_MB29().GuardarDVRol_MB29();
            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
               "Agregar permiso a Rol",
               "Usuarios",
               $"{SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29} agregó un permiso a un rol",
               criticidad: 2
            );
        }

        public void EliminarFamilia_MB29(Rol_MB29 rol, Familia_MB29 familia)
        {
            rolDAL.EliminarFamiliaDeRol_MB29(rol, familia);
            //Actualiza en memoria sin recargar todo
            var enMemoria = rol.Componentes_MB29.OfType<Familia_MB29>()
                               .FirstOrDefault(f => f.IdFamilia_MB29 == familia.IdFamilia_MB29);
            if (enMemoria != null) rol.Componentes_MB29.Remove(enMemoria);

            new BLL.DigitoVerificadorBLL_MB29().GuardarDVRol_MB29();
            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
               "Eliminar familia de Rol",
               "Usuarios",
               $"{SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29} eliminó una familia de un rol",
               criticidad: 4
            );
        }

        public void EliminarPermiso_MB29(Rol_MB29 rol, Permiso_MB29 permiso)
        {
            rolDAL.EliminarPermisoDeRol_MB29(rol, permiso);
            //Actualiza en memoria sin recargar todo
            var enMemoria = rol.Componentes_MB29.OfType<Permiso_MB29>()
                               .FirstOrDefault(p => p.Id_MB29 == permiso.Id_MB29);
            if (enMemoria != null) rol.Componentes_MB29.Remove(enMemoria);

            new BLL.DigitoVerificadorBLL_MB29().GuardarDVRol_MB29();
            BitacoraBLL_MB29.instancia.Registrar_MB29(
                SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29,
               "Eliminar permiso de Rol",
               "Usuarios",
               $"{SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29.Usuario_MB29} eliminó un permiso de un rol",
               criticidad: 4
            );
        }

        public bool ExisteRol_MB29(string nombre)
        {
            return ObtenerRoles_MB29()
                .Any(f => f.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
