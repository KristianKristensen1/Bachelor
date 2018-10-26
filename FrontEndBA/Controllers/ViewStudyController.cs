using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;
using Microsoft.AspNetCore.Authorization;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models;

namespace FrontEndBA.Controllers
{

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
            
            Study studyToShow = GetStudy(studyID);
            Inclusioncriteria incToShow = GetInclusioncriteria(studyID);

            ViewStudyModel vm = new ViewStudyModel();
            vm.study = studyToShow;
            vm.inclusioncriteria = incToShow;
            
            return View(vm);
        }

        public Study GetStudy(int id)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            return msh.getStudyDB(id);
        }

        public Inclusioncriteria GetInclusioncriteria(int id)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            return msh.getInclusioncriteriaDB(id);
        }
    }
}