using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.CreateStudyModel;
using FrontEndBA.Models.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class CreateStudyController : Controller
    {
        // GET: CreateStudy
        //[Authorize]
        public ActionResult Index()
        {
            CreateStudyModel cs = new CreateStudyModel();
            cs.inclusioncriteria = new InclusioncriteriaModel();
            cs.currentStudy = new StudyModel();
            cs.inclusioncriteria.English = false;
            cs.inclusioncriteria.IsFemale = false;
            cs.inclusioncriteria.IsMale = false;
            return View();
        }


        public ActionResult ReturnToHomePage()
        {
            return View("../Homepage/Researcher");
        }

        // POST: CreateStudy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Create([Bind("Description,Isdraft,Name,Abstract,Pay,Duration,Preparation,EligibilityRequirements")] StudyModel studymodel,
            [Bind("IsMale,IsFemale,MinAge,MaxAge,English,IdReseacher")] InclusioncriteriaModel criteriamodel,CreateStudyModel cs)
        {
            if (!ModelState.IsValid)
            {
                try
            {
                var curStudy = new Study();
                var curCriteria = new Inclusioncriteria();
                ManageStudyHandler manageStudyHandler = new ManageStudyHandler();
                manageStudyHandler.CreateStudyDB(curStudy, curCriteria);

                return View("../Homepage/Participant");
            }
            catch
            {
                return View("./Index");
            }
            }
            return View("./Index");

        }       
    }
}