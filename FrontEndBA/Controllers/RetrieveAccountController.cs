﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;
using FrontEndBA.Models.SharedModels;
using FrontEndBA.Utility.EmailHelper;
using FrontEndBA.Utility.EmailHelpers;
using Microsoft.AspNetCore.Mvc;
using StudyManagementSystem.DAOImplementations;
using StudyManagementSystem.DAOInterfaces;

namespace FrontEndBA.Controllers
{
    public class RetrieveAccountController : Controller
    {

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
        public async Task<IActionResult> RetrieveParticipant(RetrieveModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bachelordbContext db = new bachelordbContext();
                IRetrieveAccountHandler retrieveAccountHandler = new RetrieveAccountHandler(db);

                var status = retrieveAccountHandler.VerifyParticipantDB(loginModel.Email);
               
                if (status.success)
                {
                    //ResetPassword. 
                    IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
                    var oldPassword = status.participant.Password;
                    status.participant.Password = SecureString.RandomString(6);

                    var changePasswordStatus = mph.ChangePasswordParticipantDB(status.participant, oldPassword);


                    //Sending the mail
                    EmailHelper emailh = new EmailHelper();

                    await emailh.RetrieveAccount(status.participant.Email,status.participant.Password);
                    return RedirectToAction("Participant", "Welcome");
                }
                else
                {
                    var err = status.errormessage;
                    this.ModelState.AddModelError("Email", err.ToString());
                }
            }
            return View("Participant");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RetrieveResearcher(RetrieveModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IRetrieveAccountHandler retrieveAccountHandler = new RetrieveAccountHandler(new bachelordbContext());
                DbStatus status = retrieveAccountHandler.VerifyResearcherDB(loginModel.Email);

                if (status.success)
                {
                    //ResetPassword. 
                    IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
                    var oldPassword = status.researcher.Password;
                    status.researcher.Password = SecureString.RandomString(6);

                    var changePasswordStatus = mph.ChangePasswordResearcherDB(status.researcher, oldPassword);

                    //Sending the email
                    EmailHelper emailh = new EmailHelper();
                    await emailh.RetrieveAccount(status.researcher.Email, status.researcher.Password);
                    return RedirectToAction("Researcher", "Welcome");
                }
                else
                {
                    var err = status.errormessage;
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