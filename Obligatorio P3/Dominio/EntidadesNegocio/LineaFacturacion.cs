using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorio;

namespace Dominio.EntidadesNegocio
{
    public class LineaFacturacion : IValidacion 
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public Planta Planta { get; set; }
        public decimal PrecioUnitario { get; set; }

        public LineaFacturacion() { }

        public bool SoyValido()
        {
            throw new NotImplementedException();
        }
    }
}
