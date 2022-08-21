using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasosUsos;
using Dominio.EntidadesNegocio;
using WebObligatoriop3.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebObligatoriop3.Controllers
{

    public class PlantaController : Controller
    {
        public IManejadorPlantas mP { get; set; }
        public IWebHostEnvironment WebHostEnv { get; set; }
        public PlantaController(IManejadorPlantas manejadorP, IWebHostEnvironment whenv) 
        {       
            mP = manejadorP;
            WebHostEnv = whenv;
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
                Planta unaP = mP.BuscarPlantaPorId(id);
                return View(unaP);
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
                ViewModelPlanta vmP = new ViewModelPlanta();                
                vmP.TipoDeIluminaciones = mP.EncontrarTodosLosTiposDeIluminacion();
                vmP.TipoDePlantas = mP.EncontrarTodosLosTipoDePlanta();
                return View(vmP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelPlanta vmP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {
                    string nomArchivo = "";
                    bool agregada = false;

                    vmP.UnaP.NombresVulgares = vmP.NomVulgares;
                    vmP.TipoDeIluminaciones = mP.EncontrarTodosLosTiposDeIluminacion();
                    vmP.TipoDePlantas = mP.EncontrarTodosLosTipoDePlanta();
                    
                    if (vmP.Imagen != null) 
                    {                    
                    nomArchivo = vmP.Imagen.FileName;
                    string extension = vmP.UnaP.TomarExtension(vmP.Imagen.FileName);
                    nomArchivo = vmP.UnaP.FormatoFoto() + extension;
                    vmP.UnaP.Foto = nomArchivo;
                    }
      
                    if(vmP.UnaP.NombreCientifico!= null && vmP.UnaP.NombresVulgares.Any() && vmP.UnaP.AlturaMaxima >= 1 
                        && vmP.UnaP.Foto != null && vmP.UnaP.Precio>0 && vmP.UnaP.AlturaMaxima >0 && vmP.UnaF.Cantidad>0 &&
                        vmP.UnaF.UnidadDeTiempo !=null )
                    {
                        vmP.UnaP.NombreCientifico = vmP.UnaP.NombreCientifico.Trim();
                        agregada = mP.AgregarPlanta(vmP.UnaP,vmP.UnaF,vmP.IdTipoDePlantas, vmP.IdTipoDeIluminacion);
                    }

                    if (agregada)
                    {
                        string rutaRaiz = WebHostEnv.WebRootPath;
                        string rutaImagenes = Path.Combine(rutaRaiz,"Imagenes");
                        string rutaArchivo = Path.Combine(rutaImagenes,nomArchivo);
                        FileStream stream = new FileStream(rutaArchivo, FileMode.Create);
                        vmP.Imagen.CopyTo(stream);
                        TempData["MensajeOk"] = "Se ah creado correctamente la planta " + vmP.UnaP.NombreCientifico;
                        return RedirectToAction("ListaDePlantas");
                    }
                    else
                    {
                        TempData["MensajeError"] =  "No se pudo realizar el alta de la planta correctamente, verifique los datos de ingreso y vuelva a intentarlo." +
                                                    "Tenga en cuenta que: " +
                                                    "Su nombre Cientifico debera ser unico y solo contendra caracteres alfabeticos. " +
                                                    "El campo Nombre Vulgar debe contener al menos 1 nombre ingresado. " +
                                                    "La descripcion debe tener un minimo de 10 caracteres y un maximo de 500. " +
                                                    "La imagen solo tolera formatos jpg o png. " +
                                                    "Ninguno de los campos pueden encontrarse vacios. ";
                    
                        vmP.TipoDeIluminaciones = mP.EncontrarTodosLosTiposDeIluminacion();
                        vmP.TipoDePlantas = mP.EncontrarTodosLosTipoDePlanta();
                        return View(vmP);
                    }                        
                }
                catch 
                {
                    TempData["MensajeError"] = "No se pudo realizar el alta de la planta correctamente, verifique los datos de ingreso y vuelva a intentarlo." +
                                                 "Tenga en cuenta que: " +
                                                 "Su nombre Cientifico debera ser unico y solo contendra caracteres alfabeticos. " +
                                                 "El campo Nombre Vulgar debe contener al menos 1 nombre ingresado. " +
                                                 "La descripcion debe tener un minimo de 10 caracteres y un maximo de 500. " +
                                                 "La imagen solo tolera formatos jpg o png. " +
                                                 "Ninguno de los campos pueden encontrarse vacios. ";
                    return View(vmP);
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
        public ActionResult ListaDePlantas()
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                IEnumerable<Planta> plantas = mP.MostrarTodasLasPlantas();
                return View(plantas);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult PlantaPorTextoEnNombre(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                ViewModelPlanta vmP = new ViewModelPlanta();
            return View(vmP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlantaPorTextoEnNombre(string nombreBuscado,ViewModelPlanta vmP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                { 

                vmP.Plantas = mP.BuscarPlantasPorTextoEnNombre(nombreBuscado);

                if (!vmP.Plantas.Any())
                {
                    TempData["MensajeError"] = "No se ah podido encontrar ninguna Planta con ese nombre, verifique el nombre y vuelva a intentarlo";
                }
                    return View(vmP);
                }
                catch
                {
                    return View(vmP);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult PlantasPorTipo(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                ViewModelPlanta vmP = new ViewModelPlanta();
                vmP.TipoDePlantas = mP.EncontrarTodosLosTipoDePlanta();
                return View(vmP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlantasPorTipo(ViewModelPlanta vmP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                 try
                {
                    vmP.TipoDePlantas = mP.EncontrarTodosLosTipoDePlanta();
                    vmP.Plantas = mP.BuscarPlantasPorTipo(vmP.IdTipoDePlantas);
                    if (!vmP.Plantas.Any())
                    {
                        TempData["MensajeError"] = "El Tipo que selecciono no se encuentra en ninguna Planta, seleccione otro Tipo y vuelva a intentarlo";
                    }
                    return View(vmP);
                }
                catch
                {
                    return View(vmP);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult PlantasPorAmbiente() //http://localhost:35116/Planta/PlantasPorAmbiente/?nombre=Interior
        {
            ViewModelPlanta vmP = new ViewModelPlanta();
           
            return View(vmP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlantasPorAmbiente(ViewModelPlanta vmP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {               
                vmP.TipoDePlantas = mP.EncontrarTodosLosTipoDePlanta();
                vmP.Plantas = mP.BuscarPlantasPorAmbiente(vmP.IdAmbiente);
                    
                if (!vmP.Plantas.Any())
                {
                    TempData["MensajeError"] = "No se ah podido encontrar ninguna Planta en el ambiente seleccionado, seleccione otro ambiente y vuelva a intentarlo";
                }
                return View(vmP);

                }
                catch
                {
                    return View(vmP);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult PlantasPorMayorAltura(int id) //Usar id como nombre de parametro para no tener que usar Qstring
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                ViewModelPlanta vmP = new ViewModelPlanta();  
                return View(vmP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlantasPorMayorAltura(int altura,ViewModelPlanta vmP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {   
                vmP.Plantas = mP.BuscarPlantasPorAlturaMayorIgual(altura);

                if (!vmP.Plantas.Any())
                {
                    TempData["MensajeError"] = "No se ah podido encontrar ninguna Planta que sea mas alta, ingrese una altura mas baja y vuelva a intentarlo";
                }
                    return View(vmP);
                }
                catch
                {
                    return View(vmP);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult PlantasPorMenorAltura(int id)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                ViewModelPlanta vmP = new ViewModelPlanta();  
            return View(vmP);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlantasPorMenorAltura(int altura, ViewModelPlanta vmP)
        {
            if (HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                try
                {
                    vmP.Plantas = mP.BuscarPlantasMasBajas(altura);
                    if (!vmP.Plantas.Any())
                    {
                        TempData["MensajeError"] = "No se ah podido encontrar ninguna Planta mas baja, ingrese una altura mayor y vuelva a intentarlo";
                    }
                    return View(vmP);
                }
                catch
                {
                    return View(vmP);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult ControlarVistas(int id)
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

    }
}
