﻿using BachelorBackEnd;
using JwtAuthenticationHelper.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using StudyManagementSystem.Models;

namespace FrontEndBA.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly IJwtTokenGenerator tokenGenerator;
        public WelcomeController(IJwtTokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
        }
        public ActionResult Participant()
        {
            return View();
        }

        // POST: Welcome/LoginParticipant
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Route("LoginParticipant")]
        [Route("Welcome/LoginParticipant")]
        public async Task<IActionResult> LoginParticipant([Bind("Email,Password")] Participant participant)
        {
            try
            {
                ILoginHandler loginhandler = new LoginHandler();
                //Checks whether or not the participant is in the database
                var status = loginhandler.LoginParticipantDB(participant.Email, participant.Password);
                if (status.LoginStatus.IsSuccess)
                {
                    //Create an object with userinfo about the participant.
                    var userInfo = new UserInfo
                    {
                        hasAdminRights = false,
                        hasParticipantRights = true,
                        hasResearcherRights = false
                    };

                    //Generates token with claims defined from the userinfo object.
                    var accessTokenResult = tokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
                    participant.Email,
                    AddMyClaims(userInfo));
                    await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal,
                        accessTokenResult.AuthProperties);

                    //Redirects to the participant homepage
                    return RedirectToAction("Participant", "Homepage", status.LoginStatus.participant);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Route("LoginResearcher")]
        [Route("Welcome/LoginResearcher")]
        public async Task<IActionResult> LoginResearcher([Bind("Email,Password")] Researcher researcher)
        {
            try
            {
                ILoginHandler loginhandler = new LoginHandler();
                //Checks whether or not the participant is in the database
                var status = loginhandler.LoginResearcherDB(researcher.Email, researcher.Password);
                if (status.LoginStatus.IsSuccess)
                {
                    //Create an object with userinfo about the participant.
                    var userInfo = new UserInfo
                    {
                        hasAdminRights = status.LoginStatus.researcher.Isadmin,
                        hasResearcherRights = status.LoginStatus.researcher.Isverified,
                        hasParticipantRights = false
                    };

                   
                    //Generates token with claims defined from the userinfo object.
                    var accessTokenResult = tokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
                    researcher.Email,
                    AddMyClaims(userInfo));
                    await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal,
                        accessTokenResult.AuthProperties);

                    //Redirects to the researcher homepage
                    return RedirectToAction("Researcher", "Homepage", status.LoginStatus.researcher);
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
            catch (Exception)
            {
                //Handle error
                throw;
            }
        }

        public ActionResult Researcher()
        {
            return View();
        }



        private static IEnumerable<Claim> AddMyClaims(UserInfo userInfo)
        {
            var myClaims = new List<Claim>
            {
                new Claim("HasAdminRights", userInfo.hasAdminRights ? "Y" : "N"),
                new Claim("HasResearcherRights", userInfo.hasResearcherRights ? "Y" : "N"),
                new Claim("HasParticipantRights", userInfo.hasParticipantRights ? "Y" : "N"),
            };

            return myClaims;
        }
    }
}