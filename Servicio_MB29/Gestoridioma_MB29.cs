using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Gestoridioma_MB29 : ISujetoComponente
    {
        private static Gestoridioma_MB29 _instancia;
        public static Gestoridioma_MB29 Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new Gestoridioma_MB29();
                return _instancia;
            }
        }

        private Gestoridioma_MB29()
        {
            IdiomaActual = "ES";
            CargarJson(IdiomaActual);
        }

        public string IdiomaActual { get; private set; }

        private Dictionary<string, string> _traducciones;
        private readonly List<IObserverIdioma> _observers = new List<IObserverIdioma>();

       
        public void Agregar_MB29(IObserverIdioma observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Eliminar_MB29(IObserverIdioma observer)
        {
            _observers.Remove(observer);
        }

        public void Notificar_MB29()
        {
            foreach (var obs in new List<IObserverIdioma>(_observers))
                obs.actualizar_MB29(IdiomaActual); 
        }

        public void CambiarIdioma(string idioma)
        {
            IdiomaActual = idioma;
            CargarJson(idioma);

            var usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29;
            if (usuario != null)
                usuario.UltimoIdioma_MB29 = idioma;

            Notificar_MB29();
        }

        public void AplicarIdiomaUsuario()
        {
            var usuario = SessionManager_MB29.Instancia_MB29.UsuarioActual_MB29;
            IdiomaActual = usuario.UltimoIdioma_MB29;
            CargarJson(IdiomaActual);
            Notificar_MB29();
        }

        public string Traducir(string clave)
        {
            if (_traducciones != null && _traducciones.ContainsKey(clave))
                return _traducciones[clave];
            return clave; // si no encuentra la clave, devuelve la clave misma
        }

       
        private void CargarJson(string idioma)
        {
            try
            {
                string ruta = Path.Combine("Idiomas", $"{idioma}.json");
                if (File.Exists(ruta))
                {
                    string json = File.ReadAllText(ruta);
                    _traducciones = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
            }
            catch (Exception)
            {
                _traducciones = new Dictionary<string, string>();
            }
        }
    }
}

