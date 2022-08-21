using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebObligatoriop3.Models
{
    public class ViewModelLogin
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int ValiPlantaDesde { get; set; }
        public int ValiPlantaHasta { get; set; }
        public int ValiTipoDePlantaDesde { get; set; }
        public int ValiTipoDePlantaHasta { get; set; }
        public int CargarTasaArancelaria { get; set; }
        public int CargarTasaIva { get; set; }
        public int CargarImpuestoImportacion { get; set; }


    }
}
