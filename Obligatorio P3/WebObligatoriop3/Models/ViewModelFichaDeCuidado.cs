using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.EntidadesNegocio;
using Microsoft.AspNetCore.Http;

namespace WebObligatoriop3.Models
{
    public class ViewModelFichaDeCuidado
    {
        public IEnumerable<TipoDeIluminacion> TipoDeIluminaciones { get; set; }
        public FichaDeCuidado UnaF { get; set; }
        public int IdTipoDeIluminacion { get; set; }

    }
}
