using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class CreateStudyController : Controller
    {
        // GET: CreateStudy
        public ActionResult Index()
        {
            return View();
        }


       

        // POST: CreateStudy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description,Isdraft,Tag,IdResearcher")] Study studymodel)
        {
            try
            {
                ManageStudyHandler manageStudyHandler = new ManageStudyHandler();
                manageStudyHandler.CreateStudyDB(studymodel);

                return View("../Homepage/Participant");
            }
            catch
            {
                return View("./Index");
            }
        }

       
    }
}