using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioTipoDePlanta : IRepositorio<TipoDePlanta>
    {
        TipoDePlanta BuscarPorNombre(string nombre);
        bool EliminarPorNombre(string nombre);
    }
}
