using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
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
    }
}
