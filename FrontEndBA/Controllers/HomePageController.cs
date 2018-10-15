using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class HomepageController : Controller
    {
        // GET: HomePage    
        public ActionResult Participant()
        {
     
            return View();
        }

        public ActionResult Researcher()
        {
            List<Study> fakelist = new List<Study>();
            Study fakestudy = new Study();
            fakestudy.Description = "Test";
            fakestudy.Tag = "Tag";
            fakestudy.Isdraft = true;
            Study fakestudy2 = new Study();
            fakestudy2.Description = "2Test";
            fakestudy2.Tag = "2Tag";
            fakestudy2.Isdraft = false;
            fakelist.Add(fakestudy2);
            fakelist.Add(fakestudy);
            return View(fakelist);
        }


        public ActionResult AddStudyView()
        {
            return RedirectToAction("Index", "CreateStudy");
        }


    }
}