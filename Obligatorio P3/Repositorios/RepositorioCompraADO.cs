using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;

namespace Repositorios
{
    public class RepositorioCompraADO : IRepositorio<Compra>
    {
        public bool Actualizar(Compra obj)
        {
            throw new NotImplementedException();
        }

        public bool Agregar(Compra obj)
        {
            throw new NotImplementedException();
        }

        public Compra BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Compra> EncontrarTodas()
        {
            throw new NotImplementedException();
        }
    }
}
