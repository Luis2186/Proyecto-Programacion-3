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
    public class UsuarioController : Controller
    {
        public IManejadorUsuario User { get; set; }

        public UsuarioController(IManejadorUsuario usuario)
        {
            User = usuario;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            Usuario uBuscado = User.BuscarUsuarioPorId(id);
            return View(uBuscado);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        //    if(HttpContext.Session.GetInt32("User_Id") == )
        public IActionResult Bienvenida()
        {
            if(HttpContext.Session.GetString("UsuarioAutorizado") == "Si")
            {
                int idUser = HttpContext.Session.GetInt32("User_Id").Value;
                Usuario uBuscado = User.BuscarUsuarioPorId(idUser);
                return View(uBuscado);           
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


    }
}
