using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Repositorios
{
    public class RepositorioTipoDeIluminacionADO : IRepositorio<TipoDeIluminacion>
    {
        public bool Actualizar(TipoDeIluminacion tdi)
        {
            bool actualizado = false;
            SqlConnection con = Conexion.ObtenerConexion();
            TipoDeIluminacion iluminacionBuscada = BuscarPorId(tdi.Id);

            if (iluminacionBuscada == null || iluminacionBuscada.Id == tdi.Id)
            {
                string sql = "UPDATE TipoDeIluminacion SET TipoDeIluminacion_Nombre=@name WHERE TipoDeIluminacion_Id=@id;";
                SqlCommand com = new SqlCommand(sql, con);

                com.Parameters.AddWithValue("@id", tdi.Id);
                com.Parameters.AddWithValue("@name", tdi.Nombre);

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
            }

            return actualizado;
        }

        public bool Agregar(TipoDeIluminacion tdi)
        {
            bool agregado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "INSERT INTO TipoDeIluminacion VALUES(@nombre);";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@nombre", tdi.Nombre);

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
        public bool Eliminar(int id)
        {
            bool eliminado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "DELETE FROM TipoDeIluminacion WHERE TipoDeIluminacion_Id=@id;";
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

        public TipoDeIluminacion BuscarPorId(int id)
        {
            TipoDeIluminacion iluminacionBuscada = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "select * from TipoDeIluminacion where TipoDeIluminacion_Id=@id;";
            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@id", id);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    iluminacionBuscada = new TipoDeIluminacion()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("TipoDeIluminacion_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("TipoDeIluminacion_Nombre")),
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

            return iluminacionBuscada;
        }

        public IEnumerable<TipoDeIluminacion> EncontrarTodas()
        {
            List<TipoDeIluminacion> iluminaciones = new List<TipoDeIluminacion>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM TipoDeIluminacion;";
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    TipoDeIluminacion iluminacion = new TipoDeIluminacion()
                    {
                      
                        Id = reader.GetInt32(reader.GetOrdinal("TipoDeIluminacion_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("TipoDeIluminacion_Nombre")),
                    };
                    if (iluminacion.SoyValido()) 
                    { 
                    iluminaciones.Add(iluminacion);
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

            return iluminaciones;
        }
    }
}
