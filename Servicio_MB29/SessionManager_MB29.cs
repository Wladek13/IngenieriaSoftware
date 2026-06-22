using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class SessionManager_MB29
    {
        private static SessionManager_MB29 _instancia_MB29;

        public static SessionManager_MB29 Instancia_MB29
        {
            get
            {
                if (_instancia_MB29 == null)
                    _instancia_MB29 = new SessionManager_MB29();
                return _instancia_MB29;
            }
        }

        private SessionManager_MB29() { }

        public UsuarioServicio_MB29 UsuarioActual_MB29 { get; private set; }

        public bool HaySesion()
        {
            return UsuarioActual_MB29 != null;
        }

        public bool IniciarSesion(UsuarioServicio_MB29 usuario)
        {
            if (UsuarioActual_MB29 != null)
                return false; //Ya hay alguien logueado

            UsuarioActual_MB29 = usuario;
            return true;
        }

        private readonly List<IObserverSesion_MB29> _observersSesion = new List<IObserverSesion_MB29>();

        public void AgregarObserverSesion(IObserverSesion_MB29 observer)
        {
            if (!_observersSesion.Contains(observer))
                _observersSesion.Add(observer);
        }

        public void EliminarObserverSesion(IObserverSesion_MB29 observer)
        {
            _observersSesion.Remove(observer);
        }

        public void CerrarSesion()
        {
            UsuarioActual_MB29 = null;
            // Notifica a todos los forms registrados
            foreach (var obs in new List<IObserverSesion_MB29>(_observersSesion))
                obs.SesionCerrada_MB29();
            _observersSesion.Clear(); // los forms ya se cerraron, limpiamos la lista
        }
    }
}
