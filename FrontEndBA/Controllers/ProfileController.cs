using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models;
using FrontEndBA.Models.ProfileModel;
using FrontEndBA.Utility.HomepageHelpers;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Participant()
        {
            ParticipantProfileModel ppm = new ParticipantProfileModel();
            ParticipantHomepageHelper model = new ParticipantHomepageHelper();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int partID = Convert.ToInt32(claims.ElementAt(3).Value);
           
            ppm.Id = partID;
            var curParticipant = model.getParticipant(partID);
            ppm.Email = curParticipant.Email;
            ppm.Password = curParticipant.Password;
            ppm.ValidInput = true;
            return View(ppm);
            
          


            
           
        }
        public IActionResult SaveEmailParticipant(ParticipantProfileModel pmodel)
        {
            ParticipantHomepageHelper model = new ParticipantHomepageHelper();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int partID = Convert.ToInt32(claims.ElementAt(3).Value);
            pmodel.Id=partID;
            var curParticipant = model.getParticipant(partID);
            curParticipant.Email = pmodel.Email;

            // Call Db here
            return RedirectToAction("Participant");
        }

        public IActionResult SavePasswordParticipant(ParticipantProfileModel ppm)
        {
            if (ModelState.IsValid)
            {
                //Check that old password is correct



                return RedirectToAction("Participant");
            }

            ParticipantHomepageHelper model = new ParticipantHomepageHelper();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int partID = Convert.ToInt32(claims.ElementAt(3).Value);

            ppm.Id = partID;
            var curParticipant = model.getParticipant(partID);
            ppm.Email = curParticipant.Email;
            ppm.Password = curParticipant.Password;
            ppm.ValidInput = false;
            
            return View("Participant", ppm);
        }

        public IActionResult SavePasswordResearcher(ResearcherProfileModel ppm)
        {

            return View("Researcher");
        }


        public IActionResult Researcher()
        {
            ResearcherProfileModel rpm = new ResearcherProfileModel();
            ResearcherHomepageHelper model = new ResearcherHomepageHelper();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int partID = Convert.ToInt32(claims.ElementAt(3).Value);
            var curResearcher = model.getResearcher(partID);
            rpm.Id = partID;
            rpm.Verify = curResearcher.Isverified;
            rpm.Admin = curResearcher.Isadmin;
            rpm.Firstname = curResearcher.FirstName;
            rpm.Lastname = curResearcher.LastName;
            rpm.Email = curResearcher.Email;
            rpm.OldPassword = curResearcher.Password;

            return View(rpm);
        }

        public IActionResult SaveEmailResearcher(ResearcherProfileModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int partID = Convert.ToInt32(claims.ElementAt(3).Value);

            Researcher curResearcher = new Researcher();
            curResearcher.Email = model.Email;
            


            return RedirectToAction("Researcher");
        }

        public IActionResult Back()
        {
            if (User.Claims.Count() != 0)
            {
                //Checks if the user is verified as a participant
                if (User.Claims.ElementAt(2).Value == "Y")
                {
                    return RedirectToAction("Participant","Homepage");
                }
                //Checks if the user is verified as a researcher
                if (User.Claims.ElementAt(1).Value == "Y")
                {
                    return RedirectToAction("Researcher", "Homepage");
                }
                if (User.Claims.ElementAt(1).Value == "N")
                {
                    return RedirectToAction("Researcher", "Homepage");
                }
            }

            return RedirectToAction("Participant", "Welcome");
        }
    }
}