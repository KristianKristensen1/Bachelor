﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.CreateStudyModel;
using FrontEndBA.Models.SharedModels;
using FrontEndBA.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class CreateStudyController : Controller
    {
        // GET: CreateStudy
        private CreateStudyHelper cshelper;
        [Authorize]
        public ActionResult Index()
        {
           cshelper = new CreateStudyHelper();
            return View(cshelper.DefaultCreateStudyModel());
        }


        public ActionResult ReturnToHomePage()
        {
            return RedirectToAction("Researcher", "Homepage");
        }
        // POST: CreateStudy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(CreateStudyModel csModel)
        {
          //modelstate?
            if (ModelState.IsValid)
            {
            try
                {
                    //Gets the id from JWT. The id is used to retrieve user from database. 
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    int id = Convert.ToInt32(claims.ElementAt(3).Value);

                // Convert to create format
                 
                CreateStudyHelper cshelper = new CreateStudyHelper();
                    var curStudy = cshelper.ConvertStudy(csModel,id);
                    var curCriteria = cshelper.ConvertInclusioncriteria(csModel);


                    bachelordbContext db = new bachelordbContext();
                    ManageStudyHandler manageStudyHandler = new ManageStudyHandler(db);
                    manageStudyHandler.CreateStudyDB(curStudy, curCriteria);


                    return RedirectToAction("Researcher", "Homepage");
                }
            catch(Exception e)
            {
                cshelper = new CreateStudyHelper();
                ////cshelper.ErrorHandle(curCriteria,cs,curStudy
                return View("Index");
            }
            }
               
            
            return View("./Index");

        }       
    }
}