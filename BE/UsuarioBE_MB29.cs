using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_MB29
{
    public class UsuarioBE_MB29 : IInicioSesion_MB29
    {
        private int _idPersona;

        public int IdPersona
        {
            get { return _idPersona; }
            set { _idPersona = value; }
        }

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _telefono;
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        private double _dni;
        public double DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private string _passhash;
        public string PassHash
        {
            get { return _passhash; }
            set { _passhash = value; }
        }
        public string contra;
        public string Contra
        {
            get { return contra; }
            set { contra = value; }
        }
        private int _prioridad;
        public int Prioridad
        {
            get { return _prioridad; }
            set { _prioridad = value; }
        }

        public UsuarioBE_MB29(int id, string usuario, string contra, bool esHash, string nombre, string apellido, double dni, int prioridad, string email, string telefono)
        {
            IdPersona = id;
            Usuario = usuario;
            if (esHash)
            {
                PassHash = contra;
            }
            else
            {
                PassHash = Encriptador_MB29.EncriptarPassword_MB29(contra);
            }
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            Prioridad = prioridad;
            Email = email;
            Telefono = telefono;
        }

        public UsuarioBE_MB29(string usuario, string contra)
        {
            Usuario = usuario;
            PassHash = Encriptador_MB29.EncriptarPassword_MB29(contra);
        }

        public bool IniciarSesion_MB29(string usuario, string passwordIngresada)
        {
            return Usuario == usuario && Encriptador_MB29.EncriptarPassword_MB29(passwordIngresada) == this.PassHash;
        }
    }
}
