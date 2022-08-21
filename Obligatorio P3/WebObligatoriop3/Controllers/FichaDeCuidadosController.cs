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
    public class FichaDeCuidadosController : Controller
    {
        public IManejadorPlantas mP { get; set; }

        public FichaDeCuidadosController(IManejadorPlantas manejadorP) 
        {
            mP = manejadorP;
        }
        // GET: FichaDeCuidadosController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: FichaDeCuidadosController/Details/5
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                FichaDeCuidado unaF = mP.BuscarFichaDeCuidado(id);
                return View(unaF);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        // GET: FichaDeCuidadosController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                ViewModelFichaDeCuidado vmFC= new ViewModelFichaDeCuidado();
                vmFC.TipoDeIluminaciones = mP.EncontrarTodosLosTiposDeIluminacion();
                return View(vmFC);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: FichaDeCuidadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelFichaDeCuidado vmFC)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {
                    bool agregado = false;

                    vmFC.TipoDeIluminaciones = mP.EncontrarTodosLosTiposDeIluminacion();
                    if (vmFC.UnaF.Cantidad > 0 && vmFC.UnaF.Temperatura > 0 && vmFC.UnaF.UnidadDeTiempo != null) 
                    {
                        agregado = mP.IngresarFichaDeCuidado(vmFC.UnaF, vmFC.IdTipoDeIluminacion);
                    }                   

                    if (agregado == true)
                    {
                        return RedirectToAction("Create", "Planta");                  
                    }

                    if (agregado == false)
                    {
                        ViewBag.ResultadoError = "No se ah podido ingresar correctamente el Tipo de Planta";
                    }

                    return View(vmFC);
                }
                catch
                {
                    vmFC.TipoDeIluminaciones = mP.EncontrarTodosLosTiposDeIluminacion();
                    ViewBag.ResultadoError = "No se ah podido ingresar correctamente el Tipo de Planta";
                    return View(vmFC);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
