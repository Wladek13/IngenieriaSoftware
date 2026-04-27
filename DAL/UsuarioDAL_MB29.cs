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

            string query = "SELECT IdPersona, DNI, Prioridad, Usuario, PasswordHash, Nombre, Apellido, Telefono, Email FROM Persona";
            
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                using (SqlDataReader reader = comando.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["IdPersona"].ToString());
                        double dni = Convert.ToDouble(reader["DNI"].ToString());
                        int prioridad = Convert.ToInt32(reader["Prioridad"].ToString());
                        string usuario = reader["Usuario"].ToString();
                        string passwordHash = reader["PasswordHash"].ToString();
                        string telefono = reader["Telefono"].ToString();
                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellido"].ToString();
                        string email = reader["Email"].ToString();

                        usuarios.Add(new UsuarioBE_MB29(id, usuario, passwordHash, true, nombre, apellido, dni, prioridad, email, telefono));
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

            string query = @"INSERT INTO Persona (DNI, Prioridad, Usuario, Nombre, Apellido, Telefono, Email, PasswordHash)
                            VALUES (@DNI, @Prioridad, @Usuario, @Nombre, @Apellido, @Telefono, @Email, @PasswordHash);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@DNI", usuario.DNI);
                comando.Parameters.AddWithValue("@Prioridad", usuario.Prioridad);
                comando.Parameters.AddWithValue("@Usuario", usuario.Usuario);
                comando.Parameters.AddWithValue("@PasswordHash", usuario.PassHash);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Email", usuario.Email);


                int nuevoId = Convert.ToInt32(comando.ExecuteScalar());
                usuario.IdPersona = nuevoId;
            }
            conectar.Desconectar_MB29();
        }

        public void ModificarUsuario_MB29(UsuarioBE_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();

            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"UPDATE Persona SET DNI = @DNI, Usuario = @Usuario, PasswordHash = @PasswordHash, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email
                             WHERE IdPersona = @IdPersona";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona);
                comando.Parameters.AddWithValue("@DNI", usuario.DNI);
                comando.Parameters.AddWithValue("@Usuario", usuario.Usuario);
                comando.Parameters.AddWithValue("@PasswordHash", usuario.PassHash);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Email", usuario.Email);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();
        }

        public void EliminarUsuario_MB29(UsuarioBE_MB29 usuario)
        {
            var conectar = new ConexionDB_MB29();
            SqlConnection conexion = conectar.Conectar_MB29();

            string query = @"DELETE FROM Persona WHERE IdPersona = @IdPersona";
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@IdPersona", usuario.IdPersona);

                comando.ExecuteNonQuery();
            }
            conectar.Desconectar_MB29();

        }
    }
}
