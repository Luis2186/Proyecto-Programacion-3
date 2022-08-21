using System;
using System.Collections.Generic;
using System.Text;
using Repositorios;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;

namespace CasosUsos
{
    class ManejadorCompras : IManejadorCompras
    {
        public IRepositorio<Compra> _repoCompra { get; set; }
        public IRepositorio<LineaFacturacion> _repoLineaFact { get; set; }
             

        public ManejadorCompras(IRepositorio<Compra> repoCompra, IRepositorio<LineaFacturacion> lineaFact)
        { 
            _repoCompra = repoCompra;
            _repoLineaFact = lineaFact;
        }

        public bool ActualizarCompra(Compra obj)
        {
            throw new NotImplementedException();
        }

        public bool AgregarCompra(Compra obj)
        {
            throw new NotImplementedException();
        }

        public Compra BuscarPorIdCompra(int id)
        {
            throw new NotImplementedException();
        }

        public bool EliminarCompra(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Compra> EncontrarTodasCompras()
        {
            throw new NotImplementedException();
        }
    }
}
