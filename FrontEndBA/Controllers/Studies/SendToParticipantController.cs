using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.EmailModels;
using Microsoft.AspNetCore.Mvc;
using FrontEndBA.Utility;
using FrontEndBA.Utility.EmailHelper;

namespace FrontEndBA.Controllers.Studies
{
    public class SendToParticipantController : Controller
    {
        public IActionResult Index(int studyID)
        {
            ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();
           SendingModel sendToParticipantModel = new SendingModel();
            sendToParticipantModel.studies = viewStudyModelHelper.createViewStudyModel(studyID);
            return View(sendToParticipantModel);
        }

        public IActionResult Create(SendingModel sModel,int studyID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Gets the id from JWT. The id is used to retrieve user from database. 
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    int id_researcher = Convert.ToInt32(claims.ElementAt(3).Value);


                    //
                    ViewStudyModelHelper viewStudyModelHelper = new ViewStudyModelHelper();
                    sModel.studies = viewStudyModelHelper.createViewStudyModel(studyID);

                    ManageStudyHandler msh  = new ManageStudyHandler(new bachelordbContext());
                   List<Participant> participants= msh.getListParticipants(sModel.studies.study.IdStudy);

                    // Convert to create the right format
                    EmailToParticipantHelper emailHelper = new EmailToParticipantHelper();
                    
                    emailHelper.SendMessages(sModel,participants);

                    RedirectToAction("Researcher", "Homepage");

                }
                catch (Exception e)
                {
                   
                    ////cshelper.ErrorHandle(curCriteria,cs,curStudy
                    return View("Index");
                }
            }
            return View("index");
        }
    }
}