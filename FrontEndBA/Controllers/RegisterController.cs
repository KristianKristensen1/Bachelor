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

      

        public ActionResult RegisterPageParticipant()
        {
            return View();
        }


        public ActionResult RegisterPageResearcher()
        {
         
            return View();
        }



        // POST: Register/Create
        

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearcher([Bind("Email,Password")] ResearcherRegisterViewModel researcherRegisterModel)
        {
            try
            {
                

                    IRegisterHandler registerHandler = new RegisterHandler();
                    Researcher currentr = new Researcher();
                    currentr.Email = researcherRegisterModel.Email;
                    currentr.Password = researcherRegisterModel.Password;
                    currentr.Name = researcherRegisterModel.Firstname + researcherRegisterModel.Lastname;
                    registerHandler.RegisterResearcherDB(currentr);


                return View("../HomePage/index", currentr);
            }
            catch
            {
                //Skal sende en fejlmeddelse med her.
                return View("./RegisterPageResearcher");
            }
        }

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParticipant([Bind("Email,Password")] ParticipantRegisterViewModel participantRegisterModel)
        {
            try
            {


                IRegisterHandler registerHandler = new RegisterHandler();
                Participant currentp = new Participant();
                currentp.Email = participantRegisterModel.Email;
                currentp.Password = participantRegisterModel.Password;
                registerHandler.RegisterParticipantDB(currentp);


                return View("../HomePage/index", currentp);
            }
            catch
            {
                //Skal sende en fejlmeddelse med her.
                return View("./RegisterPageParticipant");
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