using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
   public class BitacoraBE_MB29
    {

     private int _Id;
        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _Usuario;
        public string usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        private string _Accion;
        public string accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }

       private string _Modulo;
        public string modulo
        {
            get { return _Modulo; }
            set { _Modulo = value; }
        }
        private DateTime _Fecha;
        public DateTime fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }
      private string _descripcion;
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        //evaluamos del 1 al 5 , 5 acciones criticas, 1 acciones menores
        private int _criticidad;
        public int Criticidad
        {
            get { return _criticidad; }
            set { _criticidad = value; }
        }
        public BitacoraBE_MB29(string _usuario, string _accion, string _modulo, DateTime _fecha, string _descripcion, int _criticidad)
        {
            usuario = _usuario;
            accion = _accion;
            modulo = _modulo;
            fecha = _fecha;
            Descripcion = _descripcion;
            Criticidad = _criticidad;
        }

        public BitacoraBE_MB29(int _id, string _usuario, string _accion, string _modulo, DateTime _fecha, string _descripcion, int _criticidad)
        {
            id = _id;
            usuario = _usuario;
            accion = _accion;
            modulo = _modulo;
            fecha = _fecha;
            Descripcion = _descripcion;
            Criticidad = _criticidad;
        }
    }
}
