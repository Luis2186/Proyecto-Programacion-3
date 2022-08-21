using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;

namespace Repositorios
{
    public class RepositorioUsuarioADO : IRepositorioUsuario
    {
        public bool Actualizar(Usuario user)
        {
            bool actualizado = false;
            SqlConnection con = Conexion.ObtenerConexion();
            Usuario uBuscadado = BuscarPorId(user.Id);

            if (uBuscadado == null || uBuscadado.Id == user.Id)
            {
                string sql = "UPDATE Usuario SET Usuario_Nombre=@nombre,Usuario_Apellido=@apellido, Usuario_Email=@email,Usuario_Contraseña=@contraseña WHERE Usuario_Id=@id;";
                SqlCommand com = new SqlCommand(sql, con);

                com.Parameters.AddWithValue("@id", user.Id);
                com.Parameters.AddWithValue("@nombre", user.Nombre);
                com.Parameters.AddWithValue("@apellido", user.Apellido);
                com.Parameters.AddWithValue("@email", user.Email);
                com.Parameters.AddWithValue("@contraseña", user.Contraseña);

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

        public bool Agregar(Usuario user)
        {
            bool agregado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "INSERT INTO Usuario VALUES(@nombre, @apellido,@email,@contraseña);";
            SqlCommand com = new SqlCommand(sql, con);

            if (user.SoyValido()) 
            { 
                com.Parameters.AddWithValue("@nombre", user.Nombre);
                com.Parameters.AddWithValue("@apellido", user.Apellido);
                com.Parameters.AddWithValue("@email", user.Email);
                com.Parameters.AddWithValue("@contraseña", user.Contraseña);
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
                Conexion.CerrarConexion(con);
            }

            return agregado;
        }
    

        public Usuario BuscarPorId(int id)
        {
            Usuario uBuscado = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM Usuario WHERE Usuario_Id=@id;";
            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@id", id);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    uBuscado = new Usuario()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Usuario_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("Usuario_Nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("Usuario_Apellido")),
                        Email = reader.GetString(reader.GetOrdinal("Usuario_Email")),
                        Contraseña = reader.GetString(reader.GetOrdinal("Usuario_Contraseña"))
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

            return uBuscado;
        }

        public bool Eliminar(int id)
        {
            bool eliminado = false;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "DELETE FROM Usuario WHERE Usuario_Id=@id;";
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

        public IEnumerable<Usuario> EncontrarTodas()
        {
            List<Usuario> usuarios = new List<Usuario>();

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM Usuario;";
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    Usuario user = new Usuario()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Usuario_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("Usuario_Nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("Usuario_Apellido")),
                        Email = reader.GetString(reader.GetOrdinal("Usuario_Email")),
                        Contraseña = reader.GetString(reader.GetOrdinal("Usuario_Contraseña"))
                    };

                    if (user.SoyValido())
                    {
                        usuarios.Add(user);
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

            return usuarios;
        }

        public Usuario Login(string email, string contraseña)
        {
            Usuario user = null;

            SqlConnection con = Conexion.ObtenerConexion();

            string sql = "SELECT * FROM Usuario WHERE Usuario_Email=@correo and Usuario_Contraseña=@contra;";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@correo", email == null ? email= "" : email.Trim());
            com.Parameters.AddWithValue("@contra", contraseña == null ? contraseña = "" : contraseña.Trim());

            try
            {
                Conexion.AbrirConexion(con);
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read()) 
                {
                    user = new Usuario()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Usuario_Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("Usuario_Nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("Usuario_Apellido")),
                        Email= reader.GetString(reader.GetOrdinal("Usuario_Email")),
                        Contraseña= reader.GetString(reader.GetOrdinal("Usuario_Contraseña"))
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

            return user;
        }
      
    }
}
