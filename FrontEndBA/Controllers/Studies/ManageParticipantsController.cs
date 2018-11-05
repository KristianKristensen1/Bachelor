using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models;
using FrontEndBA.Utility;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;

namespace FrontEndBA.Controllers.Studies
{
    public class ManageParticipantsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ManageParticipants(int studyID, string studyName)
        {
            ManageParticipantsHelper manageParticipantsHelper = new ManageParticipantsHelper();
            ManageParticipantModel manageParticipantModel = new ManageParticipantModel();
            manageParticipantModel = manageParticipantsHelper.CreateManageParticipantModel(studyID, studyName);

            return View(manageParticipantModel);
        }


        public ActionResult AddParticipant(ManageParticipantModel mpModel)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());

            if (ModelState.IsValid)
            {
                try
                {
                    ManageParticipantStatus manageParticipantStatus = msh.AddParticipantToStudyDB(mpModel.participantID, mpModel.studyID);
                    if (manageParticipantStatus.success)
                    {
                        return RedirectToAction("ManageParticipants", "ManageParticipants", new { studyID = mpModel.studyID, studyName = mpModel.participantID });
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
            mpModel.participants = msh.getParticipantsDB(mpModel.studyID);
            return View("ManageParticipants", mpModel);
        }

        public ActionResult RemoveParticipant(ManageParticipantModel mpModel)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            if (ModelState.IsValid)
            {
                try
                {
                    ManageParticipantStatus manageParticipantStatus = msh.RemoveParticipantFromStudyDB(mpModel.participantID, mpModel.studyID);
                    if (manageParticipantStatus.success)
                    {
                        return RedirectToAction("ManageParticipants", "ManageParticipants", new { studyID = mpModel.studyID, studyName = mpModel.participantID });
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
            mpModel.participants = msh.getParticipantsDB(mpModel.studyID);
            return View("ManageParticipants", mpModel);
        }
    }
}