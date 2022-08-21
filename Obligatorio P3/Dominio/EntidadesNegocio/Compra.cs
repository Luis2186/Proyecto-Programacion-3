using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorio;

namespace Dominio.EntidadesNegocio
{
    public abstract class Compra : IValidacion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public List<LineaFacturacion> PlantasFact { get; set; }

        public Compra() { }

        public abstract decimal CalcularPrecioFinal(int precio);

        public bool SoyValido()
        {
            throw new NotImplementedException();
        }

    }
}
