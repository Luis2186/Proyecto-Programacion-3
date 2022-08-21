using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Repositorios
{
    public class RepositorioTipoDePlantaADO : IRepositorioTipoDePlanta
    {
        private List<TipoDePlanta> tipoDePlantas;

        public bool Actualizar(TipoDePlanta tDP)
        {
            bool actualizado = false;
            SqlConnection con = Conexion.ObtenerConexion();
            TipoDePlanta tipoBuscadado = BuscarPorId(tDP.Id);

            string sql = "UPDATE TipoDePlanta SET TipoDePlanta_Nombre=@name, TipoDePlanta_Descripcion=@desc WHERE TipoDePlanta_Id=@id;";
            SqlCommand com = new SqlCommand(sql, con);

            if (tipoBuscadado.Id == tDP.Id && tDP.SoyValido())
            {
                com.Parameters.AddWithValue("@id", tDP.Id);
                com.Parameters.AddWithValue("@name", tDP.Nombre);
                com.Parameters.AddWithValue("@desc", tDP.Descripcion);
            }
            try
                {
                    Conexion.AbrirConexion(con);
                    int filasAfectadas = com.ExecuteNonQuery();
                    actualizado = filasAfectadas == 1;
                    Conexion.CerrarConexion(con);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    Conexion.CerrarConexion(con);
                }
            
            return actualizado;
        }

        public bool Agregar(TipoDePlanta tipoDP)
        {
            bool agregado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "INSERT INTO TipoDePlanta VALUES(@nombre, @descripcion);";
            SqlCommand com = new SqlCommand(sql, con);
            //TipoDePlanta pBuscada = BuscarPorNombre(tipoDP.Nombre);

                com.Parameters.AddWithValue("@nombre", tipoDP.Nombre);
                com.Parameters.AddWithValue("@descripcion", tipoDP.Descripcion);
                
            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = com.ExecuteNonQuery();
                agregado = filasAfectadas == 1;
                Conexion.CerrarConexion(con);
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarConexion(con);
            }

            return agregado;
        }

        public TipoDePlanta BuscarPorId(int id)
        {
            TipoDePlanta tipoBuscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM TipoDePlanta WHERE TipoDePlanta_Id=@id;";
            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@id", id);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    tipoBuscado = new TipoDePlanta()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("TipoDePlanta_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("TipoDePlanta_Nombre")),
                        Descripcion = reader.GetString(reader.GetOrdinal("TipoDePlanta_Descripcion")),
                    };
                }

                Conexion.CerrarConexion(con);
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarConexion(con);
            }

            return tipoBuscado;
        }

        public bool Eliminar(int id)
        {
            bool eliminado = false;
            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "DELETE FROM TipoDePlanta WHERE TipoDePlanta_Id=@id;";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@id", id);
          
            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = com.ExecuteNonQuery();
                eliminado = filasAfectadas == 1;
                Conexion.CerrarConexion(con);
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarConexion(con);
            }

            return eliminado;
        }

        public IEnumerable<TipoDePlanta> EncontrarTodas()
        {
            List<TipoDePlanta> tipoDePlantas = new List<TipoDePlanta>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM TipoDePlanta;";
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    TipoDePlanta tipoDePlanta = new TipoDePlanta()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("TipoDePlanta_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("TipoDePlanta_Nombre")),
                        Descripcion = reader.GetString(reader.GetOrdinal("TipoDePlanta_Descripcion")),
                    };
                    if (tipoDePlanta.SoyValido())
                    {
                        tipoDePlantas.Add(tipoDePlanta);
                    }
                }

                Conexion.CerrarConexion(con);
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarConexion(con);
            }

            return tipoDePlantas;
        }
        public TipoDePlanta BuscarPorNombre(string nombre)
        {
            TipoDePlanta tipoBuscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM TipoDePlanta WHERE TipoDePlanta_Nombre=@nombre;";
            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@nombre", nombre);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    tipoBuscado = new TipoDePlanta()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("TipoDePlanta_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("TipoDePlanta_Nombre")),
                        Descripcion = reader.GetString(reader.GetOrdinal("TipoDePlanta_Descripcion")),
                    };
                }

                Conexion.CerrarConexion(con);
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarConexion(con);
            }

            return tipoBuscado;
        }
        public bool EliminarPorNombre(string nombre)
        {
            bool eliminado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "DELETE FROM TipoDePlanta WHERE TipoDePlanta_Nombre=@name;";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@name", nombre);

            try
            {
                Conexion.AbrirConexion(con);
                int filasAfectadas = com.ExecuteNonQuery();
                eliminado = filasAfectadas == 1;
                Conexion.CerrarConexion(con);
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.CerrarConexion(con);
            }

            return eliminado;
        }

    }
}
