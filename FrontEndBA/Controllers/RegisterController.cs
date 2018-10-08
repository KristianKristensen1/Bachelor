using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models.ResearcherModel.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;

namespace FrontEndBA.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        // GET: Register/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult RegisterPageParticipant()
        {
            return View();
        }


        public ActionResult RegisterPageResearcher()
        {
         
            return View();
        }



        // POST: Register/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind("Email,Password")] ResearcherRegisterViewModel researcherRegisterModel)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        IRegisterHandler registerHandler = new RegisterHandler();
        //        Participant currentp = new Participant();
        //        currentp.Email = researcherRegisterModel.Email;
        //        currentp.Password = researcherRegisterModel.Password;
        //        registerHandler.RegisterParticipantDB(currentp);
        //    }
        //    return View();
        //}
        // not done
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind("Email,Password")] ParticipantRegisterViewModel participantRegisterModel)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        IRegisterHandler registerHandler = new RegisterHandler();
        //        Participant currentp = new Participant();
        //        currentp.Email = participantRegisterModel.Email;
        //        currentp.Password = researcherRegisterModel.Password;
        //        registerHandler.RegisterParticipantDB(currentp);
        //    }
        //    return View();
        //}

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearcher([Bind("Email,Password")] ResearcherRegisterViewModel researcherRegisterModel)
        {
            try
            {
                    IRegisterHandler registerHandler = new RegisterHandler();
                    Participant currentp = new Participant();
                    currentp.Email = researcherRegisterModel.Email;
                    currentp.Password = researcherRegisterModel.Password;
                    registerHandler.RegisterParticipantDB(currentp);
                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Register/Edit/5
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

        // GET: Register/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Register/Delete/5
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