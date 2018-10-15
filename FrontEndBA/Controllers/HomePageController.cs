using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class HomepageController : Controller
    {
        // GET: HomePage
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        //[Authorize(Policy = "RequiresVerified")]
        public ActionResult Researcher()
        {
            List<Study> fakelist = new List<Study>();
            Study fakestudy = new Study();
            fakestudy.Description = "Test";
            fakestudy.Tag = "Tag";
            fakestudy.Isdraft = true;
            Study fakestudy2 = new Study();
            fakestudy2.Description = "2Test";
            fakestudy2.Tag = "2Tag";
            fakestudy2.Isdraft = false;
            fakelist.Add(fakestudy2);
            fakelist.Add(fakestudy);
            return View(fakelist);
        }


        [Authorize]
        public ActionResult AddStudyView()
        {
            return RedirectToAction("Index", "CreateStudy");
        }

        // GET: HomePage/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        // GET: HomePage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomePage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Participant));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomePage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomePage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction(nameof(Participant));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomePage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomePage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Participant));
            }
            catch
            {
                return View();
            }
        }
    }
}