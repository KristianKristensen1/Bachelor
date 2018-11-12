using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;
using FrontEndBA.Models.SharedModels;
using FrontEndBA.Utility.EmailHelper;
using Microsoft.AspNetCore.Mvc;
using StudyManagementSystem.DAOImplementations;
using StudyManagementSystem.DAOInterfaces;

namespace FrontEndBA.Controllers
{
    public class RetrieveAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Participant()
        {
            return View();
        }
        public IActionResult Researcher()
        {
            return View();
        }
        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RetrieveParticipant(RetrieveModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bachelordbContext db = new bachelordbContext();
                IRetrieveAccountHandler retrieveAccountHandler = new RetrieveAccountHandler(db);

                var status = retrieveAccountHandler.VerifyParticipantDB(loginModel.Email);
               
                if (status.LoginStatus.IsSuccess)
                {
                    EmailHelper emailh = new EmailHelper();
                    emailh.RetrieveAccount(status.LoginStatus.participant.Email,status.LoginStatus.participant.Password);
                    return RedirectToAction("Participant", "Welcome");
                }
                else
                {
                    var err = status.LoginStatus.ErrorMessage;
                    this.ModelState.AddModelError("Email", err.ToString());
                    
                }
            }

            return View("Participant");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RetrieveResearcher(RetrieveModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bachelordbContext db = new bachelordbContext();
                IRetrieveAccountHandler retrieveAccountHandler = new RetrieveAccountHandler(db);

                var status = retrieveAccountHandler.VerifyResearcherDB(loginModel.Email);

                if (status.LoginStatus.IsSuccess)
                {
                    EmailHelper emailh = new EmailHelper();
                    emailh.RetrieveAccount(status.LoginStatus.researcher.Email, status.LoginStatus.researcher.Password);
                    return RedirectToAction("Researcher", "Welcome");
                }
                else
                {
                    var err = status.LoginStatus.ErrorMessage;
                    this.ModelState.AddModelError("Email", err.ToString());

                }
            }

            return View("Researcher");

        }
        
  

        public IActionResult BackParticipant()
        {
            return RedirectToAction("Participant", "Welcome");
        }

        public IActionResult BackResearcher()
        {
            return RedirectToAction("Researcher", "Welcome");
        }
    }
}