using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEndBA.Utility.ProfileHelper;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Participant()
        {
            return View();
        }

        public ActionResult Researcher()
        {
            //Gets the id from JWT. The id is used to retrieve user from database. 
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            int id = Convert.ToInt32(claims.ElementAt(3).Value);

            ResearcherProfileHelper researcherProfileHelper = new ResearcherProfileHelper();
            return View(researcherProfileHelper.CreateResearcherProfileModel(id));
        }
    }
}