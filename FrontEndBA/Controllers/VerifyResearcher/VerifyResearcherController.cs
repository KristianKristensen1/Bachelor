using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models;
using FrontEndBA.Utility.VerifyResearcherHelper;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class VerifyResearcherController : Controller
    {
        public IActionResult Index()
        {
            VerifyResearcherHelper verifyResearcherHelper = new VerifyResearcherHelper();
            VerifyResearcherModel verifyResearcherModel = verifyResearcherHelper.CreateVerifyResearcherModel();

            return View("VerifyResearcher", verifyResearcherModel);
        }

        public ActionResult VerifyReseracher(VerifyResearcherModel verifyResearcherModel)
        {
            UserHandler userHandler = new UserHandler(new bachelordbContext());

            if (ModelState.IsValid)
            {
                try
                {
                    ManageParticipantStatus manageParticipantStatus = userHandler.VerifyResearcher(verifyResearcherModel.researcherID);
                    if (manageParticipantStatus.success)
                    {
                        return RedirectToAction("Index", "VerifyResearcher");
                    }
                    else
                    {
                        ModelState.AddModelError("researcherID", manageParticipantStatus.errormessage);

                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            verifyResearcherModel.UnverifiedResearchers = userHandler.getUnverifiedResearchersDB();
            verifyResearcherModel.AllResearchers = userHandler.getAllResearchersDB();
            return View("VerifyResearcher", verifyResearcherModel);


        }

        public ActionResult UnverifyResearcher(VerifyResearcherModel verifyResearcherModel)
        {
            UserHandler userHandler = new UserHandler(new bachelordbContext());
            if (ModelState.IsValid)
            {
                try
                {
                    ManageParticipantStatus manageParticipantStatus = userHandler.UnverifyResearcher(verifyResearcherModel.researcherID);
                    if (manageParticipantStatus.success)
                    {
                        return RedirectToAction("Index", "VerifyResearcher");
                    }
                    else
                    {
                        ModelState.AddModelError("researcherID", manageParticipantStatus.errormessage);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            verifyResearcherModel.UnverifiedResearchers = userHandler.getUnverifiedResearchersDB();
            verifyResearcherModel.AllResearchers = userHandler.getAllResearchersDB();
            return View("VerifyResearcher", verifyResearcherModel);
        }
    }
}