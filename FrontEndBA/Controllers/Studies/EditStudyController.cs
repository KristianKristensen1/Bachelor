using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using FrontEndBA.Utility;
using FrontEndBA.Models.CreateStudy;
using System.Security.Claims;

namespace FrontEndBA.Controllers.Studies
{
    public class EditStudyController : Controller
    {
        private CreateStudyHelper cshelper;
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
        //[Authorize]
        public ActionResult Edit([Bind("Description,Isdraft,Name,Abstract,Pay,Duration,Preparation,EligibilityRequirements")] Study curStudy,
            [Bind("IsMale,IsFemale,MinAge,MaxAge,English,IdReseacher")] Inclusioncriteria curCriteria, CreateStudyModel cs)
        {
            //modelstate?
            if (ModelState.IsValid)
            {
                try
                {
                    //Gets the id from JWT. The id is used to retrieve user from database. 
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    int id = Convert.ToInt32(claims.ElementAt(3).Value);

                    // Convert to create format
                    CreateStudyHelper cshelper = new CreateStudyHelper();
                    curStudy = cshelper.ConvertStudy(cs, id);
                    curCriteria = cshelper.ConvertInclusioncriteria( cs);



                    ManageStudyHandler manageStudyHandler = new ManageStudyHandler(new bachelordbContext());
                    manageStudyHandler.EditStudy(curStudy, curCriteria);


                    return RedirectToAction("Researcher", "Homepage");
                }
                catch (Exception e)
                {
                    cshelper = new CreateStudyHelper();
                    ////cshelper.ErrorHandle(curCriteria,cs,curStudy
                    return View("Index");
                }
            }
            return View("./Index");

        }
    }
}