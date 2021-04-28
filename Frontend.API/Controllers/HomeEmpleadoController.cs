using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.API.Controllers
{
    [Authorize]
    public class HomeEmpleadoController : Controller
    {


        // GET: HomeEmpleadoController

        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeEmpleadoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeEmpleadoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeEmpleadoController/Create
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

        // GET: HomeEmpleadoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeEmpleadoController/Edit/5
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

        // GET: HomeEmpleadoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeEmpleadoController/Delete/5
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


    }
}
