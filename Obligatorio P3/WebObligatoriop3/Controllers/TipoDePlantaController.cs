using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasosUsos;
using Dominio.EntidadesNegocio;

namespace WebObligatoriop3.Controllers
{
    public class TipoDePlantaController : Controller
    {
        public IManejadorPlantas mTPlanta { get; set; }
        public TipoDePlantaController(IManejadorPlantas tPlanta) 
        {
            mTPlanta = tPlanta;
        }

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

        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                TipoDePlanta tDP = mTPlanta.BuscarPorIdTipoDePlanta(id);
                return View(tDP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {    
                return View(new TipoDePlanta());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoDePlanta nuevoTP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {
                    bool agregado = false;

                    if (nuevoTP.Nombre != null && nuevoTP.Descripcion != null)
                    {
                        nuevoTP.Nombre= nuevoTP.Nombre.Trim();
                        nuevoTP.Descripcion= nuevoTP.Descripcion.Trim();
                        agregado=mTPlanta.AgregarTipoDePlanta(nuevoTP);
                    }

                    if (agregado == true)
                    {
                        TempData["MensajeOk"] = "Se ah creado correctamente el Tipo de planta " + nuevoTP.Nombre;
                        return RedirectToAction("ListaTiposDePlanta");
                    }
                    
                    TempData["MensajeError"] = "No se ah podido ingresar correctamente el Tipo de Planta";                                          
                    return View(nuevoTP);
                }
                catch
                {
                    TempData["MensajeError"] = "No se ah podido ingresar correctamente el Tipo de Planta";
                    return View(nuevoTP);
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
                TipoDePlanta tipoDePlanta = mTPlanta.BuscarPorIdTipoDePlanta(id);
                return View(tipoDePlanta);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoDePlanta nuevoTP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {
                    bool tipoDePlanta = false;

                    if (nuevoTP.Nombre != null && nuevoTP.Descripcion != null)
                    {
                        nuevoTP.Nombre = nuevoTP.Nombre.Trim();
                        nuevoTP.Descripcion = nuevoTP.Descripcion.Trim();
                        tipoDePlanta = mTPlanta.ActualizarTipoDePlanta(nuevoTP);
                    }      
              
                    if (tipoDePlanta == true)
                    {
                        TempData["MensajeOk"]= "Se ah actualizado correctamente el Tipo de Planta";
                        return RedirectToAction("ListaTiposDePlanta");
                    }

                    TempData["MensajeError"] = "No se ah podido actualizar correctamente el Tipo de Planta";
                    return View(nuevoTP);
                }
                catch
                {            
                    return View(nuevoTP);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Delete(int id) //Ver como ocultar datos cuando se elimine el tipo de planta
        {

            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                TipoDePlanta tDP = mTPlanta.BuscarPorIdTipoDePlanta(id);
                return View(tDP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: TipoDePlantaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TipoDePlanta tipoDP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {    
                    bool eliminar = mTPlanta.EliminarTipoDePlanta(tipoDP.Id);
                    TipoDePlanta tDP = mTPlanta.BuscarPorIdTipoDePlanta(id);

                    if (eliminar == true)
                    {
                        TempData["MensajeOk"] = "El Tipo de Planta fue eliminado exitosamente";
                        return RedirectToAction("ListaTiposDePlanta");
                    }
                    else
                    {
                        TempData["MensajeError"] = "No se ah podido eliminar el Tipo de Planta";
                    }
                
                    return View(tipoDP);
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
        public ActionResult ListaTiposDePlanta()
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                IEnumerable<TipoDePlanta> tipoDePlantas = mTPlanta.EncontrarTodosLosTipoDePlanta();
                return View(tipoDePlantas);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult EditDesc(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                TipoDePlanta tipoDP = mTPlanta.BuscarPorIdTipoDePlanta(id);
                return View(tipoDP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDesc(int id, TipoDePlanta nuevoTP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {                  
                    bool tipoDePlanta = false;

                    if (nuevoTP.Descripcion != null)
                    {
                        nuevoTP.Descripcion = nuevoTP.Descripcion.Trim();
                        tipoDePlanta = mTPlanta.ActualizarTipoDePlanta(nuevoTP);
                    }
                   
                    if (tipoDePlanta == true)
                    {
                        TempData["MensajeOk"] ="Se ah actualizado correctamente el Tipo de Planta";
                        return RedirectToAction("ListaTiposDePlanta");
                    }

                    TempData["MensajeError"] = "No se ah podido actualizar correctamente el Tipo de Planta";
                    return View(nuevoTP);
                }
                catch
                {              
                    return View(nuevoTP);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult BuscarPorNombre()
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
        public IActionResult BuscarPorNombre(string nombre)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                ViewBag.tDP = mTPlanta.BuscarPorNombreTipoDePlanta(nombre);
                if (ViewBag.tDP == null)
                {
                    TempData["MensajeError"]= "No se ah podido encontrar ningun Tipo de Planta con ese nombre, verifique el nombre y vuelva a intentarlo";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
