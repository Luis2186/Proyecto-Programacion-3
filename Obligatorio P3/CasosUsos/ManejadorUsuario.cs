using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;

namespace CasosUsos
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        public IRepositorioUsuario User { get; set; }

        public ManejadorUsuario(IRepositorioUsuario usuario) 
        {
            User = usuario;
        }

        public bool AgregarNuevoUsuario(Usuario user)
        {
            bool agregado = false;

            if (user.SoyValido()) 
            {
                agregado = true;
                User.Agregar(user);
            }

            return agregado;
        }
        public Usuario IniciarSesion(string email, string contraseña)
        {
            Usuario uBuscado=User.Login(email, contraseña);
            if (!uBuscado.SoyValido())
            {
                uBuscado = null;
            }
            return uBuscado;
        }
        public Usuario BuscarUsuarioPorId(int id)
        {
            Usuario uBuscado = User.BuscarPorId(id);
            if (!uBuscado.SoyValido())
            {
                uBuscado = null;
            }
            return uBuscado;
        }
    }
}
