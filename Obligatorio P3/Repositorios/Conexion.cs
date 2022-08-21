using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Repositorios
{
    public class Conexion
    {

        //public static string StringConexion { get; set; } = @"Data Source=DESKTOP-RO4GBHV; Initial Catalog=Obligatoriop3; Integrated Security=SSPI; Encrypt=false";

        public static string ObtenerStringConexion()
        {
            string con = "";
            ConfigurationBuilder cb = new ConfigurationBuilder();
            cb.AddJsonFile("appsettings.json");
            IConfiguration configuracion = cb.Build();

            con = configuracion.GetConnectionString("ConexionSQL");

            return con;
        }

        public static SqlConnection ObtenerConexion()
        {
            string con = ObtenerStringConexion();
            return new SqlConnection(con);
        }

        public static void AbrirConexion(SqlConnection con)
        {
            if (con != null && con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public static void CerrarYDisposeConexion(SqlConnection con)
        {
            CerrarConexion(con);
            con.Dispose();
        }

        public static void CerrarConexion(SqlConnection con)
        {
            if (con != null && con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }



    }
}
