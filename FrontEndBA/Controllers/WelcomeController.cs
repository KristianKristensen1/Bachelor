using BachelorBackEnd;
using JwtAuthenticationHelper.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using StudyManagementSystem.Configs;

namespace FrontEndBA.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IJwtTokenGenerator tokenGenerator;

      
        public WelcomeController(IJwtTokenGenerator tokenGenerator, IConfiguration configuration)
        {
            this.tokenGenerator = tokenGenerator;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        public ActionResult Participant()
        {
            ConfigStrings.Connectionstring = configuration.GetConnectionString("DbConnectionstring");
            //Redirects the user to the right page based on their authentication status            
            if (User.Claims.Count() != 0)
            {
                //Checks if the user is verified as a participant
                if (User.Claims.ElementAt(2).Value == "Y")
                {
                    return RedirectToAction("Participant", "Homepage");
                }
                //Checks if the user is verified as a researcher
                if (User.Claims.ElementAt(1).Value == "Y")
                {
                    return RedirectToAction("Researcher", "Homepage");
                }
                if (User.Claims.ElementAt(1).Value == "N")
                {
                    return View("../NotVerifiedResearcher/NotVerifiedResearcher");
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Researcher()
        {
            //Redirects the user to the right page based on their authentication status            
            if (User.Claims.Count() != 0)
            {
                //Checks if the user is verified as a participant
                if (User.Claims.ElementAt(2).Value == "Y")
                {
                    return RedirectToAction("Participant", "Homepage");
                }
                //Checks if the user is verified as a researcher
                if (User.Claims.ElementAt(1).Value == "Y")
                {
                    return RedirectToAction("Researcher", "Homepage");
                }
                if (User.Claims.ElementAt(1).Value == "N")
                {
                    return View("NotVerifiedResearcher");
                }
            }
            return View();
        }


        // POST: Welcome/LoginParticipant
        [AllowAnonymous]
        [Route("LoginParticipant")]
        [Route("Welcome/LoginParticipant")]
        public async Task<IActionResult> LoginParticipant(Participant participant)
        {
            try
            {
                bachelordbContext db = new bachelordbContext();
                ILoginHandler loginhandler = new LoginHandler(db);
                //Checks whether or not the participant is in the database
                var status = loginhandler.LoginParticipantDB(participant.Email, participant.Password);
                if (status.LoginStatus.IsSuccess)
                {
                    //Create an object with userinfo about the participant.
                    var userInfo = new UserInfo
                    {
                        hasAdminRights = false,
                        hasParticipantRights = true,
                        hasResearcherRights = false,
                        userID = "" + status.LoginStatus.participant.IdParticipant
                    };

                    //Generates token with claims defined from the userinfo object.
                    var accessTokenResult = tokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
                    participant.Email,
                    AddMyClaims(userInfo));
                    await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal,
                        accessTokenResult.AuthProperties);

                    //Redirects to the participant homepage
                    return RedirectToAction("Participant", "Homepage");
                }
                else
                {
                    var err = status.LoginStatus.ErrorMessage;
                    if (err == "Wrong password")
                        this.ModelState.AddModelError("Password", err.ToString());
                    else
                    {
                        this.ModelState.AddModelError("Email", err.ToString());
                    }
                }
                return View("Participant");
            }
            catch (Exception)
            {
                //Handle error
                throw;
            }
        }


        [AllowAnonymous]
        [Route("LoginResearcher")]
        [Route("Welcome/LoginResearcher")]
        public async Task<IActionResult> LoginResearcher(Researcher researcher)
        {
            try
            {
                bachelordbContext db = new bachelordbContext();
                ILoginHandler loginhandler = new LoginHandler(db);
               
                //Checks whether or not the participant is in the database
                var status = loginhandler.LoginResearcherDB(researcher.Email, researcher.Password);
                if (status.LoginStatus.IsSuccess)
                {
                    //Create an object with userinfo about the participant.
                    var userInfo = new UserInfo
                    {
                        hasAdminRights = status.LoginStatus.researcher.Isadmin,
                        hasResearcherRights = status.LoginStatus.researcher.Isverified,
                        hasParticipantRights = false,
                        userID = ""+status.LoginStatus.researcher.IdResearcher,
                    };

                   
                    //Generates token with claims defined from the userinfo object.
                    var accessTokenResult = tokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
                    researcher.Email,
                    AddMyClaims(userInfo));
                    await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal,
                        accessTokenResult.AuthProperties);

                    //Redirects to the researcher homepage
                    return RedirectToAction("Researcher", "Homepage");
                }
                else
                {
                    var err = status.LoginStatus.ErrorMessage;
                    if (err == "Wrong password")
                        this.ModelState.AddModelError("Password", err.ToString());
                    else
                    {
                        this.ModelState.AddModelError("Email", err.ToString());
                    }

                }
                return View("Researcher");
            }
            catch (Exception e)
            {
                var test = e;
                //Handle error
                throw;
            }
        }

        [AllowAnonymous]
        [Route("LogoutParticipant")]
        public async Task<IActionResult> LogoutParticipant()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Participant", "Welcome");
        }

        [AllowAnonymous]
        [Route("LogoutResearcher")]
        public async Task<IActionResult> LogoutResearcher()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Researcher", "Welcome");
        }

        private static IEnumerable<Claim> AddMyClaims(UserInfo userInfo)
        {
            var myClaims = new List<Claim> //Hvorfor Y/N i stedet for en bool? Skal det være string?
            {
                new Claim("HasAdminRights", userInfo.hasAdminRights ? "Y" : "N"),
                new Claim("HasResearcherRights", userInfo.hasResearcherRights ? "Y" : "N"),
                new Claim("HasParticipantRights", userInfo.hasParticipantRights ? "Y" : "N"),     
                new Claim("userID", userInfo.userID)
            };

            return myClaims;
        }
    }
}