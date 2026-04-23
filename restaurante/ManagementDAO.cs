using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace restaurante
{
    public class ManagementDAO
    {
        private static readonly string _conn =
            ConfigurationManager.ConnectionStrings[
                ConfigurationManager.AppSettings["Ambiente"]
            ].ConnectionString;

        // ── Helper: obtener categoria_id desde nombre ─────────────
        private int ObtenerCategoriaId(string categoria)
        {
            switch (categoria.ToLower())
            {
                case "desayuno": return 1;
                case "almuerzo": return 2;
                case "comida": return 3;
                case "cena": return 4;
                default: return 1;
            }
        }

        // ══════════════════════════════════════════════════════════
        //  CARGAR GRID
        // ══════════════════════════════════════════════════════════
        public DataTable CargarGrid(string tipo, string categoria)
        {
            int categoriaId = ObtenerCategoriaId(categoria);
            string query = "";

            switch (tipo)
            {
                case "Platillo":
                    query = @"SELECT platillo_id   AS ID,
                                     nombre        AS Nombre,
                                     descripcion   AS Descripcion,
                                     precio        AS Precio,
                                     es_vegetariano AS Vegetariano,
                                     disponible    AS Disponible,
                                     imagen_url    AS Imagen
                              FROM   platillos
                              WHERE  categoria_id = @categoriaId
                              ORDER  BY nombre";
                    break;

                case "Bebida":
                    query = @"SELECT bebida_id   AS ID,
                                     nombre      AS Nombre,
                                     descripcion AS Descripcion,
                                     capacidad   AS Capacidad,
                                     precio      AS Precio,
                                     imagen_url  AS Imagen
                              FROM   bebidas
                              WHERE  categoria_id = @categoriaId
                              ORDER  BY nombre";
                    break;

                case "Postre":
                    query = @"SELECT postre_id    AS ID,
                                     nombre       AS Nombre,
                                     descripcion  AS Descripcion,
                                     precio       AS Precio,
                                     disponible   AS Disponible,
                                     imagen_url   AS Imagen
                              FROM   postres
                              WHERE  categoria_id = @categoriaId
                              ORDER  BY nombre";
                    break;
            }

            var dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                    conn.Open();
                    using (var adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar registros:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // ══════════════════════════════════════════════════════════
        //  INSERT
        // ══════════════════════════════════════════════════════════
        public bool InsertarPlatillo(string nombre, string descripcion,
            decimal precio, bool esVegetariano, bool disponible,
            string imagenUrl, string categoria)
        {
            string query = @"INSERT INTO platillos
                                (categoria_id, nombre, descripcion,
                                 precio, es_vegetariano, disponible, imagen_url)
                             VALUES
                                (@categoriaId, @nombre, @descripcion,
                                 @precio, @esVegetariano, @disponible, @imagenUrl)";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", ObtenerCategoriaId(categoria));
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@esVegetariano", esVegetariano);
                    cmd.Parameters.AddWithValue("@disponible", disponible);
                    cmd.Parameters.AddWithValue("@imagenUrl", (object)imagenUrl ?? DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar platillo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool InsertarBebida(string nombre, string descripcion,
            string capacidad, decimal precio, string imagenUrl, string categoria)
        {
            string query = @"INSERT INTO bebidas
                                (categoria_id, nombre, descripcion,
                                 capacidad, precio, imagen_url)
                             VALUES
                                (@categoriaId, @nombre, @descripcion,
                                 @capacidad, @precio, @imagenUrl)";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", ObtenerCategoriaId(categoria));
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@capacidad", capacidad);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@imagenUrl", (object)imagenUrl ?? DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar bebida:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool InsertarPostre(string nombre, string descripcion,
            decimal precio, bool disponible, string imagenUrl, string categoria)
        {
            string query = @"INSERT INTO postres
                                (categoria_id, nombre, descripcion,
                                 precio, disponible, imagen_url)
                             VALUES
                                (@categoriaId, @nombre, @descripcion,
                                 @precio, @disponible, @imagenUrl)";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@categoriaId", ObtenerCategoriaId(categoria));
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@disponible", disponible);
                    cmd.Parameters.AddWithValue("@imagenUrl", (object)imagenUrl ?? DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar postre:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ══════════════════════════════════════════════════════════
        //  DELETE
        // ══════════════════════════════════════════════════════════
        public bool Eliminar(string tipo, int id)
        {
            string tabla = tipo == "Platillo" ? "platillos"
                         : tipo == "Bebida" ? "bebidas"
                         : "postres";

            string pk = tipo == "Platillo" ? "platillo_id"
                      : tipo == "Bebida" ? "bebida_id"
                      : "postre_id";

            string query = $"DELETE FROM {tabla} WHERE {pk} = @id";

            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // ══════════════════════════════════════════════════════════
        //  UPDATE
        // ══════════════════════════════════════════════════════════
        public bool ActualizarPlatillo(int id, string nombre, string descripcion,
            decimal precio, bool esVegetariano, bool disponible, string imagenUrl)
        {
            string query = @"UPDATE platillos SET
                        nombre        = @nombre,
                        descripcion   = @descripcion,
                        precio        = @precio,
                        es_vegetariano = @esVegetariano,
                        disponible    = @disponible,
                        imagen_url    = @imagenUrl
                     WHERE platillo_id = @id";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@esVegetariano", esVegetariano);
                    cmd.Parameters.AddWithValue("@disponible", disponible);
                    cmd.Parameters.AddWithValue("@imagenUrl", (object)imagenUrl ?? DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar platillo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ActualizarBebida(int id, string nombre, string descripcion,
            string capacidad, decimal precio, string imagenUrl)
        {
            string query = @"UPDATE bebidas SET
                        nombre      = @nombre,
                        descripcion = @descripcion,
                        capacidad   = @capacidad,
                        precio      = @precio,
                        imagen_url  = @imagenUrl
                     WHERE bebida_id = @id";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@capacidad", capacidad);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@imagenUrl", (object)imagenUrl ?? DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar bebida:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ActualizarPostre(int id, string nombre, string descripcion,
            decimal precio, bool disponible, string imagenUrl)
        {
            string query = @"UPDATE postres SET
                        nombre      = @nombre,
                        descripcion = @descripcion,
                        precio      = @precio,
                        disponible  = @disponible,
                        imagen_url  = @imagenUrl
                     WHERE postre_id = @id";
            try
            {
                using (var conn = new SqlConnection(_conn))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@disponible", disponible);
                    cmd.Parameters.AddWithValue("@imagenUrl", (object)imagenUrl ?? DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar postre:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}