using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Rol_MB29 : ComponentePermiso_MB29
    {
        public List<ComponentePermiso_MB29> Componentes { get; set; } = new List<ComponentePermiso_MB29>();

        public override HashSet<int> ObtenerPermisos()
        {
            HashSet<int> permisos = new HashSet<int>();

            foreach (var componente in Componentes)
            {
                permisos.UnionWith(componente.ObtenerPermisos());
            }

            return permisos;
        }

        public void Agregar(ComponentePermiso_MB29 componente)
        {
            if (ObtenerPermisos().Overlaps(componente.ObtenerPermisos()))
            {
                throw new Exception("El rol contiene permisos repetidos.");
            }

            Componentes.Add(componente);
        }
    }
}
