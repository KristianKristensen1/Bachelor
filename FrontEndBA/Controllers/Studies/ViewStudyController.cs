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

        [Authorize]
        public ActionResult ViewStudyDraft(int studyID)
        {
            EditStudyHelper editStudyHelper = new EditStudyHelper();

            return View(editStudyHelper.CreateEditStudyModel(studyID));
        }

       
    }
}