using Servicio_MB29;
using System;
using DAL_MB29;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class FamiliaBLL_MB29
    {
        private readonly FamiliaDAL_MB29 familiaDAL = new FamiliaDAL_MB29();
        public bool FamiliaEstaEnUso(
        Familia_MB29 familia,
        List<Familia_MB29> familias,
        List<Rol_MB29> roles)
        {
            bool usadaPorFamilia =
                familias.Any(f => f.Hijos.Contains(familia));

            bool usadaPorRol =
                roles.Any(r => r.Componentes.Contains(familia));

            return usadaPorFamilia || usadaPorRol;
        }

        public void EliminarPermiso(Familia_MB29 familia, Permiso_MB29 permiso)
        {
            familiaDAL.EliminarPermiso(familia, permiso);
            //Actualizo los hijos en memoria
            var hijo = familia.Hijos.OfType<Permiso_MB29>().FirstOrDefault(p => p.Id == permiso.Id);
            if (hijo != null) familia.Hijos.Remove(hijo);
        }

        public List<Familia_MB29> ObtenerFamilias()
        {
            return familiaDAL.ObtenerTodas();
        }

        public void GuardarFamilia(Familia_MB29 familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new Exception("El nombre de la familia no puede estar vacío.");

            familiaDAL.Guardar(familia);

        }

        public List<Permiso_MB29> PermisosFamilia(Familia_MB29 familia)
        {
            List<Permiso_MB29> permisos = new List<Permiso_MB29>();
            permisos = familiaDAL.PermisosFamilia(familia);
            return permisos;
        }

        public void Eliminar(Familia_MB29 familia)
        {
            var familias = familiaDAL.ObtenerTodas();

            bool usadaPorOtraFamilia = familias.Any(f =>
                f.IdFamilia != familia.IdFamilia &&
                ContieneComponente(f, familia));

            if (usadaPorOtraFamilia)
                throw new Exception("La familia está siendo utilizada por otra familia.");

            var roles = familiaDAL.ObtenerRolesQueUsanFamilia(familia.IdFamilia);

            if (roles.Count > 0)
                throw new Exception("La familia está asignada a uno o más roles.");

            familiaDAL.Eliminar(familia);
        }
        public void AgregarComponente(Familia_MB29 familia, ComponentePermiso_MB29 componente)
        {
            // Valida reglas del composite (lanza excepción si hay ciclo o permiso repetido)
            familia.Agregar(componente);

            // Persiste en BD según tipo
            if (componente is Permiso_MB29 permiso)
                familiaDAL.AgregarPermiso(familia, permiso);
            else
                throw new Exception("Solo se pueden agregar permisos a una familia desde esta pantalla.");
        }


        private bool ContieneComponente(Familia_MB29 familia, ComponentePermiso_MB29 buscado)
        {
            foreach (var hijo in familia.Hijos)
            {
                if (hijo == buscado) return true;
                if (hijo is Familia_MB29 subfamilia && ContieneComponente(subfamilia, buscado))
                    return true;
            }
            return false;
        }

    }
}
