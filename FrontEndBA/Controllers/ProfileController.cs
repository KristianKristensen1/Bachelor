﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models;
using FrontEndBA.Models.ProfileModel;
using FrontEndBA.Utility;
using FrontEndBA.Utility.HomepageHelpers;
using Microsoft.AspNetCore.Mvc;
using FrontEndBA.Utility.ProfileHelper;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndBA.Controllers.EditProfile
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            if (User.Claims.Count() != 0)
            {
                //Checks if the user is verified as a participant
                if (User.Claims.ElementAt(2).Value == "Y")
                {
                    return RedirectToAction("Participant");
                }
                //Checks if the user is verified as a researcher
                if (User.Claims.ElementAt(1).Value == "Y")
                {
                    return RedirectToAction("Researcher");
                }
                if (User.Claims.ElementAt(1).Value == "N")
                {
                    return RedirectToAction("Researcher");
                }
            }

            return RedirectToAction("Participant", "Welcome");
        }

        [Authorize(Policy = "RequiresParticipant")]
        public IActionResult Participant()
        {
            
      
            //Getting user ID
            int partID = IdentityHelper.getUserID(User);

            //Getting default participant model obj.
            ParticipantProfileModel ppm = ProfileParticipantHelper.getdefaultParticipant(partID);
            ppm.ValidInput = true;
      
            return View(ppm);

        }


        [Authorize(Policy = "RequiresParticipant")]
        public IActionResult SaveEmailParticipant(ParticipantProfileModel pmodel)
        {
            //Getting user ID
            int partID = IdentityHelper.getUserID(User);
            //Checking if there is a valid Email
            if (pmodel.Email == null)
            {
                var err = "A Participant must have a Email";

                this.ModelState.AddModelError("Email", err.ToString());
            }
            else
            {
                if (ModelState.IsValid)
                {

                    //Creating local Participant obj.
                    Participant part = new Participant
                    {
                        Email = pmodel.Email,
                        IdParticipant = partID,
                        English = pmodel.English,
                    };

                    // Call Db here
                    IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
                    mph.ChangeProfileParticipantDB(part);

                    return RedirectToAction("Participant");
                }
            }
           

            //Return view to show error message if something wrong happend. 
            ParticipantProfileModel ppm = ProfileParticipantHelper.getdefaultParticipant(partID);
          
            ppm.ValidInput = true;

            return View("Participant",ppm);

        }


        [Authorize(Policy = "RequiresParticipant")]
        public IActionResult SavePasswordParticipant(ParticipantProfileModel ppm)
        {
            //Getting user ID
            int partID = IdentityHelper.getUserID(User);
            ppm.SuccesChangePassword = false;
            var err = "";

            if (ModelState.IsValid && ppm.Password != null)
            {


                //Creating a local version with changes parametes of Participant obj.
                IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
                    Participant curpart = new Participant
                    {
                        Email = ppm.Email,
                        IdParticipant = partID,
                        Password = ppm.Password,
                    };

                var status = mph.ChangePasswordParticipantDB(curpart, ppm.OldPassword);
                //Check that old password is correct    
                if (status.success)
                {
                   
                    ParticipantHomepageHelper helper = new ParticipantHomepageHelper();
                    var curParticipants = helper.getParticipant(partID);
                    //Creates a new part obj. since SuccesChange has to be true to show dialog
                    ParticipantProfileModel sppm =
                        ProfileParticipantHelper.convertToModel(curParticipants, status.success, true);

                    return View("Participant",sppm);
                }
                else
                {
                    //Error message if Password did not match
                    err = status.errormessage;
                
                    this.ModelState.AddModelError("OldPassword", err.ToString());
                }
            }
            else
            {
                //Error message if Password was not put in.
                err = "Must Assign a Password";
                this.ModelState.AddModelError("Password", err.ToString());
            }

            return View("Participant", ProfileParticipantHelper.getdefaultParticipant(partID));
        }


        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult SavePasswordResearcher(ResearcherProfileModel rpm)
        {
            int resID = IdentityHelper.getUserID(User);
            var err = "";

            if (ModelState.IsValid && rpm.Password!=null)
            {


                //Creating a local version with changes parametes of Participant obj.
                IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
                Researcher curResearcher = new Researcher
                {
                    Email = rpm.Email,
                    IdResearcher = resID,
                    Password = rpm.Password,
                };

                var status = mph.ChangePasswordResearcherDB(curResearcher, rpm.OldPassword);
                //Check that old password is correct    
                if (status.success)
                {
                    ResearcherProfileModel srpm = new ResearcherProfileModel();
                    ResearcherHomepageHelper smodel = new ResearcherHomepageHelper();
           
                    var curSResearcher = smodel.getResearcher(resID);
                    srpm = ProfileResearcherHelper.convertToModel(curSResearcher,status.success,true);
                    return View("Researcher", srpm);
                }
                else
                {
                    err = status.errormessage;

                    this.ModelState.AddModelError("OldPassword", err.ToString());
                }

            }
            else
            {
                err = "Must Assign a Password";
                this.ModelState.AddModelError("Password", err.ToString());
            }

        
            rpm = ProfileResearcherHelper.getdefaultResearcher(resID);
            return View("Researcher",rpm);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult Researcher()
        {

            //Getting user ID
            int resID = IdentityHelper.getUserID(User);
            //Creating default Researhcer Model to view
            ResearcherProfileModel rpm = ProfileResearcherHelper.getdefaultResearcher(resID);
            rpm.ValidInput = true;
            return View(rpm);
        }

        [Authorize(Policy = "RequiresResearcher")]
        public IActionResult SaveEmailResearcher(ResearcherProfileModel model)
        {
            //Getting user ID
            int resID = IdentityHelper.getUserID(User);
            //Checking if user input a Email and Name
            if (model.Email == null)
            {
                var err = "A Researcher must have a Email";

                this.ModelState.AddModelError("Email", err.ToString());
            }
            else if (model.Firstname == null)
            {
                var err = "A Researcher must have a Firstname";

                this.ModelState.AddModelError("Firstname", err.ToString());
            }
            else if (model.Lastname == null)
            {
                var err = "A Researcher must have a Lastname";

                this.ModelState.AddModelError("Lastname", err.ToString());
            }
            else
            {
                if (ModelState.IsValid)
                {

                    //Creating local researrhcer object with model parameters.
                    Researcher curResearcher = new Researcher
                    {
                        Email = model.Email,
                        IdResearcher = resID,
                        FirstName = model.Firstname,
                        LastName = model.Lastname
                    };


                    // Call Db here
                    IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
                    mph.ChangeProfileResearcherDB(curResearcher);
                    
                    return RedirectToAction("Researcher");
                }
            }

            model = ProfileResearcherHelper.getdefaultResearcher(resID);
            model.ValidInput = true;
            return View("Researcher", model);
        }

     
        [Authorize(Policy = "RequiresParticipant")]
        public IActionResult DeleteAccountParticipant(int partID)
        {
            IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
            mph.DeleteAccountParticipantDB(partID);

            return RedirectToAction("LogoutParticipant", "Welcome");
        }
    }
}