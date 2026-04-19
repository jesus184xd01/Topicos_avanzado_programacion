using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace restaurante
{
    public class AlimentoDAO
    {
        private string connectionString =
            "Server=TU_SERVIDOR;" +
            "Database=TU_BASE_DE_DATOS;" +
            "User Id=TU_USUARIO;" +
            "Password=TU_CONTRASEÑA;";

        public List<Alimento> ObtenerPorCategoria(string categoria, string tipoDia)
        {
            List<Alimento> lista = new List<Alimento>();

            string query = @"
                SELECT id, nombre, descripcion, precio, categoria, tipo_dia, imagen
                FROM alimentos
                WHERE categoria = @categoria
                AND   tipo_dia  = @tipoDia";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@categoria", categoria);
                        cmd.Parameters.AddWithValue("@tipoDia", tipoDia);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Alimento
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Descripcion = reader["descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(reader["precio"]),
                                    Categoria = reader["categoria"].ToString(),
                                    TipoDia = reader["tipo_dia"].ToString(),
                                    Imagen = reader["imagen"] as byte[]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al conectar con la base de datos:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return lista;
        }
    }
}