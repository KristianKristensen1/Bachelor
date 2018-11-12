﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.EmailModels;
using Microsoft.AspNetCore.Mvc;
using FrontEndBA.Utility;
using FrontEndBA.Utility.EmailHelper;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndBA.Controllers.Studies
{
    public class SendToParticipantController : Controller
    {
        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Index(int studyID)
        {
            ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();
           SendingModel sendToParticipantModel = new SendingModel();
            sendToParticipantModel.Study = viewStudyModelHelper.createViewStudyModel(studyID);

            EmailHelper emailHelper = new EmailHelper();

            emailHelper.PrefillTextArea(sendToParticipantModel);

            return View(sendToParticipantModel);
        }
        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Create(SendingModel sModel,int studyID)
        {
            ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();
            sModel.Study = viewStudyModelHelper.createViewStudyModel(studyID);
            if (ModelState.IsValid)
            {
                try
                {
                    //Gets the id from JWT. The id is used to retrieve user from database. 
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    int id_researcher = Convert.ToInt32(claims.ElementAt(3).Value);


                    //
                    //ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();
                    //sModel.Study = viewStudyModelHelper.createViewStudyModel(studyID);

                    ManageStudyHandler msh  = new ManageStudyHandler(new bachelordbContext());
                   List<Participant> participants= msh.getParticipantsListDB(sModel.Study.study.IdStudy);

                    // Convert to create the right format
                    EmailHelper emailHelper = new EmailHelper();
                    
                    emailHelper.SendMessages(sModel,participants);

                    return RedirectToAction("Researcher", "Homepage");

                }
                catch (Exception e)
                {
                   
             
                    return View("Index");
                }
            }
            return View("index", sModel);
        }
        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Back(int studyID)
        {

            return RedirectToAction("ViewStudy", "ViewStudy", new { studyID = studyID });
        }
    }
}