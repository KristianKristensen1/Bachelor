using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEndBA.Models;
using FrontEndBA.Models.ProfileModel;
using FrontEndBA.Utility.HomepageHelpers;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;

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
            ParticipantHomepageHelper helper = new ParticipantHomepageHelper();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int partID = Convert.ToInt32(claims.ElementAt(3).Value);
           
            ppm.Id = partID;
            var curParticipant = helper.getParticipant(partID);
            ppm.Email = curParticipant.Email;
            ppm.Password = curParticipant.Password;
            ppm.English = curParticipant.English;

            return View(ppm);
        }

        public IActionResult SaveEmailParticipant(ParticipantProfileModel pmodel)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                int partID = Convert.ToInt32(claims.ElementAt(3).Value);

                Participant testpart = new Participant
                {
                    Email = pmodel.Email,
                    IdParticipant = partID,
                    English = pmodel.English,
                };

                // Call Db here
                IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
                mph.ChangeProfileParticipantDB(testpart);

                return RedirectToAction("Participant");
            }

            //Return view her i stedet for at få vist fejlmeddelelse
            return RedirectToAction("Participant", "Profile");

        }

        public IActionResult SavePasswordParticipant(ParticipantProfileModel ppm)
        {
            return RedirectToAction("Participant");
        }

        public IActionResult SavePasswordResearcher(ResearcherProfileModel ppm)
        {

            return RedirectToAction("Researcher");
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
            rpm.Name = curResearcher.FirstName + curResearcher.LastName;
            rpm.Email = curResearcher.Email;
            rpm.OldPassword = curResearcher.Password;

            return View(rpm);
        }

        public IActionResult SaveEmailResearcher(ResearcherProfileModel model)
        {
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

        public IActionResult DeleteAccountParticipant(int partID)
        {
            IManageProfileHandler mph = new ManageProfileHandler(new bachelordbContext());
            mph.DeleteAccountParticipantDB(partID);

            return RedirectToAction("LogoutParticipant", "Welcome");
        }
    }
}