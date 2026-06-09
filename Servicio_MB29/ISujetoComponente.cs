using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public interface ISujetoComponente
    {
         void Agregar_MB29(IObserverIdioma idioma);

         void Eliminar_MB29(IObserverIdioma idioma);

        void Notificar_MB29();




    }
}
