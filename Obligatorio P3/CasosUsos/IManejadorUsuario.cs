using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
namespace CasosUsos
{
    public interface IManejadorUsuario
    {
        public Usuario IniciarSesion(string email, string contraseña);
        public bool AgregarNuevoUsuario(Usuario user);
        public Usuario BuscarUsuarioPorId(int id);
    }
}
