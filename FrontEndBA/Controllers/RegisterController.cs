using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models.ResearcherModel.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndBA.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Participant()
        {
            return View("Participant");
        }

        public ActionResult Researcher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult CreateResearcher(ResearcherRegisterViewModel researcherRegisterModel)
        {
            IRegisterHandler registerHandler = new RegisterHandler(new bachelordbContext());
            Researcher currentr = new Researcher();
            currentr.Email = researcherRegisterModel.Email;
            currentr.Password = researcherRegisterModel.Password;
            currentr.FirstName = researcherRegisterModel.Firstname;
            currentr.LastName = researcherRegisterModel.Lastname;
            bool success = registerHandler.RegisterResearcherDB(currentr); 

            if (!success)
            {
                //User not saved in db
                this.ModelState.AddModelError("Email", "Email already exists");
                return View("Researcher");
            }
            return RedirectToAction("LoginResearcher", "Welcome", currentr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Route("/CreateParticipant")]
        public ActionResult CreateParticipant(ParticipantRegisterViewModel participantRegisterModel)
        {
            IRegisterHandler registerHandler = new RegisterHandler(new bachelordbContext());
            Participant currentp = new Participant();
            currentp.Email = participantRegisterModel.Email;
            currentp.Password = participantRegisterModel.Password;
            currentp.Age = participantRegisterModel.Age;
            currentp.English = participantRegisterModel.Language;
            if (participantRegisterModel.GenderType == Gender.Male)
                currentp.Gender = true;
            else
            {
                currentp.Gender = false;
            }
            
            
            bool success = registerHandler.RegisterParticipantDB(currentp);

            if (!success)
             {
                //User not saved in db
                 this.ModelState.AddModelError("Email", "Email already exists");
                 return View("Participant");

            }
            return RedirectToAction("LoginParticipant", "Welcome", currentp);
        }

    }
}