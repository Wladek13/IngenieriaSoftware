using BE;
using DAL_MB29;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class BitacoraDAL_MB29
    {
       
        
            public void Guardar(BitacoraBE_MB29 bitacora)
            {
                var conectar = new ConexionDB_MB29();
                var conexion = conectar.Conectar_MB29();

                string query = @"INSERT INTO Bitacora (usuario, accion, modulo, fecha, descripcion,criticidad)
                         VALUES (@usuario, @accion, @modulo, @fecha, @descripcion,@criticidad)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", bitacora.usuario);
                    comando.Parameters.AddWithValue("@Accion", bitacora.accion);
                    comando.Parameters.AddWithValue("@Modulo", bitacora.modulo);
                    comando.Parameters.AddWithValue("@Fecha", bitacora.fecha);
                    comando.Parameters.AddWithValue("@Descripcion", bitacora.Descripcion);
                    comando.Parameters.AddWithValue("@Criticidad", bitacora.Criticidad);
                    comando.ExecuteNonQuery();
                }

                conectar.Desconectar_MB29();
            }
        
    }
}
