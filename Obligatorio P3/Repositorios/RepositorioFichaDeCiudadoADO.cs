using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorio;
using Dominio.EntidadesNegocio;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Repositorios
{
    public class RepositorioFichaDeCiudadoADO : IRepositorio<FichaDeCuidado>
    {

        private List<FichaDeCuidado> fichaDeCiudado;

        public bool Agregar(FichaDeCuidado unaF)
        {
            bool agregado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "INSERT INTO FichaDeCiudado VALUES(@cantidad,@unidadTiempo,@temperatura,@tipoI);";                  
            SqlCommand com = new SqlCommand(sql, con);
            FichaDeCuidado fBuscada = BuscarPorId(unaF.Id);

            if (unaF.SoyValido() && fBuscada == null)
            {
                com.Parameters.AddWithValue("@cantidad", unaF.Cantidad);
                com.Parameters.AddWithValue("@unidadTiempo", unaF.UnidadDeTiempo);
                com.Parameters.AddWithValue("@temperatura", unaF.Temperatura);
                com.Parameters.AddWithValue("@tipoI", unaF.TipoDeIluminacion.Id);
            }
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
                Conexion.CerrarYDisposeConexion(con);
            }

            return agregado;
        }

        public bool Eliminar(int id)
        {
              bool eliminado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "Delete from FichaDeCiudado where FichaDeCiudado_Id=@id";
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

        public bool Actualizar(FichaDeCuidado obj)
        {
            bool actualizado = false;
            SqlConnection con = Conexion.ObtenerConexion();
            FichaDeCuidado fichaBuscada = BuscarPorId(obj.Id);

            if (fichaBuscada == null || fichaBuscada.Id == obj.Id)
            {
                string sql = "UPDATE FichaDeCiudado SET FichaDeCiudado_Cantidad=@cant,FichaDeCiudado_UnidadDeTiempo=@unidad,FichaDeCiudado_Temperatura=@temp,FichaDeCiudado_TipoDeIluminacion=@tipoI WHERE FichaDeCiudado_Id=@id;";
                SqlCommand com = new SqlCommand(sql, con);

                com.Parameters.AddWithValue("@id", obj.Id);
                com.Parameters.AddWithValue("@cant", obj.Cantidad);
                com.Parameters.AddWithValue("@unidad", obj.UnidadDeTiempo);
                com.Parameters.AddWithValue("@temp", obj.Temperatura);
                com.Parameters.AddWithValue("@tipoI", obj.TipoDeIluminacion);

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

        public IEnumerable<FichaDeCuidado> EncontrarTodas()
        {
            List<FichaDeCuidado> fichasDeCuidados = new List<FichaDeCuidado>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "Select F.*, Tdi.TipoDeIluminacion_Nombre from FichaDeCiudado F left join TipoDeIluminacion Tdi " +  
                         "on F.FichaDeCiudado_TipoDeIluminacion = Tdi.TipoDeIluminacion_Id";
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    FichaDeCuidado ficha = new FichaDeCuidado()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("FichaDeCiudado_Id")),
                        Cantidad = reader.GetInt32(reader.GetOrdinal("FichaDeCiudado_Cantidad")),
                        UnidadDeTiempo = reader.GetString(reader.GetOrdinal("FichaDeCiudado_UnidadDeTiempo")),
                        Temperatura = reader.GetDecimal(reader.GetOrdinal("FichaDeCiudado_Temperatura"))
                    };
                    ficha.TipoDeIluminacion = CrearTipoDeIluminacion(reader);

                    fichasDeCuidados.Add(ficha);
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

            return fichasDeCuidados;
        }

        public FichaDeCuidado BuscarPorId(int id)
        {
            FichaDeCuidado fichaBuscada = null;

            SqlConnection con = Conexion.ObtenerConexion();
            string sql = "select f.*,Tdi.TipoDeIluminacion_Nombre from FichaDeCiudado F left join TipoDeIluminacion Tdi " + 
                         "on F.FichaDeCiudado_TipoDeIluminacion = Tdi.TipoDeIluminacion_Id " + 
                         "where F.FichaDeCiudado_Id=@id";
            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@id", id);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    fichaBuscada = new FichaDeCuidado()
                    {
                                                                                                                    // VER COMO MANEJAR OBJETOS CON SQL
                        Id = reader.GetInt32(reader.GetOrdinal("FichaDeCiudado_Id")),
                        Cantidad = reader.GetInt32(reader.GetOrdinal("FichaDeCiudado_Cantidad")),
                        UnidadDeTiempo = reader.GetString(reader.GetOrdinal("FichaDeCiudado_UnidadDeTiempo")),
                        Temperatura= reader.GetDecimal(reader.GetOrdinal("FichaDeCiudado_Temperatura"))
                    };
                    fichaBuscada.TipoDeIluminacion = CrearTipoDeIluminacion(reader);
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

            return fichaBuscada;
        }
            private TipoDeIluminacion CrearTipoDeIluminacion(SqlDataReader reader)
        {
            TipoDeIluminacion tI = new TipoDeIluminacion();
            tI.Id = reader.GetInt32(4);
            tI.Nombre = reader.GetString(5);

            return tI;
        }       

    }
}
