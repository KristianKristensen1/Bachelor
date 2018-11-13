using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models;
using FrontEndBA.Utility.VerifyResearcherHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class VerifyResearcherController : Controller
    {
        [Authorize(Policy = "RequiresAdmin")]
        public IActionResult Index()
        {
            VerifyResearcherHelper verifyResearcherHelper = new VerifyResearcherHelper();
            VerifyResearcherModel verifyResearcherModel = verifyResearcherHelper.CreateVerifyResearcherModel();

            return View("VerifyResearcher", verifyResearcherModel);
        }

        [Authorize(Policy = "RequiresAdmin")]
        public ActionResult VerifyReseracher(VerifyResearcherModel verifyResearcherModel)
        {
            IUserHandler ush = new UserHandler(new bachelordbContext());

            if (ModelState.IsValid)
            {
                try
                {
                    DbStatus manageParticipantStatus = ush.VerifyResearcherDB(verifyResearcherModel.researcherID);
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
            verifyResearcherModel.UnverifiedResearchers = ush.GetUnverifiedResearchersDB();
            verifyResearcherModel.AllResearchers = ush.GetAllResearchersDB();
            return View("VerifyResearcher", verifyResearcherModel);
        }

        [Authorize(Policy = "RequiresAdmin")]
        public ActionResult UnverifyResearcher(VerifyResearcherModel verifyResearcherModel)
        {
            IUserHandler ush = new UserHandler(new bachelordbContext());
            if (ModelState.IsValid)
            {
                try
                {
                    DbStatus manageParticipantStatus = ush.UnverifyResearcherDB(verifyResearcherModel.researcherID);
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
            verifyResearcherModel.UnverifiedResearchers = ush.GetUnverifiedResearchersDB();
            verifyResearcherModel.AllResearchers = ush.GetAllResearchersDB();
            return View("VerifyResearcher", verifyResearcherModel);
        }
    }
}