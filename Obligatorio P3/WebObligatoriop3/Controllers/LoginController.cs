using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasosUsos;
using Dominio.EntidadesNegocio;
using WebObligatoriop3.Models;



namespace WebObligatoriop3.Controllers
{
    public class LoginController : Controller
    {
        public IManejadorUsuario User { get; set; }

        public LoginController(IManejadorUsuario usuario) 
        {
            User = usuario;
        }

        public IActionResult Login()
        {
            ViewModelLogin log = new ViewModelLogin();
            return View();
        }
        [HttpPost]
        public IActionResult Login(ViewModelLogin log)
        {
            Usuario user = User.IniciarSesion(log.Email,log.Contraseña);

            if (user != null)
            {
                HttpContext.Session.SetInt32("User_Id", user.Id);
                HttpContext.Session.SetString("UsuarioAutorizado","Si");
                Planta.CambiarParametrosValidacion(log.ValiPlantaDesde,log.ValiPlantaHasta);
                TipoDePlanta.CambiarParametrosValidacion(log.ValiTipoDePlantaDesde, log.ValiTipoDePlantaHasta);
                Importacion.ImpuestoImportacion = log.CargarImpuestoImportacion;
                Importacion.TasaArancelaria = log.CargarTasaArancelaria;
                Plaza.TasaIva = log.CargarTasaIva;                
            }
            else
            {
                ViewBag.Error = "Las credenciales son incorrectas o se encuentran vacias.";
                ViewBag.Error1 = "¡Ingrese los datos correctamente y vuelva a intentarlo!";
            }

            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                return RedirectToAction("Bienvenida", "Usuario");
            }
            else
            {
                return View();
            }
        }
        public IActionResult LogOut()
        {    
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }


    }
}
