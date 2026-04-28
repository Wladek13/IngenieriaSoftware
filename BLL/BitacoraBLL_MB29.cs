using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class BitacoraBLL_MB29
    {
       private static BitacoraBLL_MB29 _instancia;
        
        public static BitacoraBLL_MB29 instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new BitacoraBLL_MB29();
                return _instancia;
            }
        }

        private BitacoraDAL_MB29 _dal = new BitacoraDAL_MB29();

        public void registrar(string accion, string modulo, string descripcion, int criticidad)
        {
            //no va a guardar nada en bitacora sin usuario logueado
            if (!SessionManager_MB29.Instancia.HaySesion())
                throw new Exception("No se puede registrar en la bitácora sin un usuario logueado.");

            string usuario = SessionManager_MB29.Instancia.UsuarioActual.Usuario;
            var bitacora = new BE.BitacoraBE_MB29
            {
                usuario = usuario,
                accion = accion,
                modulo = modulo,
                fecha = DateTime.Now,
                Descripcion = descripcion,
                Criticidad = criticidad
            };
            _dal.Guardar(bitacora);
        }
    }
}
