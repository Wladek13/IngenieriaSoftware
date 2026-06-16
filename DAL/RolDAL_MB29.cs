using DAL_MB29;
using Servicio_MB29;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class RolDAL_MB29
    {
        public List<Rol_MB29> ObtenerTodos()
        {
            var roles = new List<Rol_MB29>();
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT IdRol, Nombre FROM Rol";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    roles.Add(new Rol_MB29
                    {
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }

            conectar.Desconectar_MB29();
            return roles;
        }

        public void Guardar(Rol_MB29 rol)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Rol (Nombre)
                             VALUES (@Nombre);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);
                rol.IdRol = Convert.ToInt32(cmd.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
        }

        public void Eliminar(Rol_MB29 rol)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM RolFamilia WHERE IdRol = @IdRol", conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM RolPermiso WHERE IdRol = @IdRol", conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Rol WHERE IdRol = @IdRol", conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void AgregarFamilia(Rol_MB29 rol, Familia_MB29 familia)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "INSERT INTO RolFamilia (IdRol, IdFamilia) VALUES (@IdRol, @IdFamilia)";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol);
                cmd.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void AgregarPermiso(Rol_MB29 rol, Permiso_MB29 permiso)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "INSERT INTO RolPermiso (IdRol, IdPermiso) VALUES (@IdRol, @IdPermiso)";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol);
                cmd.Parameters.AddWithValue("@IdPermiso", permiso.Id);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public bool EstaEnUso(Rol_MB29 rol)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT COUNT(*) FROM Persona WHERE IdRol = @IdRol";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdRol", rol.IdRol);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conectar.Desconectar_MB29();
                return count > 0;
            }
        }
    }
}
