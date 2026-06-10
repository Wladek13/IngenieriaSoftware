using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FamiliaBLL_MB29
    {
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

        public void EliminarPermiso(Familia_MB29 familia, ComponentePermiso_MB29 componente)
        {
            if (!familia.Hijos.Contains(componente))
                throw new Exception("El componente no existe en esta familia.");

            familia.Hijos.Remove(componente);
        }

        public List<Familia_MB29> ObtenerFamilias()
        {

        }

        public void GuardarFamilia()
        {

        }

        public void Eliminar(Familia_MB29 familia)
        {
            var familias = familiaDAL.ObtenerTodas();
            var roles = rolDAL.ObtenerTodos();

            bool usadaPorOtraFamilia = familias.Any(f =>
                f != familia &&
                ContieneComponente(f, familia));

            if (usadaPorOtraFamilia)
                throw new Exception("La familia está siendo utilizada por otra familia.");

            bool usadaPorRol = roles.Any(r =>
                r.Componentes.Any(c => c == familia));

            if (usadaPorRol)
                throw new Exception("La familia está asignada a un rol.");

            familiaDAL.Eliminar(familia);
        }
    }
}
