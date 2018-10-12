using BachelorBackEnd;
using JwtAuthenticationHelper.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FrontEndBA.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly IJwtTokenGenerator tokenGenerator;
        public WelcomeController(IJwtTokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
        }
        public ActionResult WelcomePageParticipant()
        {
            return View(("WelcomePageParticipant"));
        }

        // POST: Welcome/LoginParticipant
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LoginParticipant")]
        [Route("Welcome/LoginParticipant")]
        public async Task<IActionResult> LoginParticipant([Bind("Email,Password")] Participant participant)
        {             
            ILoginHandler loginhandler = new LoginHandler();
            //Checks whether or not the participant is in the database
            var status = loginhandler.LoginParticipantDB(participant.Email, participant.Password);
            if (status.LoginStatus.IsSuccess)
            {
                //Create an object with userinfo about the participant.
                var userInfo = new UserInfo
                {
                    Email = participant.Email,
                    Id = participant.IdParticipant,
                    isAdmin = false
                };

                //Generates token with claims defined from the userinfo object.
                var accessTokenResult = tokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
                participant.Email,
                AddMyClaims(userInfo));
                await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal,
                    accessTokenResult.AuthProperties);
            }
            else
            {
                /*
                // Handle error jacob
                return View("WelcomePageParticipant");
                */
            }
            return RedirectToAction("Index", "Homepage");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LoginResearcher")]
        [Route("Welcome/LoginResearcher")]
        public ActionResult LoginResearcher([Bind("Email,Password")] Researcher researcher)
        {

            LoginHandler loginhandler = new LoginHandler();
            var status = loginhandler.LoginResearcherDB(researcher.Email, researcher.Password);
            if (status.LoginStatus.IsSuccess)
            {
                return View("../HomePage/index", status.LoginStatus.researcher);
            }
            else
            {
                // Handle error jacob
                return View("WelcomePageParticipant");
            }

        }

        public ActionResult WelcomePageResearcher()
        {
            return View();
        }

        // POST: Welcome/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Welcome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Welcome/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Welcome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Welcome/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        private static IEnumerable<Claim> AddMyClaims(UserInfo userInfo)
        {
            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim("HasAdminRights", userInfo.isAdmin ? "Y" : "N")
            };

            return myClaims;
        }
    }
}