using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using Microsoft.AspNetCore.Authorization;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models;
using FrontEndBA.Utility;

namespace FrontEndBA.Controllers
{
    /// <summary>
    /// The ViewStudyController handles the 
    /// </summary>
    public class ViewStudyController : Controller
    {
        public string Index()
        {
            return "Hallo";
        }


        public string Participant(int? id)
        {

            return "Id is " + id;
        }

        [Authorize]
        public ActionResult ViewStudy(int studyID)
        {
            //Creates a ViewStudyModel containing a Study and InclusionCriteria.
            ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();

            return View(viewStudyModelHelper.createViewStudyModel(studyID));
        }

        public ActionResult ReturnToHomepage()
        {
            //redirects to the welcome page, and from there to the Homepage if the user is authorized. 
            return RedirectToAction("Participant", "Welcome");
        }

    }
}