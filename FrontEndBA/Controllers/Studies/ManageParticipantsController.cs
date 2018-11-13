using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models;
using FrontEndBA.Utility;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndBA.Controllers.Studies
{
    public class ManageParticipantsController : Controller
    {
        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult ManageParticipants(int studyID, string studyName)
        {
            ManageParticipantsHelper manageParticipantsHelper = new ManageParticipantsHelper();
            ManageParticipantModel manageParticipantModel = new ManageParticipantModel();
            manageParticipantModel = manageParticipantsHelper.CreateManageParticipantModel(studyID, studyName);

            return View(manageParticipantModel);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult AddParticipant(ManageParticipantModel mpModel)
        {
            IManageParticipantHandler mph = new ManageParticipantHandler(new bachelordbContext());
            if (ModelState.IsValid)
            {
                try
                {
                    DbStatus manageParticipantStatus = mph.AddParticipantToStudyDB(mpModel.participantID, mpModel.studyID);
                    if (manageParticipantStatus.success)
                    {
                        return RedirectToAction("ManageParticipants", "ManageParticipants", new { studyID = mpModel.studyID, studyName = mpModel.nameOfStudy});
                    }
                    else
                    {
                        ModelState.AddModelError("participantID", manageParticipantStatus.errormessage);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            mpModel.participants = mph.GetParticipantsInStudyDB(mpModel.studyID);   
            return View("ManageParticipants", mpModel);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult RemoveParticipant(ManageParticipantModel mpModel)
        {
            IManageParticipantHandler mph = new ManageParticipantHandler(new bachelordbContext());
            if (ModelState.IsValid)
            {
                try
                {
                    DbStatus manageParticipantStatus = mph.RemoveParticipantFromStudyDB(mpModel.participantID, mpModel.studyID);
                    if (manageParticipantStatus.success)
                    {
                        return RedirectToAction("ManageParticipants", "ManageParticipants", new { studyID = mpModel.studyID, studyName = mpModel.nameOfStudy });
                    }
                    else
                    {
                        ModelState.AddModelError("ParticipantID", manageParticipantStatus.errormessage);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            mpModel.participants = mph.GetParticipantsInStudyDB(mpModel.studyID);
            return View("ManageParticipants", mpModel);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult GetEmail(ManageParticipantModel mpModel)
        {
            IManageParticipantHandler mph = new ManageParticipantHandler(new bachelordbContext());
            try
            {
                DbStatus manageParticipantStatus = mph.GetParticipantEmailDB(mpModel.participantID);
                if (manageParticipantStatus.success)
                {
                    mpModel.participantEmail = manageParticipantStatus.participantEmail;
                }
                else
                {
                    ModelState.AddModelError("ParticipantID", manageParticipantStatus.errormessage);
                    mpModel.participantEmail = "";
                }
            }
            catch (Exception)
            {
                throw;
            }
            mpModel.participants = mph.GetParticipantsInStudyDB(mpModel.studyID);
            return View("ManageParticipants", mpModel);
        }
    }
}