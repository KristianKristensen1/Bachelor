using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FrontEndBA.Models;


namespace FrontEndBA.Controllers
{
    public class HomepageController : Controller
    {
        // GET: HomePage
        [HttpGet]
        public ActionResult Participant(Participant participant)
        {
            ManageStudyHandler mst = new ManageStudyHandler();
            Studies studiesCollection = new Studies();
            studiesCollection.myParticipantStudies = mst.GetMyParticipantStudiesDB(participant.IdParticipant);
            studiesCollection.relevantStudies = mst.GetRelevantStudiesDB(participant);
            return View(studiesCollection);
        }

        [Authorize]
        //[Authorize(Policy = "RequiresVerified")]
        public ActionResult Researcher(Researcher researcher)
        {
            ManageStudyHandler mst = new ManageStudyHandler();
            Studies studiesCollection = new Studies();
            studiesCollection.allStudies = mst.GetAllStudiesDB();
            studiesCollection.myResearcherStudies = mst.GetMyResearcherStudiesDB(researcher.IdResearcher);
            return View(studiesCollection);
        }

        [Authorize]
        public ActionResult AddStudyView()
        {
            return RedirectToAction("Index", "CreateStudy");
        }

        // GET: HomePage/Details/5
    
    }
}