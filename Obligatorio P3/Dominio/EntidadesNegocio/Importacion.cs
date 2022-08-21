using System;
using System.Collections.Generic;
using System.Text;


namespace Dominio.EntidadesNegocio
{
    public abstract class Importacion : Compra
    {
        public static int ImpuestoImportacion { get; set; }
        public string MedidasSanitarias { get; set; }
        public bool AmericaDelSur { get; set; }
        public static int TasaArancelaria { get; set; }

        public Importacion() { }

        public override decimal CalcularPrecioFinal(int precio)
        {
            int precioFinal = (ImpuestoImportacion / 100 + 1) * precio;
            int descuentoADS= (TasaArancelaria / 100) * precio;

            if (AmericaDelSur == true) { 
                precioFinal = precioFinal - descuentoADS;
            }

            return precioFinal;
        }

       
    }
}
