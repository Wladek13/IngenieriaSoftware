using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_MB29;

namespace DAL_MB29
{
    public class UsuarioDAL_MB29
    {
         public List<UsuarioBE_MB29> CargarUsuarios_MB29()
         {
            List<UsuarioBE_MB29> usuarios = new List<UsuarioBE_MB29>();

            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT IdPersona, DNI, IdRol, Usuario, PasswordHash, Nombre, Apellido, Telefono, Email, Estado, Bloqueado FROM Persona";
            
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["IdPersona"].ToString());
                        double dni = Convert.ToDouble(reader["DNI"].ToString());
                        int idrol = Convert.ToInt32(reader["IdRol"].ToString());
                        string usuario = reader["Usuario"].ToString();
                        string passwordHash = reader["PasswordHash"].ToString();
                        string telefono = reader["Telefono"].ToString();
                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellido"].ToString();
                        string email = reader["Email"].ToString();
                        string estado = reader["Estado"].ToString();
                        bool bloqueado = Convert.ToBoolean(reader["Bloqueado"]);

                        UsuarioBE_MB29 user = new UsuarioBE_MB29(id, usuario, passwordHash, true, nombre, apellido, dni, idrol, email, telefono, bloqueado);
                        user.Estado = estado;

                        usuarios.Add(user);
                    }
                }
            }
            conectar.Desconectar_MB29();

            return usuarios;
         }

        public void GuardarUsuario_MB29(UsuarioBE_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();

            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Persona (DNI, IdRol, Usuario, Nombre, Apellido, Telefono, Email, PasswordHash, Bloqueado, Estado)
                            VALUES (@DNI, @IdRol, @Usuario, @Nombre, @Apellido, @Telefono, @Email, @PasswordHash, @Bloqueado, @Estado);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@DNI", usuario.DNI);
                comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                comando.Parameters.AddWithValue("@Usuario", usuario.Usuario);
                comando.Parameters.AddWithValue("@PasswordHash", usuario.PassHash);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Email", usuario.Email);
                comando.Parameters.AddWithValue("@Bloqueado", false);
                comando.Parameters.AddWithValue("@Estado", "Habilitado");

                int nuevoId = Convert.ToInt32(comando.ExecuteScalar());
                usuario.IdPersona = nuevoId;
            }
            conectar.Desconectar_MB29();
        }

        public void ModificarUsuario_MB29(UsuarioBE_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();

            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET Email = @Email, IdRol = @IdRol, Bloqueado = @Bloqueado
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona);
                comando.Parameters.AddWithValue("@Email", usuario.Email);
                comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                comando.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void DeshabilitarUsuario_MB29(UsuarioBE_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();
            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET Estado = @Estado 
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona);
                comando.Parameters.AddWithValue("@Estado", usuario.Estado);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void DesbloquearUsuario_MB29(UsuarioBE_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();
            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET Bloqueado = @Bloqueado
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona);
                comando.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void CambiarContraseña_MB29(UsuarioBE_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();

            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET PasswordHash = @PasswordHash
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona);
                comando.Parameters.AddWithValue("@PasswordHash", usuario.PassHash);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }
    }
}
