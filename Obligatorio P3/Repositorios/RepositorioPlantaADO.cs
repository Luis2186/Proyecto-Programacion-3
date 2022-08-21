using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Repositorios
{
    public class RepositorioPlantaADO : IRepositorioPlanta
    {
        public bool Actualizar(Planta unaP)
        {
            bool actualizado = false;
            SqlConnection con = Conexion.ObtenerConexion();
            Planta plantaBuscada = BuscarPorId(unaP.Id);

            if (plantaBuscada == null || plantaBuscada.Id == unaP.Id && unaP.SoyValido()) { 
            
                string strSQL= "UPDATE Planta SET Planta_Tipo=@tipo, Planta_NombreCientifico=@nomCien, Planta_NombresVulgares=@nomVulg, Planta_Descripcion=@pDesc, Planta_Ambientes=@pAmbientes,Planta_AlturaMaxima=@pAltMax,Planta_Foto=@pFoto,Planta_FichaDeCiudados=@pFiCuidados,Planta_Precio=pPrecio WHERE Planta_Id=@id";
                SqlCommand com = new SqlCommand(strSQL,con);

                com.Parameters.AddWithValue("@tipo", unaP.Tipo.Id);
                com.Parameters.AddWithValue("@nomCien", unaP.NombreCientifico);
                com.Parameters.AddWithValue("@nomVulg", unaP.NombresVulgares);
                com.Parameters.AddWithValue("@pDesc", unaP.Descripcion);
                com.Parameters.AddWithValue("@pAmbientes", unaP.Ambiente);
                com.Parameters.AddWithValue("@pAltMax", unaP.AlturaMaxima);
                com.Parameters.AddWithValue("pFoto", unaP.Foto);
                com.Parameters.AddWithValue("@pFiCuidados", unaP.FichaDeCuidados.Id);
                com.Parameters.AddWithValue("@pPrecio", unaP.Precio);

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
        public bool Agregar(Planta unaP)
        {
            bool agregado = false;
            SqlConnection con = Conexion.ObtenerConexion();
            Planta plantaBuscada = BuscarNombreCientifico(unaP.NombreCientifico);
            string sqlFicha =   "INSERT INTO FichaDeCiudado VALUES(@cantidad,@unidadTiempo,@temperatura,@tipoI);" +
                                "SELECT CAST(SCOPE_IDENTITY() AS INT);";
            string sqlPlanta = "INSERT INTO Planta VALUES(@tipo,@nomCien,@pDesc,@pAmbientes,@pAltMax,@pFoto,@pFiCuidados,@pPrecio);" +
                                "SELECT CAST(SCOPE_IDENTITY() AS INT);";
            string sqlNombresV = "INSERT INTO ListaNVulgares VALUES(@idPlanta,@nombre);";
            SqlCommand com = new SqlCommand(sqlFicha, con);
            SqlTransaction tran = null;

            try
            {
                Conexion.AbrirConexion(con);
                tran = con.BeginTransaction();
                com.Transaction = tran;

                if (plantaBuscada == null)
                {
                    com.Parameters.AddWithValue("@cantidad", unaP.FichaDeCuidados.Cantidad);
                    com.Parameters.AddWithValue("@unidadTiempo", unaP.FichaDeCuidados.UnidadDeTiempo);
                    com.Parameters.AddWithValue("@temperatura", unaP.FichaDeCuidados.Temperatura);
                    com.Parameters.AddWithValue("@tipoI", unaP.FichaDeCuidados.TipoDeIluminacion.Id);

                    int idFichaPlanta = (int)com.ExecuteScalar();
                    com.Parameters.Clear();

                    com.Parameters.AddWithValue("@tipo", unaP.Tipo.Id);
                    com.Parameters.AddWithValue("@nomCien", unaP.NombreCientifico);
                    com.Parameters.AddWithValue("@pDesc", unaP.Descripcion);
                    com.Parameters.AddWithValue("@pAmbientes", unaP.Ambiente);
                    com.Parameters.AddWithValue("@pAltMax", unaP.AlturaMaxima);
                    com.Parameters.AddWithValue("@pFoto", unaP.Foto);
                    com.Parameters.AddWithValue("@pFiCuidados", idFichaPlanta);
                    com.Parameters.AddWithValue("@pPrecio", unaP.Precio);
                    com.CommandText = sqlPlanta;

                    int idPlanta = (int)com.ExecuteScalar();

                    foreach (string nombreVulgar in unaP.NombresVulgares)
                    {
                        com.Parameters.Clear();
                        com.Parameters.AddWithValue("@idPlanta", idPlanta);
                        com.Parameters.AddWithValue("@nombre", nombreVulgar);
                        com.CommandText = sqlNombresV;
                        com.ExecuteNonQuery();
                    }                
                }
                tran.Commit();
                agregado = true;
                Conexion.CerrarConexion(con);
            }
            catch 
            {
                if (tran != null) tran.Rollback();
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
            string strSQL = "DELETE FROM Planta Where Planta_Id=@id";
            SqlCommand com = new SqlCommand(strSQL, con);

            com.Parameters.AddWithValue("@id",id);

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
        public Planta BuscarPorId(int id)
        {
            Planta plantaBuscada = null;
            SqlConnection con = Conexion.ObtenerConexion();
            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo " +
                            "where Planta_Id=@id";
            SqlCommand com = new SqlCommand(strSQL, con);

            com.Parameters.AddWithValue("@id", id);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    plantaBuscada = CrearPlanta(reader);
                    plantaBuscada.Tipo = CrearTipoDePlanta(reader);
                    plantaBuscada.NombresVulgares = BuscarTodosNombresVulgares(plantaBuscada.Id);
                    plantaBuscada.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    plantaBuscada.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);
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
            return plantaBuscada;
        }
        public IEnumerable<Planta> EncontrarTodas()
        {
            List<Planta> plantas = new List<Planta>();

            SqlConnection con = Conexion.ObtenerConexion();
            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo";
            SqlCommand com = new SqlCommand(strSQL,con);

                try
                {
                    Conexion.AbrirConexion(con);
                    SqlDataReader reader = com.ExecuteReader();

                    while (reader.Read()) 
                    {
                    Planta p = CrearPlanta(reader);
                    p.Tipo = CrearTipoDePlanta(reader);
                    p.NombresVulgares = BuscarTodosNombresVulgares(p.Id);          
                    p.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    p.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);

                    if (p.SoyValido())
                        {
                            plantas.Add(p);
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

            return plantas;
        }

        public IEnumerable<Planta> BuscarPorTextoEnNombre(string nombreBuscado)
        {
            List<Planta> plantas = new List<Planta>();

            SqlConnection con = Conexion.ObtenerConexion();
            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo " +
                            "Left join ListaNVulgares as Lnv on P.Planta_Id = Lnv.Planta_Id " +
                            "where P.Planta_NombreCientifico like @nombre " +
                            "or Lnv.NombreVulgar like @nombre";

            SqlCommand com = new SqlCommand(strSQL, con);
            com.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar);
            com.Parameters["@nombre"].Value = "%" + nombreBuscado + "%";// Los % hacen que traiga los resultados si encuentra nombrebuscado en la cadena 

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Planta p = CrearPlanta(reader);
                    p.Tipo = CrearTipoDePlanta(reader);
                    p.NombresVulgares = BuscarTodosNombresVulgares(p.Id);
                    p.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    p.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);

                    if (p.SoyValido())
                    {
                        plantas.Add(p);
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

            return plantas;
        }
        public IEnumerable<Planta> BuscarPorAmbiente(string tipoDeAmbiente)
        {
            List<Planta> plantas = new List<Planta>();

            SqlConnection con = Conexion.ObtenerConexion();
            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo " +
                            "where P.Planta_Ambientes=@ambiente";
            SqlCommand com = new SqlCommand(strSQL, con);

            com.Parameters.AddWithValue("@ambiente", tipoDeAmbiente);


            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Planta p = CrearPlanta(reader);
                    p.Tipo = CrearTipoDePlanta(reader);
                    p.NombresVulgares = BuscarTodosNombresVulgares(p.Id);
                    p.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    p.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);

                    if (p.SoyValido())
                    {
                        plantas.Add(p);
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
            return plantas;
        }
        public IEnumerable<Planta> BuscarPorTipo(int id)
        {
            List<Planta> plantas = new List<Planta>();

            SqlConnection con = Conexion.ObtenerConexion();
            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo " +
                            "where P.Planta_Tipo="+id;
            SqlCommand com = new SqlCommand(strSQL, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Planta p = CrearPlanta(reader);
                    p.Tipo = CrearTipoDePlanta(reader);
                    p.NombresVulgares = BuscarTodosNombresVulgares(p.Id);
                    p.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    p.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);

                    if (p.SoyValido())
                    {
                        plantas.Add(p);
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
            return plantas;
        }
        public IEnumerable<Planta> PlantasConMismaAlturaOMas(int AlturaLimite)
        {
            List<Planta> plantas = new List<Planta>();

            SqlConnection con = Conexion.ObtenerConexion();
            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo " +
                            "where P.Planta_AlturaMaxima >=" + AlturaLimite;
            
            SqlCommand com = new SqlCommand(strSQL, con);
           

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Planta p = CrearPlanta(reader);
                    p.Tipo = CrearTipoDePlanta(reader);
                    p.NombresVulgares = BuscarTodosNombresVulgares(p.Id);
                    p.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    p.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);

                    if (p.SoyValido())
                    {
                        plantas.Add(p);
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
            return plantas;
        }
        public IEnumerable<Planta> PlantasMasBajas(int AlturaLimite)
        {
            List<Planta> plantas = new List<Planta>();

            SqlConnection con = Conexion.ObtenerConexion();
            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo " +
                            "where P.Planta_AlturaMaxima <" + AlturaLimite;
            SqlCommand com = new SqlCommand(strSQL, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Planta p = CrearPlanta(reader);
                    p.Tipo = CrearTipoDePlanta(reader);
                    p.NombresVulgares = BuscarTodosNombresVulgares(p.Id);
                    p.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    p.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);

                    if (p.SoyValido())
                    {
                        plantas.Add(p);
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
            return plantas;
        }
        public Planta BuscarNombreCientifico(string nombreCientifico)
        {
            Planta plantaBuscada = null;
            SqlConnection con = Conexion.ObtenerConexion();

            string strSQL = "Select P.*, F.FichaDeCiudado_Cantidad,F.FichaDeCiudado_UnidadDeTiempo,F.FichaDeCiudado_Temperatura, Ti.*,TP.TipoDePlanta_Nombre,TP.TipoDePlanta_Descripcion from Planta as P " +
                            "Left join FichaDeCiudado as F on P.Planta_FichaDeCiudados = F.FichaDeCiudado_Id " +
                            "Left join TipoDeIluminacion as TI on Ti.TipoDeIluminacion_Id = FichaDeCiudado_TipoDeIluminacion " +
                            "Left join TipoDePlanta as TP on Tp.TipoDePlanta_Id = P.Planta_Tipo " +
                            "where p.Planta_NombreCientifico=@nombreC ";
            SqlCommand com = new SqlCommand(strSQL, con);

            com.Parameters.AddWithValue("@nombreC", nombreCientifico);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    plantaBuscada = CrearPlanta(reader);
                    plantaBuscada.Tipo = CrearTipoDePlanta(reader);
                    plantaBuscada.NombresVulgares = BuscarTodosNombresVulgares(plantaBuscada.Id);
                    plantaBuscada.FichaDeCuidados = CrearFichaDeCuidado(reader);
                    plantaBuscada.FichaDeCuidados.TipoDeIluminacion = CrearTipoDeIluminacion(reader);
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
            return plantaBuscada;
        }

        #region SQLReader
        private Planta CrearPlanta(SqlDataReader reader)
        {

            Planta planta = new Planta();

            planta.Id = reader.GetInt32(0);        
            planta.NombreCientifico = reader.GetString(2);
            planta.Descripcion = reader.GetString(3);
            planta.Ambiente = reader.GetString(4);
            planta.AlturaMaxima = reader.GetInt32(5);
            planta.Foto= reader.GetString(6);
            planta.Precio = reader.GetDecimal(8);

            return planta;
        }
        private TipoDePlanta CrearTipoDePlanta(SqlDataReader reader)
        {
            TipoDePlanta tPlanta = new TipoDePlanta();
            tPlanta.Id = reader.GetInt32(1);
            tPlanta.Nombre = reader.GetString(14);
            tPlanta.Descripcion = reader.GetString(15);

            return tPlanta;
        }
    
        private FichaDeCuidado CrearFichaDeCuidado(SqlDataReader reader)
        {
            FichaDeCuidado fC = new FichaDeCuidado();
            fC.Id = reader.GetInt32(7);
            fC.Cantidad = reader.GetInt32(9);
            fC.UnidadDeTiempo = reader.GetString(10);
            fC.Temperatura = reader.GetDecimal(11);
            return fC;
        }
        private TipoDeIluminacion CrearTipoDeIluminacion(SqlDataReader reader)
        {
            TipoDeIluminacion tI = new TipoDeIluminacion();
            tI.Id = reader.GetInt32(12);
            tI.Nombre = reader.GetString(13);

            return tI;
        }
        public List<string> BuscarTodosNombresVulgares(int idPlanta) 
        {
            List<string> nombreV = new List<string>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM ListaNVulgares WHERE Planta_Id=" + idPlanta;
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    string nombre = reader.GetString(2);                   
                    nombreV.Add(nombre);
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

            return nombreV;
        }

        #endregion  
       
     

    }
}
