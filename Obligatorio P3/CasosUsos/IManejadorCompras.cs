using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;
using Repositorios;

namespace CasosUsos
{
    interface IManejadorCompras
    {
        public bool ActualizarCompra(Compra obj);
        public bool AgregarCompra(Compra obj);
        public Compra BuscarPorIdCompra(int id);
        public bool EliminarCompra(int id);
        public IEnumerable<Compra> EncontrarTodasCompras();
    }
}
