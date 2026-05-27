using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
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
        private int _idrol;
        public int IdRol
        {
            get { return _idrol; }
            set { _idrol = value; }
        }
        private int _intentosErrados;
        public int IntentosErrados
        {
            get { return _intentosErrados; }
            set { _intentosErrados = value; }
        }

        private bool _bloqueado;
        public bool Bloqueado
        {
            get { return _bloqueado; }
            set { _bloqueado = value; }
        }

        private string _estado;
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private bool _primerLogin;
        public bool PrimerLogin
        {
            get { return _primerLogin; }
            set { _primerLogin = value; }
        }

        public UsuarioBE_MB29(int id, string usuario, string contra, bool esHash, string nombre, string apellido, double dni, int idrol, string email, string telefono, bool bloqueado)
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
            IdRol = idrol;
            Email = email;
            Telefono = telefono;
            IntentosErrados = 0;
            Bloqueado = bloqueado;
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
