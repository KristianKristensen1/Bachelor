﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models.ResearcherModel.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;
using StudyManagementSystem.Models;

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
            IRegisterHandler registerHandler = new RegisterHandler();
            Researcher currentr = new Researcher();
            currentr.Email = researcherRegisterModel.Email;
            currentr.Password = researcherRegisterModel.Password;
            currentr.Name = researcherRegisterModel.Firstname + researcherRegisterModel.Lastname; //TODO - First name og Last name i DB
            bool success = registerHandler.RegisterResearcherDB(currentr); 

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
            IRegisterHandler registerHandler = new RegisterHandler();
            Participant currentp = new Participant();
            currentp.Email = participantRegisterModel.Email;
            currentp.Password = participantRegisterModel.Password;
            currentp.Age = participantRegisterModel.Age;
            currentp.English = participantRegisterModel.Language; //Lidt misvisende navn i databasen, men intet kritisk (lyder bare som om der er flere valgmuligheder)
            if (participantRegisterModel.GenderType == Gender.Male)
                currentp.Gender = true;
            else
            {
                currentp.Gender = false;
            }
            
            
            bool success = registerHandler.RegisterParticipantDB(currentp); //Samme som ved Researcher

            if (!success)
             {
                 this.ModelState.AddModelError("Email", "Email already exists");
                 return View("Participant");

            }
            return RedirectToAction("Participant", "Homepage", currentp);
           
        }

   

       
    }
}