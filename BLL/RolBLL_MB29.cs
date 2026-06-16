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

        public List<Rol_MB29> ObtenerRoles()
        {
            return rolDAL.ObtenerTodos();
        }

        public void GuardarRol(Rol_MB29 rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new Exception("El nombre del rol no puede estar vacío.");

            rolDAL.Guardar(rol);
        }

        public void Eliminar(Rol_MB29 rol)
        {
            if (rolDAL.EstaEnUso(rol))
                throw new Exception("El rol está asignado a uno o más usuarios y no puede eliminarse.");

            rolDAL.Eliminar(rol);
        }

        public void AgregarComponente(Rol_MB29 rol, ComponentePermiso_MB29 componente)
        {
            // Valida permisos repetidos usando el composite
            rol.Agregar(componente);

            // Persiste según tipo
            if (componente is Familia_MB29 familia)
                rolDAL.AgregarFamilia(rol, familia);
            else if (componente is Permiso_MB29 permiso)
                rolDAL.AgregarPermiso(rol, permiso);
        }
    }
}
