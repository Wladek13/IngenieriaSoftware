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
   public class FamiliaDAL_MB29
    {
        public List<Familia_MB29> ObtenerTodas()
        {
            var familias = new List<Familia_MB29>();
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "SELECT IdFamilia, Nombre FROM Familia";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    familias.Add(new Familia_MB29
                    {
                        IdFamilia = Convert.ToInt32(reader["IdFamilia"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }

            conectar.Desconectar_MB29();

            // Cargar hijos (permisos directos y subfamilias)
            var permisoDAL = new PermisoDAL_MB29();
            var todos = new Dictionary<int, Familia_MB29>();
            foreach (var f in familias) todos[f.IdFamilia] = f;

            CargarHijos(familias, todos, permisoDAL, conexion);

            return familias;
        }

        private void CargarHijos(
            List<Familia_MB29> familias,
            Dictionary<int, Familia_MB29> todas,
            PermisoDAL_MB29 permisoDAL,
            SqlConnection conexion)
        {
            // Permisos directos de cada familia
            var conectar2 = new ConexionDB_MB29();
            var conn2 = conectar2.Conectar_MB29();

            string queryPermisos = @"
                SELECT fp.IdFamilia, p.IdPermiso, p.Nombre
                FROM FamiliaPermiso fp
                INNER JOIN Permiso p ON fp.IdPermiso = p.IdPermiso";

            using (SqlCommand cmd = new SqlCommand(queryPermisos, conn2))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idFamilia = Convert.ToInt32(reader["IdFamilia"]);
                    if (todas.TryGetValue(idFamilia, out var familia))
                    {
                        familia.Hijos.Add(new Permiso_MB29
                        {
                            Id = Convert.ToInt32(reader["IdPermiso"]),
                            Nombre = reader["Nombre"].ToString()
                        });
                    }
                }
            }

            // Subfamilias
            string querySubfamilias = @"
                SELECT IdFamiliaPadre, IdFamiliaHija
                FROM FamiliaFamilia";

            using (SqlCommand cmd = new SqlCommand(querySubfamilias, conn2))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idPadre = Convert.ToInt32(reader["IdFamiliaPadre"]);
                    int idHija = Convert.ToInt32(reader["IdFamiliaHija"]);
                    if (todas.TryGetValue(idPadre, out var padre) &&
                        todas.TryGetValue(idHija, out var hija))
                    {
                        padre.Hijos.Add(hija);
                    }
                }
            }

            conectar2.Desconectar_MB29();
        }

        public void Guardar(Familia_MB29 familia)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"INSERT INTO Familia (Nombre)
                             VALUES (@Nombre);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@Nombre", familia.Nombre);
                familia.IdFamilia = Convert.ToInt32(cmd.ExecuteScalar());
            }

            conectar.Desconectar_MB29();
        }

        public void Eliminar(Familia_MB29 familia)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            // Eliminar relaciones primero
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM FamiliaPermiso WHERE IdFamilia = @Id", conexion))
            {
                cmd.Parameters.AddWithValue("@Id", familia.IdFamilia);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM FamiliaFamilia WHERE IdFamiliaPadre = @Id OR IdFamiliaHija = @Id",
                conexion))
            {
                cmd.Parameters.AddWithValue("@Id", familia.IdFamilia);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Familia WHERE IdFamilia = @Id", conexion))
            {
                cmd.Parameters.AddWithValue("@Id", familia.IdFamilia);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void AgregarPermiso(Familia_MB29 familia, Permiso_MB29 permiso)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "INSERT INTO FamiliaPermiso (IdFamilia, IdPermiso) VALUES (@IdFamilia, @IdPermiso)";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia);
                cmd.Parameters.AddWithValue("@IdPermiso", permiso.Id);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public void EliminarPermiso(Familia_MB29 familia, Permiso_MB29 permiso)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = "DELETE FROM FamiliaPermiso WHERE IdFamilia = @IdFamilia AND IdPermiso = @IdPermiso";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia);
                cmd.Parameters.AddWithValue("@IdPermiso", permiso.Id);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public List<Rol_MB29> ObtenerRolesQueUsanFamilia(int idFamilia)
        {
            var roles = new List<Rol_MB29>();
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"SELECT r.IdRol, r.Nombre FROM Rol r
                             INNER JOIN RolFamilia rf ON r.IdRol = rf.IdRol
                             WHERE rf.IdFamilia = @IdFamilia";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
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
            }

            conectar.Desconectar_MB29();
            return roles;
        }
    }
}
