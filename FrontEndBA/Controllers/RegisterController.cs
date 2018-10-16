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

     
        public ActionResult Participant()
        {
            
            return View("Participant");
        }


        public ActionResult Researcher()
        {
         
            return View();
        }

        

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearcher([Bind("Email,Password")] ResearcherRegisterViewModel researcherRegisterModel)
        {
            string Error;

            IRegisterHandler registerHandler = new RegisterHandler();
            Researcher currentr = new Researcher();
            currentr.Email = researcherRegisterModel.Email;
            currentr.Password = researcherRegisterModel.Password;
            currentr.Name = researcherRegisterModel.Firstname + researcherRegisterModel.Lastname;
            bool success = registerHandler.RegisterResearcherDB(currentr, out Error);

            if (!success)
            {
                this.ModelState.AddModelError("Email", "Email already exists");
                return View("Researcher");

            }
            return RedirectToAction("Researcher", "Homepage", currentr);
         
            
        }

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateParticipant")]
        public ActionResult CreateParticipant([Bind("Email,Password,GenderType,Language,Age")] ParticipantRegisterViewModel participantRegisterModel)
        {
            string Error;

            IRegisterHandler registerHandler = new RegisterHandler();
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
            
            
            bool success = registerHandler.RegisterParticipantDB(currentp, out Error);

            if (!success)
             {
                 this.ModelState.AddModelError("Email", "Email already exists");
                 return View("Participant");

            }
            return RedirectToAction("Participant", "Homepage", currentp);
           
        }

   

       
    }
}