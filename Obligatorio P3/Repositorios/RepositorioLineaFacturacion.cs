using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;

namespace Repositorios
{
    public class RepositorioLineaFacturacion : IRepositorio<LineaFacturacion>
    {
        public bool Actualizar(LineaFacturacion obj)
        {
            throw new NotImplementedException();
        }

        public bool Agregar(LineaFacturacion obj)
        {
            throw new NotImplementedException();
        }

        public LineaFacturacion BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineaFacturacion> EncontrarTodas()
        {
            throw new NotImplementedException();
        }
    }
}
