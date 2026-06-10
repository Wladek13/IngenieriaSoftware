using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Familia_MB29 : ComponentePermiso_MB29
    {
        public List<ComponentePermiso_MB29> Hijos { get; set; } = new List<ComponentePermiso_MB29>();

        public override HashSet<int> ObtenerPermisos()
        {
            HashSet<int> permisos = new HashSet<int>();

            foreach (var hijo in Hijos)
            {
                permisos.UnionWith(hijo.ObtenerPermisos());
            }

            return permisos;
        }

        public void Agregar(ComponentePermiso_MB29 componente)
        {
            if (componente == this)
                throw new Exception("Una familia no puede contenerse a sí misma.");

            if (componente is Familia_MB29 familia &&
                ContieneFamilia(familia, this))
            {
                throw new Exception("Se produciría una referencia circular.");
            }

            if (ObtenerPermisos().Overlaps(componente.ObtenerPermisos()))
            {
                throw new Exception("Existen permisos repetidos.");
            }

            Hijos.Add(componente);
        }

        private bool ContieneFamilia(Familia_MB29 origen, Familia_MB29 buscada)
        {
            foreach (var hijo in origen.Hijos)
            {
                if (hijo == buscada)
                    return true;

                if (hijo is Familia_MB29 familia &&
                    ContieneFamilia(familia, buscada))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
