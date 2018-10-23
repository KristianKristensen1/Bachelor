using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FrontEndBA.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudyManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


namespace FrontEndBA.Controllers
{
    public class HomepageController : Controller
    {
        // GET: HomePage
        [HttpGet]
        [Authorize]
        public ActionResult Participant(int id)
        {
            Participant participant = getParticipant(id);
            ManageStudyHandler mst = new ManageStudyHandler();
            Studies studiesCollection = new Studies();            
            studiesCollection.relevantStudies = mst.GetRelevantStudiesDB(participant);            

            //NOT YET IMPLEMENTED
            //studiesCollection.myParticipantStudies = mst.GetMyParticipantStudiesDB(participant.IdParticipant);

            //INSTEAD FAKE
            List<Study> fakelist = new List<Study>();
            Study fakestudy = new Study();
            fakestudy.Description = "Test";
            fakestudy.Tag = "Tag";
            fakestudy.Isdraft = true;
            fakestudy.Name = "This is a name of study1";
            Study fakestudy2 = new Study();
            fakestudy2.Description = "2Test";
            fakestudy2.Tag = "2Tag";
            fakestudy2.Isdraft = false;
            fakestudy2.Name = "This is a name of study2";
            fakelist.Add(fakestudy2);
            fakelist.Add(fakestudy);

            studiesCollection.myParticipantStudies = fakelist;

            return View(studiesCollection);
        }

        [Authorize]
        //[Authorize(Policy = "RequiresVerified")]
        public ActionResult Researcher(int id)
        {
            Researcher researcher = getResearcher(id);
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

        public Participant getParticipant(int id)
        {
            UserHandler userHandler = new UserHandler();
            Participant participant = userHandler.getParticipant(id);
            return participant;
        }

        public Researcher getResearcher(int id)
        {
            UserHandler userHandler = new UserHandler();
            Researcher researcher = userHandler.getResearcher(id);
            return researcher;
        }
    
    }
}