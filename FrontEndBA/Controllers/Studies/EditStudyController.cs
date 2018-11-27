using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using FrontEndBA.Utility;
using FrontEndBA.Models.CreateStudy;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndBA.Controllers.Studies
{
    public class EditStudyController : Controller
    {
        private CreateStudyHelper cshelper;

        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Index(int studyID)
        {
            EditStudyHelper editStudyHelper = new EditStudyHelper();
            return View(editStudyHelper.CreateEditStudyModel(studyID));
        }

        public ActionResult ReturnToHomepage()
        {
            //redirects to the welcome page, and from there to the Homepage if the user is authorized. 
            return RedirectToAction("Participant", "Welcome");
        }

        // POST: CreateStudy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Policy ="RequiresResearcher")]
        public ActionResult Edit(CreateStudyModel csModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Gets the id from JWT. The id is used to retrieve user from database. 
                    int id_researcher = IdentityHelper.getUserID(User);

                    // Convert to create the right format
                    CreateStudyHelper cshelper = new CreateStudyHelper();
                    int id_study = csModel.currentStudy.IdStudy;
                    var curStudy = cshelper.ConvertStudy(csModel, id_researcher, id_study);
                    var curCriteria = cshelper.ConvertInclusioncriteria(csModel);

                    IManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
                    msh.EditStudyDB(curStudy, curCriteria);

                    return RedirectToAction("Researcher", "Homepage");
                }
                catch (Exception)
                {
                    cshelper = new CreateStudyHelper();
                    return View("Index");
                }
            }

            return RedirectToAction("Index", new { studyID = csModel.currentStudy.IdStudy});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult EditAsDraft(CreateStudyModel csModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Gets the id from JWT. The id is used to retrieve user from database. 
                    int id_researcher = IdentityHelper.getUserID(User);

                    // Convert to create the right format
                    CreateStudyHelper cshelper = new CreateStudyHelper();
                    int id_study = csModel.currentStudy.IdStudy;
                    var curStudy = cshelper.ConvertStudy(csModel, id_researcher, id_study);
                    var curCriteria = cshelper.ConvertInclusioncriteria(csModel);

                    //Storing as a draft
                    curStudy.Isdraft = true;

                    //Storing in the DB
                    IManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
                    msh.EditStudyDB(curStudy, curCriteria);

                    return RedirectToAction("Researcher", "Homepage");
                }
                catch (Exception)
                {
                    return View("Index");
                }
            }

            EditStudyHelper editStudyHelper = new EditStudyHelper();
            return View("Index", editStudyHelper.CreateEditStudyModel(csModel.currentStudy.IdStudy));
        }
    }
}