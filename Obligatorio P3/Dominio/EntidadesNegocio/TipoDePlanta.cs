using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorio;

namespace Dominio.EntidadesNegocio
{
    public class TipoDePlanta : IValidacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public string Descripcion { get; set; }
        private static int ValidacionDesde { get; set; }
        private static int ValidacionHasta { get; set; }

        public TipoDePlanta() { }

        public bool SoyValido()
        {
            bool valido = false;   

            if (Nombre.Trim()!= "" && Descripcion.Length >= ValidacionDesde && Descripcion.Length <= ValidacionHasta && ValidarSoloLetrasConASCI(Nombre))
            {
                valido = true;
            }
            return valido;
        }
        private bool ValidarSoloLetrasConASCI(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                //A=65 Z=90 and a=97 z=122
                if ((int)texto[i] < 32 || (int)texto[i] > 32 && (int)texto[i] < 65 || ((int)texto[i] > 90
                    && (int)texto[i] < 97) || (int)texto[i] > 122)
                    return false;
            }
            return true;
        }
        public override string ToString()
        {
            return Nombre;
        }
        public static void CambiarParametrosValidacion(int desde, int hasta)
        {
            ValidacionDesde = desde;
            ValidacionHasta = hasta;
        }

    }
}
