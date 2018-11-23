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
using FrontEndBA.Utility;
using StudyManagementSystem.DAOImplementations;
using StudyManagementSystem.DAOInterfaces;
using FrontEndBA.Utility.HomepageHelpers;


namespace FrontEndBA.Controllers
{
    public class HomepageController : Controller
    {

        [Authorize(Policy = "RequiresParticipant")]
        public ActionResult Participant()
        {
            //Gets the id from JWT. The id is used to retrieve user from database. 
            int partID = IdentityHelper.getUserID(User);


            //Creates a ParticipantHomepageModel
            ParticipantHomepageHelper participantHomepageHelper = new ParticipantHomepageHelper();
            ParticipantHomepageModel participantHomepageModel = participantHomepageHelper.CreateParticipantHomepageModel(partID);
            return View(participantHomepageModel );
        }

        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult Researcher()
        {
            //Gets the id from JWT. The id is used to retrieve user from database. 
            int resID = IdentityHelper.getUserID(User);

            ResearcherHomepageHelper researcherHomepageHelper = new ResearcherHomepageHelper();
            ResearcherHomepageModel researcherHomepageModel = researcherHomepageHelper.CreateResearcherHompepageModel(resID);
            return View(researcherHomepageModel);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public ActionResult AddStudyView()
        {
           return RedirectToAction("Index", "CreateStudy");
        }
    }
}