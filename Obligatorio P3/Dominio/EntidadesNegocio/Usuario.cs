using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorio;
using System.ComponentModel.DataAnnotations;


namespace Dominio.EntidadesNegocio
{
    public class Usuario :IValidacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        public bool SoyValido()
        {
            bool soyValido = false;

            if (ValidarContraseña(Contraseña, 6, 1, 1, 1) && ValidarEmail() && Nombre.Trim() != "" && Apellido.Trim() !="") 
            {
                soyValido = true;
            }
            return soyValido;
        }

        private bool ValidarContraseña(string contraseña, int minCaracteresAceptados, int minMayusuclaAceptadas, int minMinusculaAceptadas, int minNumerosAceptados)
        {
            int mayusculas = 0;
            int minusculas = 0;
            int numerocontraseña = 0;
            bool isOk = false;

            for (int e = 0; e < contraseña.Length; e++)
            {
                int codigoASCII = (int)contraseña[e];

                if (codigoASCII >= 65 && codigoASCII <= 90)
                {
                    mayusculas++;
                }
                else if (codigoASCII >= 97 && codigoASCII <= 122)
                {
                    minusculas++;
                }
                else if (codigoASCII >= 48 && codigoASCII <= 57)
                {
                    numerocontraseña++;
                }
            }

            if (contraseña.Length >= minCaracteresAceptados && mayusculas >= minMayusuclaAceptadas && minusculas >= minMinusculaAceptadas && numerocontraseña >= minNumerosAceptados)
            {
                isOk = true;
            }

            return isOk;
        }
        private bool ValidarEmail() 
        {
            bool esValido = false;
            if (new EmailAddressAttribute().IsValid(Email)) 
            {
                esValido=true;
            }
            
            return esValido;
        }
    }
}
