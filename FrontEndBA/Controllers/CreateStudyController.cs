using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class CreateStudyController : Controller
    {
        // GET: CreateStudy
        public ActionResult Index()
        {
            return View();
        }

        // GET: CreateStudy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: CreateStudy/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

       

        // POST: CreateStudy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,Isdraft,Tag,IdResearcher")] Study studymodel)
        {
            try
            {
                ManageStudyHandler manageStudyHandler = new ManageStudyHandler();
                manageStudyHandler.CreateStudyDB(studymodel);

                return View("../HomePage/index");
            }
            catch
            {
                return View("./Participant");
            }
        }

        // GET: CreateStudy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CreateStudy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CreateStudy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreateStudy/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}