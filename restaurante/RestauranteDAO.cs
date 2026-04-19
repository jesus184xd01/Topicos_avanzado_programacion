using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace restaurante
{
    public class RestauranteDAO
    {
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings[
                ConfigurationManager.AppSettings["Ambiente"]
            ].ConnectionString;

        // ── Categorias: devuelve el id según nombre ───────────────
        private int ObtenerCategoriaId(string tipoDia)
        {
            switch (tipoDia.ToLower())
            {
                case "desayuno": return 1;
                case "almuerzo": return 2;
                case "comida": return 3;
                case "cena": return 4;
                default: return 1;
            }
        }

        // ── Obtener platillos por tipo de día ─────────────────────
        public List<Platillo> ObtenerPlatillos(string tipoDia)
        {
            var lista = new List<Platillo>();
            int categoriaId = ObtenerCategoriaId(tipoDia);

            string query = @"
                SELECT platillo_id, categoria_id, nombre, descripcion,
                       precio, es_vegetariano, disponible, imagen_url
                FROM   platillos
                WHERE  categoria_id = @categoriaId
                AND    disponible   = 1";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Platillo
                            {
                                PlatilloId = Convert.ToInt32(reader["platillo_id"]),
                                CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                EsVegetariano = Convert.ToBoolean(reader["es_vegetariano"]),
                                Disponible = Convert.ToBoolean(reader["disponible"]),
                                ImagenUrl = reader["imagen_url"] == DBNull.Value ? null : reader["imagen_url"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar platillos:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return lista;
        }

        // ── Obtener bebidas por tipo de día ───────────────────────
        public List<Bebida> ObtenerBebidas(string tipoDia)
        {
            var lista = new List<Bebida>();
            int categoriaId = ObtenerCategoriaId(tipoDia);

            string query = @"
                SELECT bebida_id, categoria_id, nombre, descripcion,
                       capacidad, precio, imagen_url
                FROM   bebidas
                WHERE  categoria_id = @categoriaId";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Bebida
                            {
                                BebidaId = Convert.ToInt32(reader["bebida_id"]),
                                CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Capacidad = reader["capacidad"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                ImagenUrl = reader["imagen_url"] == DBNull.Value ? null : reader["imagen_url"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar bebidas:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return lista;
        }

        // ── Obtener postres por tipo de día ───────────────────────
        public List<Postre> ObtenerPostres(string tipoDia)
        {
            var lista = new List<Postre>();
            int categoriaId = ObtenerCategoriaId(tipoDia);

            string query = @"
                SELECT postre_id, categoria_id, nombre, descripcion,
                       precio, disponible, imagen_url
                FROM   postres
                WHERE  categoria_id = @categoriaId
                AND    disponible   = 1";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Postre
                            {
                                PostreId = Convert.ToInt32(reader["postre_id"]),
                                CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Disponible = Convert.ToBoolean(reader["disponible"]),
                                ImagenUrl = reader["imagen_url"] == DBNull.Value ? null : reader["imagen_url"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar postres:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return lista;
        }
    }
}