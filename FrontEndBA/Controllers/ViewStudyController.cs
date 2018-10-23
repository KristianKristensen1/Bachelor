using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;

namespace FrontEndBA.Controllers
{
    public class ViewStudyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ShowStudyParticipant(Study study)
        {
            return View("ViewStudy");
        }

        public ActionResult ShowStudyResearcher(Study study)
        {
            return View("ViewStudy");
        }
    }
}