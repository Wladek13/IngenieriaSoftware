using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Permiso_MB29 : ComponentePermiso_MB29
    {
        public int Id { get; set; }

        public override HashSet<int> ObtenerPermisos()
        {
            return new HashSet<int> { Id };
        }
    }
}
