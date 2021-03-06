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

            IManageParticipantHandler mph = new ManageParticipantHandler(new bachelordbContext());
            List<Participant> participants = mph.GetAllEligibalParticipants(sendToParticipantModel.Study.inclusioncriteria,studyID);
            sendToParticipantModel.ParticipantCount = participants.Count;
            
            return View(sendToParticipantModel);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Create(SendingModel sModel, int studyID)
        {
            ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();
            sModel.Study = viewStudyModelHelper.createViewStudyModel(studyID);
            if (ModelState.IsValid)
            {
                try
                {


                    // Convert to create the right format
                    EmailHelper emailHelper = new EmailHelper();
                    emailHelper.SendMessages(sModel, studyID);

                    return RedirectToAction("Researcher", "Homepage");
                }
                catch (Exception e)
                {
                    //Should store error in a internal log. 
                    return View("Index");
                }
            }
            IManageParticipantHandler mph1 = new ManageParticipantHandler(new bachelordbContext());
            List<Participant> participants1 = mph1.GetAllEligibalParticipants(sModel.Study.inclusioncriteria,studyID);
            sModel.ParticipantCount = participants1.Count;
            return View("index", sModel);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Back(int studyID)
        {
            return RedirectToAction("ViewStudy", "ViewStudy", new { studyID = studyID });
        }
    }
}