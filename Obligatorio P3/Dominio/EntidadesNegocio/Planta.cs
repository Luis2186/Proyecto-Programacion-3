using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorio;

namespace Dominio.EntidadesNegocio
{
    public class Planta : IValidacion
    {
        public int Id { get; set; }
        public TipoDePlanta Tipo { get; set; }
        public string NombreCientifico { get; set; }
        public List<string> NombresVulgares { get; set; }
        public string Descripcion { get; set; }
        public string Ambiente { get; set; }
        public decimal AlturaMaxima { get; set; }
        public string Foto { get; set; } 
        public FichaDeCuidado FichaDeCuidados { get; set; }
        public decimal Precio { get; set; }
        public string Codigo { get; set; }
        private static int ValidacionDesde { get; set; }
        private static int ValidacionHasta { get; set; }

        public Planta() { }

        public bool SoyValido()
        {
            bool valido = false;
            if (NombreCientifico.Trim() != "" && NombresVulgares.Count > 0 && Ambiente != "" && Foto != "" /*&& Foto !=null && Precio !=null && Codigo !=null && AlturaMaxima !=null*/ && ValidarSoloLetrasConASCI(NombreCientifico) 
                && Descripcion.Length >= ValidacionDesde && Descripcion.Length <= ValidacionHasta && ValidarAmbiente() 
                && ValidarExtensionFoto(Foto))
            {
                valido = true;
            }
            return valido;
        }

        private bool ValidarAmbiente()
        {
            bool valido = false;
            if (Ambiente.Trim() == "Interior" || Ambiente.Trim() == "Exterior" || Ambiente.Trim() == "Mixta")
            {
                valido = true;

            }
            return valido;
        }
        public string FormatoFoto()
        {
            Codigo = "001";
            if (NombreCientifico != "" && NombreCientifico!= null)
            {
                Foto = NombreCientifico.Replace(" ", "_").Trim() + "_" + Codigo;
            }
            else
            {
                Foto = "";
            }
            return Foto;
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
        public string TomarExtension(string archivo)
        {
            string extension = "";
            for (int i= archivo.Length - 4; i < archivo.Length; i++)
            {
                extension += archivo[i];
            }
            return extension;
        }
        private bool ValidarExtensionFoto(string archivo)
        {
            string extension = "";
            bool valido = false;
            for (int i = archivo.Length - 4; i < archivo.Length; i++)
            {
                extension += archivo[i];
            }
            if (extension == ".png" || extension == ".jpg")
            {
                valido = true;
            }
            return valido;
        }
        public static void CambiarParametrosValidacion(int desde, int hasta)
        {
            ValidacionDesde = desde;
            ValidacionHasta = hasta;
        }

        public void AgregarNombreVulgar(string nombre)
        {
            if (NombresVulgares == null)
            {
                NombresVulgares = new List<string>();
            }
            NombresVulgares.Add(nombre);            
        }



    }
}
