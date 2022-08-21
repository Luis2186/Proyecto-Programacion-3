using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.EntidadesNegocio;
using Microsoft.AspNetCore.Http;

namespace WebObligatoriop3.Models
{
    public class ViewModelPlanta
    {
        public Planta UnaP { get; set; }
        public FichaDeCuidado UnaF { get; set; }
        public IEnumerable<TipoDePlanta> TipoDePlantas { get; set; }
        public IEnumerable<TipoDeIluminacion> TipoDeIluminaciones { get; set; }
        public IEnumerable<Planta> Plantas { get; set; }
        public List<string> NomVulgares { get; set; }
        public int IdTipoDeIluminacion { get; set; }
        public int IdTipoDePlantas { get; set; }
        public int IdTipoDeBusqueda { get; set; }
        public string IdAmbiente { get; set; } 
        public string NombreV { get; set; }     
        public IFormFile Imagen { get; set; }
    }
}
