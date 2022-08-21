using System;
using System.Collections.Generic;
using System.Text;


namespace Dominio.EntidadesNegocio
{
    public class Plaza : Compra
    {
        public static int TasaIva { get; set; }
        public int CostoFlete { get; set; }


        public override decimal CalcularPrecioFinal(int precio)
        {
            int precioFinal= (TasaIva / 100 + 1) * precio + CostoFlete;
            return precioFinal;
        }      
     
    }
}
