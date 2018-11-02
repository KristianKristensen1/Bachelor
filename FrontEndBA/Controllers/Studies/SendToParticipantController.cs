using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models.CreateStudy;
using Microsoft.AspNetCore.Mvc;
using FrontEndBA.Utility;

namespace FrontEndBA.Controllers.Studies
{
    public class SendToParticipantController : Controller
    {
        public IActionResult Index(int studyID)
        {
            ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();
           
            return View(viewStudyModelHelper.createViewStudyModel(studyID));
        }

        public IActionResult Create()
        {

            return View();
        }
    }
}