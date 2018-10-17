﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FrontEndBA.Controllers
{
    public class HomepageController : Controller
    {
        // GET: HomePage
        [HttpGet]
        public ActionResult Participant() //Similar to the reseachers?
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

        [Authorize]
        //[Authorize(Policy = "RequiresVerified")]
        public ActionResult Researcher(Researcher researcher)
        {
            ManageStudyHandler mst = new ManageStudyHandler();

            //MY RESEARCHER STUDIES TEST //IMPEDED
            //List<Study> myResearcherStudies = new List<Study>();
            //int id = researcher.IdResearcher;
            //myResearcherStudies = mst.GetMyResearcherStudiesDB(id);
            //return View(myResearcherStudies);

            List<Study> allStudies = new List<Study>();
            int id = researcher.IdResearcher;
            allStudies = mst.GetAllStudiesDB();
            return View(allStudies);
        }

        [Authorize]
        public ActionResult AddStudyView()
        {
            return RedirectToAction("Index", "CreateStudy");
        }

        // GET: HomePage/Details/5
    
    }
}