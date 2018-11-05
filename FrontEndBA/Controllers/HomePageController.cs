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
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using StudyManagementSystem.DAOImplementations;
using StudyManagementSystem.DAOInterfaces;


namespace FrontEndBA.Controllers
{
    public class HomepageController : Controller
    {

        [Authorize(Policy = "RequiresParticipant")]
        public ActionResult Participant()
        {
            //Gets the id from JWT. The id is used to retrieve user from database. 
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int id = Convert.ToInt32(claims.ElementAt(3).Value);
            
            bachelordbContext db = new bachelordbContext();
            Participant participant = getParticipant(id);
            IViewStudyHandler vsh = new ViewStudyHandler(new bachelordbContext());
            Models.Studies studiesCollection = new Models.Studies();
            
            //Gets the relevant studies
            studiesCollection.relevantStudies = vsh.GetRelevantStudiesDB(participant);            

            //Gets the studies that the participant is enrolled in. 
            studiesCollection.myParticipantStudies = vsh.GetMyParticipantStudiesDB(participant.IdParticipant);

            return View(studiesCollection);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult Researcher()
        {
            //Gets the id from JWT. The id is used to retrieve user from database. 
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int id = Convert.ToInt32(claims.ElementAt(3).Value);

            Researcher researcher = getResearcher(id);
            IViewStudyHandler vsh = new ViewStudyHandler(new bachelordbContext());
            Models.Studies studiesCollection = new Models.Studies();
            studiesCollection.allStudies = vsh.GetAllStudiesDB();
            studiesCollection.myResearcherStudies = vsh.GetMyResearcherStudiesDB(researcher.IdResearcher);
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