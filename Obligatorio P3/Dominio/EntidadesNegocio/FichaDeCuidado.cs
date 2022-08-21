using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorio;

namespace Dominio.EntidadesNegocio
{
    public class FichaDeCuidado : IValidacion
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public string UnidadDeTiempo { get; set; }
        public decimal Temperatura { get; set; }
        public TipoDeIluminacion TipoDeIluminacion { get; set; }

        public FichaDeCuidado() { }

        public bool SoyValido()
        {
            bool soyValido = false;
            if (Cantidad > 0 && UnidadDeTiempo.Trim() != "")
            {
                soyValido = true;
            }
            return soyValido;
        }

        public override string ToString()
        {
            string datos = "";
            datos = "Su Frecuencia de Riego es de: " + Cantidad + " " + UnidadDeTiempo + "\n";            
            datos += "Se debe mantener a: " + Temperatura + "°C de Temp" + "\n";
            return datos;
        }
    }
}
